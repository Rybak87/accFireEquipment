using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace BL
{
    /// <summary>
    /// Изменения свойств пожарного инвентаря.
    /// </summary>
    [Table("История")]
    public class History : EntityBase
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public History()
        {
            DateChange = DateTime.Now;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public History(Equipment equipment, PropertyInfo property, History oldHistory, string newValue)
        {
            Equipment = equipment;
            Property = property.Name;
            PrevHistory = oldHistory;
            Value = newValue;
            DateChange = DateTime.Now;
        }

        /// <summary>
        /// Первичный ключ пожарного инвентаря.
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// Пожарный инвентарь.
        /// </summary>
        public virtual Equipment Equipment { get; set; }

        /// <summary>
        /// Предыдущее изменение свойств пожарного инвентаря.
        /// </summary>
        public virtual History PrevHistory { get; set; }

        /// <summary>
        /// Свойство пожарного инвентаря.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Новое значение свойства пожарного инветаря.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Дата и время внесения изменений.
        /// </summary>
        public DateTime DateChange { get; set; }
    }
}
