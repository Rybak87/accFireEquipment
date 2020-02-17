using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Базовый класс для иерархичных сущностей.
    /// </summary>
    public abstract class Hierarchy : EntityBase
    {
        /// <summary>
        /// Порядковый номер.
        /// </summary>
        [Column("Номер")]
        [Control("NumericUpDown", true, 2)]
        public int Number { get; set; }

        /// <summary>
        /// Возвращает родительское помещение.
        /// </summary>
        [NotMapped]
        public abstract Location GetLocation { get; }

        public abstract Hierarchy Clone();
    }
}
