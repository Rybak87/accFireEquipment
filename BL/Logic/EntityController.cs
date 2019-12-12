using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace BL
{
    public class EntityController : BLContext
    {
        public event Action<EntityBase> entityAdd;
        public event Action<EntityBase> entityEdit;
        public event Action<EntityBase> entityRemove;

        public EntityBase CreateEntity(Type typeEntity) => (EntityBase)GetTable(typeEntity).Create();

        /// <summary>Возвращает сущность по его метке.
        /// Второй параметр NoTracking.
        /// </summary>
        public EntityBase GetEntity(EntitySign sign, bool noTracking = false)
        {
            var table = GetIQueryable(sign.Type);
            if (noTracking)
                table = table.AsNoTracking();
            return table.FirstOrDefault(ent => ent.Id == sign.Id);
        }

        /// <summary>
        /// Добавляет сущность в БД.
        /// Вызывает событие по добавлению сущности.
        /// </summary>
        public void AddEntity(EntityBase entity)
        {
            GetTable(entity.GetType()).Add(entity);
            SaveChanges();
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
        public void EditEntity(EntitySign sign)
        {
            Entry(GetEntity(sign)).State = EntityState.Modified;
            entityEdit?.Invoke(GetEntity(sign));
            SaveChanges();
        }

        /// <summary>
        /// Копирует и возврщает сущность.
        /// </summary>
        public EntityBase CopyEntity(EntitySign sign)
        {
            var entitySourse = GetEntity(sign);
            var newEntity = CreateEntity(sign.Type);
            GetTable(sign.Type).Attach(newEntity);
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

            GetTable(sign.Type).Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// Возвращает таблицу из БД.
        /// </summary>
        public DbSet GetTable(Type typeEntity)
        {
            if (typeEntity == typeof(Location))
                return Locations;
            else if (typeEntity == typeof(FireCabinet))
                return FireCabinets;
            else if (typeEntity == typeof(Extinguisher))
                return Extinguishers;
            else if (typeEntity == typeof(Hose))
                return Hoses;
            else if (typeEntity == typeof(Hydrant))
                return Hydrants;
            else if (typeEntity == typeof(SpeciesExtinguisher))
                return TypeExtinguishers;
            else if (typeEntity == typeof(SpeciesFireCabinet))
                return TypeFireCabinets;
            else if (typeEntity == typeof(SpeciesHose))
                return TypeHoses;
            return null;
        }

        /// <summary>
        /// Возвращает IQueryable<EntityBase> из БД.
        /// </summary>
        public IQueryable<EntityBase> GetIQueryable(Type typeEntity)
        {
            if (typeEntity == typeof(Location))
                return Locations;
            else if (typeEntity == typeof(FireCabinet))
                return FireCabinets;
            else if (typeEntity == typeof(Extinguisher))
                return Extinguishers;
            else if (typeEntity == typeof(Hose))
                return Hoses;
            else if (typeEntity == typeof(Hydrant))
                return Hydrants;
            else if (typeEntity == typeof(SpeciesExtinguisher))
                return TypeExtinguishers;
            else if (typeEntity == typeof(SpeciesFireCabinet))
                return TypeFireCabinets;
            else if (typeEntity == typeof(SpeciesHose))
                return TypeHoses;
            return null;
        }

        /// <summary>
        /// Возвращает таблицу из БД в виде List.
        /// </summary>
        public List<EntityBase> GetTableList(Type typeEntity) => ((IEnumerable<EntityBase>)GetTable(typeEntity)).ToList();

        /// <summary>
        /// Возвращает таблицу из БД в виде List.
        /// </summary>
        public List<EntityBase> GetTableList(DbSet table) => ((IEnumerable<EntityBase>)table).ToList();

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
            if (GetTable(entity.GetType()).Local.Count != 0)
                return findedCollection.Max(e => e.Number) + 1;
            else
                return 1;
        }
        /// <summary>
        /// Возвращает родительский Location.
        /// </summary>
        public Location ParentLocation(EntitySign sign, bool noTracking = false)
        {
            var entity = GetEntity(sign, noTracking);
            if (sign.Type == typeof(Location))
                return (Location)entity;
            return ParentLocation(entity.Parent.GetSign());
        }
    }
}
