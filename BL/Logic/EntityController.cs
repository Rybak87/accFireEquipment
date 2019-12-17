using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BL
{
    public class EntityController : BLContext
    {
        public event Action<EntityBase> entityAdd;
        //public event Action<EntityBase> entityEdit;
        public event Action<EntityBase> entityRemove;

        public EntityBase CreateEntity(Type typeEntity) => (EntityBase)Set(typeEntity).Create();

        /// <summary>Возвращает сущность по его метке.
        /// Второй параметр NoTracking.
        /// </summary>
        public EntityBase GetEntity(EntitySign sign, bool noTracking = false)
        {
            //var table = Set(sign.Type);
            //if (noTracking)
            //    table = (DbSet)table.AsNoTracking();
            //return (EntityBase)table.Find(sign.Id);
            var result = (EntityBase)Set(sign.Type).Find(sign.Id);
            if (noTracking)
                Entry(result).State = EntityState.Detached;
            return result;

        }

        /// <summary>
        /// Добавляет сущность в БД.
        /// Вызывает событие по добавлению сущности.
        /// </summary>
        public void AddEntity(EntityBase entity)
        {
            Set(entity.GetType()).Add(entity);
            //SaveChanges();
            if (entity is Equipment)
            {
                var newValues = GetValues(entity as Equipment);
                var oldValues = new List<string>(newValues.Count);
                for (int i = 0; i < newValues.Count; i++)
                    oldValues.Add("");
                AddHistory(entity as Equipment, oldValues, newValues);
            }

            entityAdd?.Invoke(entity);
        }

        /// <summary>Добавляет сущности в БД.</summary>
        public void AddRangeEntity(EntityBase entity, int count)
        {
            AddEntity(entity);
            int currNumber = ((INumber)entity).Number;
            for (int i = 1; i < count; i++)
            {
                currNumber++;
                var copyEntity = CopyEntity(entity.GetSign());
                ((INumber)copyEntity).Number = currNumber;
                AddEntity(copyEntity);
            }
        }

        /// <summary>
        /// Помечает сущность как измененную.
        /// Сохраняет изменения в БД.
        /// Вызывает событие по изменению сущности.
        /// </summary>
        //public void EditEntity(EntityBase entity)
        //{
        //    Entry(entity).State = EntityState.Modified;
        //    entityEdit?.Invoke(entity);
        //    SaveChanges();
        //}

        /// <summary>
        /// Копирует и возврщает сущность.
        /// </summary>
        public EntityBase CopyEntity(EntitySign sign)
        {
            var entitySourse = GetEntity(sign);
            var newEntity = CreateEntity(sign.Type);
            Set(sign.Type).Attach(newEntity);
            EntityBase temp = (EntityBase)Entry(entitySourse).CurrentValues.ToObject();
            temp.Id = newEntity.Id;
            Entry(newEntity).CurrentValues.SetValues(temp);
            return newEntity;
        }

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        public void RemoveEntity(EntitySign sign)
        {
            var entity = GetEntity(sign);
            entityRemove?.Invoke(entity);
            if (sign.Type.IsSubclassOf(typeof(SpeciesBase)))
            {
                if (((SpeciesBase)entity).Childs.Count != 0)
                {
                    MessageBox.Show("Существует инвентарь с этим типом");
                    return;
                }
            }

            Set(sign.Type).Remove(entity);
        }

        /// <summary>
        /// Возвращает таблицу из БД в виде List.
        /// </summary>
        public List<EntityBase> GetTableList(Type typeEntity) => ((IQueryable<EntityBase>)Set(typeEntity)).ToList();

        /// <summary>
        /// Возвращает таблицу из БД в виде List.
        /// </summary>
        public List<EntityBase> GetTableList(DbSet table) => ((IQueryable<EntityBase>)table).ToList();

        /// <summary>
        /// Возвращает следующий по порядку номер подсущности.
        /// </summary>
        public int GetNumberChild(EntityBase entity, Type childType)
        {
            var propertiesEntity = entity.GetType().GetProperties();
            var desiredType = typeof(ICollection<>).MakeGenericType(childType);
            var findedProperty = propertiesEntity.Single(p => p.PropertyType == desiredType);
            var findedCollection = findedProperty.GetValue(entity) as IEnumerable<INumber>;
            if (findedCollection.Count() != 0)
                return findedCollection.Max(ch => ch.Number) + 1;
            else
                return 1;
        }
        /// <summary>
        /// Возвращает следующий по порядку номер сущности.
        /// </summary>
        public int GetNumber(EntityBase entity)
        {
            var EntityBaseCollection = GetTableList(entity.GetType());
            var findedCollection = EntityBaseCollection.Cast<INumber>();
            if (findedCollection.Count() != 0)
                return findedCollection.Max(e => e.Number) + 1;
            else
                return 1;
        }
        /// <summary>
        /// Возвращает родительский Location.
        /// </summary>

        public List<(PropertyInfo, ControlAttribute, string)> GetProperties(EntityBase entity)
        {
            var result = new List<(PropertyInfo, ControlAttribute, string)>();
            foreach (PropertyInfo prop in entity?.GetType().GetProperties())
            {
                var controlAttr = GetControlAttribute(prop);
                if (controlAttr == null)
                    continue;

                bool controlHide = controlAttr.IsCanHide;
                if (controlHide)
                    continue;

                var nameAttr = GetColumnAttribute(prop)?.Name;
                result.Add((prop, controlAttr, nameAttr));
            }
            return result;

            ControlAttribute GetControlAttribute(PropertyInfo pi)
            {
                foreach (var item in pi.GetCustomAttributes())
                    if (item.GetType() == typeof(ControlAttribute))
                        return (ControlAttribute)item;
                return null;
            }
            ColumnAttribute GetColumnAttribute(PropertyInfo pi)
            {
                foreach (var item in pi.GetCustomAttributes())
                    if (item.GetType() == typeof(ColumnAttribute))
                        return (ColumnAttribute)item;
                return null;
            }
        }

        public void AddHistory(Equipment currEntity, List<string> saveValues, List<string> currValues)
        {
            //int i = 0;
            //foreach (var pr in GetProperties(currEntity).Select(j => j.Item1))
            for (int i = 0; i < saveValues.Count(); i++)
            {
                if (saveValues[i] != currValues[i])
                {
                    var hy = (History)CreateEntity(typeof(History));
                    hy.EquipmentBase = currEntity;
                    hy.Property = pr.Name;
                    hy.OldValue = saveValues[i];
                    hy.NewValue = currValues[i];
                    AddEntity(hy);
                }
                i++;
            }
            SaveChanges();
        }
        public List<string> GetValues(Equipment currEntity)
        {
            var result = new List<string>();
            foreach (var pr in GetProperties(currEntity))
                result.Add(pr.Item1.GetValue(currEntity).ToString());
            return result;
        }
        public IEnumerable<Equipment> GetDrawEquipment(Location location)
        {
            var result = new List<Equipment>();

            var fireCabinets = Entry(location).Collection(l => l.FireCabinets)?.Query().AsNoTracking();
            var drawFireCabinets = fireCabinets.Where(f => !f.Point.Empty);
            var drawExtinguishers = fireCabinets.SelectMany(f => f.Extinguishers).Where(e => !e.Point.Empty);
            var drawHoses = fireCabinets.SelectMany(f => f.Hoses).Where(h => !h.Point.Empty);
            var drawHydrants = fireCabinets.SelectMany(f => f.Hydrants).Where(hy => !hy.Point.Empty);
            result.AddRange(drawFireCabinets);
            result.AddRange(drawExtinguishers);
            result.AddRange(drawHoses);
            result.AddRange(drawHydrants);
            return result;
        }

        public Location GetParentLocation(EntitySign sign)
        {
            var entity = GetEntity(sign, false);
            if (!(entity is Hierarchy))
                return null;
            return ((Hierarchy)entity).GetLocation;
        }
    }
}
