using System;

namespace BL
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public class EntitySign
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public EntitySign(Type typeEntity, int idEntity)
        {
            Type = typeEntity;
            Id = idEntity;
        }

        /// <summary>
        /// Тип сущности.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Первичный ключ сущности.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Сравнивает объекты по типу и первичному ключу
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>Идентичны ли объекты</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;
            else if (Type == ((EntitySign)obj).Type && Id == ((EntitySign)obj).Id)
                return true;
            return false;
        }

        /// <returns>Возвращает хэш-код</returns>
        public override int GetHashCode() => Type.GetHashCode() + Id.GetHashCode();

        /// <returns>Результат сравнения на равенство</returns>
        public static bool operator ==(EntitySign e1, object e2) => Equals(e1, e2);

        /// <returns>Результат сравнения на неравенство</returns>
        public static bool operator !=(EntitySign e1, object e2) => !Equals(e1, e2);
    }
}
