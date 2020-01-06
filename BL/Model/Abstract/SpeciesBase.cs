using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Базовый класс для видов пожарного инвентаря.
    /// </summary>
    public abstract class KindBase : EntityBase
    {
        /// <summary>
        /// Марка пожарного инвентаря.
        /// </summary>
        [Column("Марка")]
        [Control("TextBox", true)]
        public string Name { get; set; }

        /// <summary>
        /// Производитель пожарного инвентаря.
        /// </summary>
        [Column("Производитель")]
        [Control("TextBox", true)]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Сравнение типов пожарного инвентаря по типу, марке, производителю.
        /// </summary>
        public bool EqualsValues(KindBase obj)
        {
            if (GetType() != obj.GetType())
                return false;
            var th = obj;
            if (Name == th.Name && Manufacturer == th.Manufacturer)
                return true;
            return false;
        }
        /// <summary>
        /// Коллекция пожарного инвентаря данного типа.
        /// </summary>
        [NotMapped]
        public abstract ICollection<EntityBase> Childs { get; }
    }
}
