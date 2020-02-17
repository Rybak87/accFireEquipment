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
        [Control("TextBox", true, 3)]
        public string Name { get; set; }

        /// <summary>
        /// План (изображение).
        /// </summary>
        [Column("Изображение")]
        [Control("Image", false, 4)]
        public virtual byte[] Plan { get; set; }

        /// <summary>
        /// Пожарные шкафы.
        /// </summary>
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }

        public override Hierarchy Clone()
        {
            return new Location
            {
                Id = 0,
                Number = this.Number,
                Name = this.Name,
                Plan = this.Plan
            };
        }

        /// <summary>
        /// Возвращает именование.
        /// </summary>
        public override string ToString() => Name;
    }
}
