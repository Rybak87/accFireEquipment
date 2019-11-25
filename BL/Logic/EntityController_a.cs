//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;

//namespace BL
//{
//    public class EntityController : BLContext
//    {
//        public event Action<EntityBase> entityAdd;
//        public event Action<EntityBase> entityEdit;
//        public event Action<EntityBase> entityRemove;
//        public DbSet currCollection { get; private set; }///
//        public int CollectionCount
//        {
//            get
//            {
//                currCollection.Load();
//                return currCollection.Local.Count;
//            }
//        }

//        public EntityController(ref EntityBase entity) : this(entity.GetType())
//        {
//            entity = GetEntity(entity.Id);
//        }
//        public EntityController(EntityBase entity) : this(entity.GetType())
//        {
//        }
//        public EntityController(Type typeEntity) : base()
//        {
//            currCollection = GetCollection(typeEntity);
//        }

//        public EntityBase CreateEntity() => (EntityBase)currCollection.Create();
//        public EntityBase GetEntity(int id) => (EntityBase)currCollection.Find(id);
//        public EntityBase AttachEntity(EntityBase entity)
//        {
//            var coll = GetCollection(entity.GetType());
//            var findEntity = coll.Find(entity.Id);
//            var attachEntity = coll.Attach(findEntity);
//            return (EntityBase)attachEntity;
//        }
//        public void AddNewEntity(EntityBase entity, byte[] currImage = null)
//        {
//            currCollection.Add(entity);

//            if (currImage != null)
//            {
//                SaveChanges();
//                var img = new ImageLocation(entity.Id, currImage);
//                ImagesLocation.Add(img);
//            }

//            entityAdd?.Invoke(entity);

//            SaveChanges();
//        }
//        public void EditEntity(EntityBase entity, byte[] currImage = null)
//        {
//            Entry(entity).State = EntityState.Modified;
//            entityEdit?.Invoke(entity);
//            var img = ImagesLocation.FirstOrDefault(i => i.Id == entity.Id);
//            if (currImage != null)
//            {
//                if (img != null)
//                    img.Image = currImage;
//                else
//                {
//                    img = new ImageLocation(entity.Id, currImage);
//                    ImagesLocation.Add(img);
//                }
//            }
//            else
//            {
//                if (entity is Location && img != null)
//                    ImagesLocation.Remove(img);
//            }
//            SaveChanges();
//        }
//        public void EditEntity(ref EntityBase entity, byte[] currImage = null)
//        {
//            Entry(entity).State = EntityState.Modified;
//            entityEdit?.Invoke(entity);
//            int findedId = entity.Id;
//            var img = ImagesLocation.FirstOrDefault(i => i.Id == findedId);
//            if (currImage != null)
//            {
//                if (img != null)
//                    img.Image = currImage;
//                else
//                {
//                    img = new ImageLocation(entity.Id, currImage);
//                    ImagesLocation.Add(img);
//                }
//            }
//            else
//            {
//                if (entity is Location && img != null)
//                    ImagesLocation.Remove(img);
//            }
//            SaveChanges();
//        }
//        public void RemoveEntity(int entityId)
//        {
//            var entity = GetEntity(entityId);
//            entityRemove?.Invoke(entity);
//            currCollection.Remove(entity);
//            SaveChanges();
//        }
//        public void RemoveEntity(EntityBase entity)
//        {
//            var entity2 = GetEntity(entity.Id);
//            entityRemove?.Invoke(entity2);
//            currCollection.Remove(entity2);
//            SaveChanges();
//        }
//        public DbSet GetCollection(Type typeEntity)
//        {
//            if (typeEntity == typeof(Location))
//            {
//                Locations.Include(l => l.Image).Load();
//                return Locations;
//            }
//            else if (typeEntity == typeof(FireCabinet))
//                return FireCabinets;
//            else if (typeEntity == typeof(Extinguisher))
//                return Extinguishers;
//            else if (typeEntity == typeof(Hose))
//                return Hoses;
//            else if (typeEntity == typeof(Hydrant))
//                return Hydrants;
//            else if (typeEntity == typeof(TypeExtinguisher))
//                return TypeExtinguishers;
//            else if (typeEntity == typeof(TypeFireCabinet))
//                return TypeFireCabinets;
//            else if (typeEntity == typeof(TypeHose))
//                return TypeHoses;
//            return null;
//        }
//        public List<EntityBase> GetCollectionList(Type typeEntity) => ((IEnumerable<EntityBase>)GetCollection(typeEntity)).ToList();
//        public int GetNumber(EntityBase entity, Type childType)
//        {
//            var propertiesEntity = entity.GetType().GetProperties();
//            var desiredType = typeof(ICollection<>).MakeGenericType(childType);
//            var findedProperty = propertiesEntity.Single(p => p.PropertyType == desiredType);
//            var findedCollection = findedProperty.GetValue(entity) as IEnumerable<INumber>;
//            if (findedCollection.Count() != 0)
//                return findedCollection.Max(ch => ch.Number) + 1;
//            else
//                return 1;
//        }
//        public int GetNumber(EntityBase entity)
//        {
//            var EntityBaseCollection = GetCollectionList(entity.GetType());
//            var findedCollection = EntityBaseCollection.Cast<INumber>();
//            if (currCollection.Local.Count != 0)
//                return findedCollection.Max(e => e.Number) + 1;
//            else
//                return 1;
//        }
//        public Location ParentLocation(EntityBase entity)
//        {
//            if (entity.GetType() == typeof(Location))
//                return (Location)entity;
//            return ParentLocation(entity.Parent);
//        }


//    }
//}
