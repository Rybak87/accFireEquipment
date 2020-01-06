﻿using System;
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
        public event Action<Hierarchy> entityAdd;
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
            if (entity is Equipment)
            {
                var historySet = new HistorySet(entity as Equipment);
                historySet.SetNewValues();
                historySet.SetOldValuesEmpty();
                historySet.Save(this);
            }
            if (entity is Hierarchy)
                entityAdd?.Invoke((Hierarchy)entity);
        }

        /// <summary>Добавляет сущности в БД.</summary>
        public void AddRangeEntity(Hierarchy entity, int count)
        {
            AddEntity(entity);
            int currNumber = entity.Number;
            for (int i = 1; i < count; i++)
            {
                currNumber++;
                var copyEntity = CopyEntity(entity.GetSign());
                ((Hierarchy)copyEntity).Number = currNumber;
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
            if (sign.Type.IsSubclassOf(typeof(KindBase)))
            {
                if (((KindBase)entity).Childs.Count != 0)
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
        public int GetNumberChild(Hierarchy entity, Type childType)
        {
            var propertiesEntity = entity.GetType().GetProperties();
            var desiredType = typeof(ICollection<>).MakeGenericType(childType);
            var findedProperty = propertiesEntity.Single(p => p.PropertyType == desiredType);
            var findedCollection = findedProperty.GetValue(entity) as IEnumerable<Hierarchy>;
            if (findedCollection.Count() != 0)
                return findedCollection.Max(ch => ch.Number) + 1;
            else
                return 1;
        }
        /// <summary>
        /// Возвращает следующий по порядку номер сущности.
        /// </summary>
        public int GetNumber(Hierarchy entity)
        {
            var EntityBaseCollection = GetTableList(entity.GetType());
            var findedCollection = EntityBaseCollection.Cast<Hierarchy>();
            if (findedCollection.Count() != 0)
                return findedCollection.Max(e => e.Number) + 1;
            else
                return 1;
        }


        public List<(PropertyInfo, ControlAttribute, string)> GetEditProperties(EntityBase entity)
        {
            var result = new List<(PropertyInfo, ControlAttribute, string)>();
            foreach (PropertyInfo prop in entity?.GetType().GetProperties())
            {
                var controlAttr = GetControlAttribute(prop);
                if (controlAttr == null)
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

        public IEnumerable<Equipment> GetDrawEquipment(Location location)
        {
            var result = new List<Equipment>();

            var fireCabinets = Entry(location).Collection(l => l.FireCabinets)?.Query().AsNoTracking();
            var drawFireCabinets = fireCabinets.Where(f => f.Point.Displayed);
            var drawExtinguishers = fireCabinets.SelectMany(f => f.Extinguishers).Where(e => e.Point.Displayed);
            var drawHoses = fireCabinets.SelectMany(f => f.Hoses).Where(h => h.Point.Displayed);
            var drawHydrants = fireCabinets.SelectMany(f => f.Hydrants).Where(hy => hy.Point.Displayed);
            result.AddRange(drawFireCabinets);
            result.AddRange(drawExtinguishers);
            result.AddRange(drawHoses);
            result.AddRange(drawHydrants);
            return result;
        }

        /// <summary>
        /// Возвращает родительский Location.
        /// </summary>
        public Location GetParentLocation(EntitySign sign)
        {
            var entity = GetEntity(sign, false);
            if (!(entity is Hierarchy))
                return null;
            return ((Hierarchy)entity).GetLocation;
        }
    }
}
