using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Базовый класс для пожарного инвентаря.
    /// </summary>
    public abstract class Equipment : Hierarchy
    {
        /// <summary>
        /// Родитель.
        /// </summary>
        [NotMapped]
        public abstract Hierarchy Parent { get; set; }

        /// <summary>
        /// Относительная точка.
        /// </summary>
        public ScalePoint Point { get; set; }

        /// <summary>
        /// Коллекция изменений сущности.
        /// </summary>
        public virtual ICollection<History> Histories { get; set; }
    }
}
