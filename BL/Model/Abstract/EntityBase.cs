using System;
using System.Data.Entity.Core.Objects;

namespace BL
{
    /// <summary>
    /// Базовый класс для всех сущностей БД
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сравнивает объекты по типу и первичному ключу
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>Идентичны ли объекты</returns>
        public override bool Equals(object obj)
        {
            //if (ObjectContext.GetObjectType(obj.GetType()) != ObjectContext.GetObjectType(GetType())) //Приводим к чистому типу и сравниваем
            if (ObjectContext.GetObjectType(obj.GetType()) != GetType()) //Приводим к объект сравнения к чистому типу и сравниваем
                return false;
            else
                if (Id == ((EntityBase)obj).Id) //Сравниваем по первичному ключу
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает идентификатор сущности
        /// </summary>
        /// <returns>Возвращает идентификатор сущности</returns>
        public EntitySign GetSign()
        {
            return new EntitySign(GetType(), Id);
        }

        /// <returns>Возвращает хэш-код</returns>
        public override int GetHashCode() => GetType().GetHashCode() + Id;

        /// <returns>Результат сравнения на равенство</returns>
        public static bool operator ==(EntityBase e1, object e2) => Equals(e1, e2);

        /// <returns>Результат сравнения на неравенство</returns>
        public static bool operator !=(EntityBase e1, object e2) => !Equals(e1, e2);

        /// <returns>Возвращает чистый тип</returns>
        public new Type GetType() => ObjectContext.GetObjectType(base.GetType());
    }
}
