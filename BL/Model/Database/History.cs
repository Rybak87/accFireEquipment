using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Изменения свойств пожарного инвентаря.
    /// </summary>
    [Table("History")]
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
        /// Первичный ключ пожарного инвентаря.
        /// </summary>
        public int EquipmentBaseId { get; set; }

        /// <summary>
        /// Пожарный инвентарь.
        /// </summary>
        public virtual Equipment EquipmentBase { get; set; }

        //public virtual Inspection Inspection { get; set; }
        //public int InspectionId { get; set; }

        /// <summary>
        /// Свойство пожарного инвентаря.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Новое значение свойства пожарного инветаря.
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Дата и время внесения изменений.
        /// </summary>
        public DateTime DateChange { get; set; }

    }
}
