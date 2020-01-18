using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BL
{
    /// <summary>
    /// Контроллер сущностей.
    /// </summary>
    public class EntityController : BLContext
    {
        /// <summary>
        /// Событие по добавлению сущности в БД.
        /// </summary>
        public event Action<EntityBase> EntityAdd;

        /// <summary>
        /// Событие по удалению сущности в БД.
        /// </summary>
        public event Action<EntityBase> EntityRemove;

        /// <summary>
        /// Добавляет сущность в БД.
        /// Вызывает событие по добавлению сущности.
        /// </summary>
        public void AddEntity(EntityBase entity)
        {
            Set(entity.GetType()).Add(entity);
            var eq = entity as Equipment;
            if (eq!=null)
            {
                var histories = eq.GetNewHistories();
                Set<History>().AddRange(histories);
            }
            SaveChanges();
            EntityAdd?.Invoke(entity);
        }

        /// <summary>
        /// Добавляет сущности в БД.
        /// </summary>
        /// <param name="entity">Иерархическая сущность.</param>
        /// <param name="count">Количество копий.</param>
        public void AddRangeEntity(Hierarchy entity, int count)
        {
            AddEntity(entity);
            var sign = entity.GetSign();
            var table = Set(sign.Type);
            int currNumber = entity.Number;
            var temp = (Hierarchy)Entry(entity).CurrentValues.ToObject();//Сохранят значения уже добавленной 1-ой сущности
            temp.Id = 0;
            for (int i = 1; i < count; i++)
            {
                currNumber++;
                var newEntity = (Hierarchy)table.Create();
                table.Attach(newEntity);
                Entry(newEntity).CurrentValues.SetValues(temp);//Устанавливает значения уже добавленной 1-ой сущности в новую копию
                newEntity.Number = currNumber;
                AddEntity(newEntity);
            }
        }

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        public void RemoveEntity(EntitySign sign)
        {
            var entity = GetEntity(sign);
            EntityRemove?.Invoke(entity);
            if (sign.Type.IsSubclassOf(typeof(KindBase)))
            {
                if (((KindBase)entity).Childs.Count != 0)//Не используем каскадное удаление
                {
                    MessageBox.Show("Существует инвентарь с этим типом");
                    return;
                }
            }
            Set(sign.Type).Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// Возвращает следующий по порядку номер подсущности.
        /// </summary>
        /// <param name="entity">Иерархическая сущность.</param>
        /// <param name="childType">Тип подсущности.</param>
        /// <returns></returns>
        public int GetNumberChild(Hierarchy entity, Type childType)
        {
            var properties = entity.GetType().GetProperties();
            var desiredType = typeof(ICollection<>).MakeGenericType(childType);//Сгенерированный тип коллекции
            var findedProperty = properties.SingleOrDefault(p => p.PropertyType == desiredType);//Св-во со сгенерирванным типом.
            if (findedProperty == null)
                return 1;
            var findedCollection = findedProperty.GetValue(entity) as IEnumerable<Hierarchy>;//Коллекция подсущностей.
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
            var findedCollection = GetIQueryable(entity.GetType()).Cast<Hierarchy>();
            if (findedCollection.Count() != 0)
                return findedCollection.Max(e => e.Number) + 1;
            else
                return 1;
        }

        /// <summary>
        /// Создает сущность по типу.
        /// </summary>
        /// <param name="typeEntity">Тип сущности.</param>
        /// <returns></returns>
        public EntityBase CreateEntity(Type typeEntity) => (EntityBase)Set(typeEntity).Create();

        /// <summary>
        /// Создает сущность по типу.
        /// </summary>
        /// <typeparam name="T">Тип сущности.</typeparam>
        /// <returns></returns>
        public T CreateEntity<T>() where T : EntityBase => Set<T>().Create();

        /// <summary>Возвращает сущность по его метке.
        /// Второй параметр NoTracking.
        /// </summary>
        public EntityBase GetEntity(EntitySign sign) => (EntityBase)Set(sign.Type).Find(sign.Id);

        /// <summary>
        /// Возвращает родительское помещение.
        /// </summary>
        public Location GetParentLocation(EntitySign sign)
        {
            var entity = GetEntity(sign) as Hierarchy;
            if (entity == null)
                return null;
            return (entity).GetLocation;
        }

        /// <summary>
        /// Возвращает таблицу из БД в виде IQueryable.
        /// </summary>
        public IQueryable<EntityBase> GetIQueryable(Type typeEntity) => Set(typeEntity) as IQueryable<EntityBase>;

        /// <summary>
        /// Возвращает таблицу из БД в виде IQueryable.
        /// </summary>
        public IQueryable<EntityBase> GetIQueryable(DbSet table) => table as IQueryable<EntityBase>;

        /// <summary>
        /// Возвращает коллекцию отрисовываемого инвентаря в помещении.
        /// </summary>
        /// <param name="location">Помещение.</param>
        public IEnumerable<Equipment> GetDrawEquipment(Location location)
        {
            var fireCabinets = Entry(location).Collection(l => l.FireCabinets)?.Query().AsNoTracking();
            Func<Equipment, bool> displayed = eq => eq.Point.Displayed;

            var drawFireCabinets = fireCabinets.Where(displayed);
            var drawExtinguishers = fireCabinets.SelectMany(f => f.Extinguishers).Where(displayed);
            var drawHoses = fireCabinets.SelectMany(f => f.Hoses).Where(displayed);
            var drawHydrants = fireCabinets.SelectMany(f => f.Hydrants).Where(displayed);

            return drawFireCabinets.Concat(drawExtinguishers).Concat(drawHoses).Concat(drawHydrants);
        }
    }
}
