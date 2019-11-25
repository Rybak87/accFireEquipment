using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BL
{
    public class EntityController : BLContext
    {
        public event Action<EntityBase> entityAdd;
        public event Action<EntityBase> entityEdit;
        public event Action<EntityBase> entityRemove;

        public EntityBase CreateEntity(Type typeEntity) => (EntityBase)GetCollection(typeEntity).Create();
        public EntityBase GetEntity(EntitySign sign)
        {
            if (sign.typeEntity == null)
                return null;
            var coll = GetCollection(sign.typeEntity);
            return (EntityBase)coll.Find(sign.idEntity);
        }
        public void AddNewEntity(EntityBase entity)
        {
            GetCollection(entity.GetType()).Add(entity);
            SaveChanges();
            entityAdd?.Invoke(entity);
        }
        public void EditEntity(EntitySign sign)
        {
            Entry(GetEntity(sign)).State = EntityState.Modified;
            entityEdit?.Invoke(GetEntity(sign));
            SaveChanges();
        }
        public void RemoveEntity(EntitySign sign)
        {
            var entity = GetEntity(sign);
            entityRemove?.Invoke(entity);
            GetCollection(sign.typeEntity).Remove(entity);
            SaveChanges();
        }
        public DbSet GetCollection(Type typeEntity)
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
            else if (typeEntity == typeof(TypeExtinguisher))
                return TypeExtinguishers;
            else if (typeEntity == typeof(TypeFireCabinet))
                return TypeFireCabinets;
            else if (typeEntity == typeof(TypeHose))
                return TypeHoses;
            return null;
        }
        public List<EntityBase> GetCollectionList(Type typeEntity) => ((IEnumerable<EntityBase>)GetCollection(typeEntity)).ToList();
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
        public int GetNumber(EntityBase entity)
        {
            var EntityBaseCollection = GetCollectionList(entity.GetType());
            var findedCollection = EntityBaseCollection.Cast<INumber>();
            if (GetCollection(entity.GetType()).Local.Count != 0)
                return findedCollection.Max(e => e.Number) + 1;
            else
                return 1;
        }
        public Location ParentLocation(EntitySign sign)
        {
            var entity = GetEntity(sign);
            if (sign.typeEntity == typeof(Location))
                return (Location)entity;
            return ParentLocation(entity.Parent.GetSign());
        }
    }
}
