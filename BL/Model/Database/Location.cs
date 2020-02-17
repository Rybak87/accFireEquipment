using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Помещение.
    /// </summary>
    [Table("Помещения")]
    public class Location : Hierarchy
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Location()
        { }

        /// <summary>
        /// Возвращает родительское помещение.
        /// </summary>
        public override Location GetLocation => this;

        /// <summary>
        /// Название.
        /// </summary>
        [Column("Имя")]
        [Copying]
        [Control("TextBox", true, 3)]
        public string Name { get; set; }

        /// <summary>
        /// План (изображение).
        /// </summary>
        [Column("Изображение")]
        [Copying]
        [Control("Image", false, 4)]
        public virtual byte[] Plan { get; set; }

        /// <summary>
        /// Пожарные шкафы.
        /// </summary>
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }

        /// <summary>
        /// Возвращает именование.
        /// </summary>
        public override string ToString() => Name;
    }
}
