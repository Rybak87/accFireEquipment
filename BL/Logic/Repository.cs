//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data.Entity.Core.Objects;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Linq.Expressions;

//namespace BL
//{
//    public class Repository : IDisposable
//    {
//        private BLContext db;

//        public BLContext DataContext
//        {
//            get { return db ?? (db = new BLContext()); }
//        }

//        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
//        {
//            predicate.CheckNotNull("Predicate value must be passed to Get<TResult>.");

//            return DataContext.Set<TEntity>().Where(predicate).SingleOrDefault();
//        }

//        public IQueryable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
//        {
//            predicate.CheckNotNull("Predicate value must be passed to GetList<TResult>.");
//            return DataContext.Set<TEntity>().Where(predicate);
//        }

//        public IQueryable<TEntity> GetList<TEntity, TKey>(Expression<Func<TEntity, bool>> predicate,
//            Expression<Func<TEntity, TKey>> orderBy) where TEntity : class
//        {
//            return GetList(predicate).OrderBy(orderBy);
//        }

//        public IQueryable<TEntity> GetList<TEntity, TKey>(Expression<Func<TEntity, TKey>> orderBy) where TEntity : class
//        {
//            return GetList<TEntity>().OrderBy(orderBy);
//        }

//        public IQueryable<TEntity> GetList<TEntity>() where TEntity : class
//        {
//            return DataContext.Set<TEntity>();
//        }

//        public bool Save<TEntity>(TEntity entity) where TEntity : class
//        {
//            return DataContext.SaveChanges() > 0;
//        }

//        public bool Update<TEntity>(TEntity entity, params string[] propsToUpdate) where TEntity : class
//        {
//            DataContext.Set<TEntity>().Attach(entity);
//            return DataContext.SaveChanges() > 0;
//        }

//        public bool Delete<TEntity>(TEntity entity) where TEntity : class
//        {
//            ObjectSet<TEntity> objectSet = ((IObjectContextAdapter)DataContext).ObjectContext.CreateObjectSet<TEntity>();
//            objectSet.Attach(entity);
//            objectSet.DeleteObject(entity);
//            return DataContext.SaveChanges() > 0;
//        }

//        public void Dispose()
//        {
//            if (DataContext != null) DataContext.Dispose();
//        }
//        public TEntity CreateNewEntity<TEntity>() where TEntity : class, new()
//        {
//            return new TEntity();
//        }
//    }
//    public static class ObjectExtensions
//    {
//        public static void CheckNotNull(this object value, string error)
//        {
//            if (value == null)
//                throw new Exception(error);
//        }
//        public static List<object> GetListSourse(this object value, string property)
//        {
//            var list = new List<object>();

//            foreach (var item in (IEnumerable)typeof(BLContext).GetProperty(property).GetValue(new BLContext()))
//            {
//                list.Add(item);
//            }
//            return list;
//        }
//    }
//}
