using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Гидрант.
    /// </summary>
    [Table("ПожарныеКраны")]
    public class Hydrant : Equipment
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Hydrant()
        {
            Point = new ScalePoint();
        }

        /// <summary>
        /// Родитель.
        /// </summary>
        public override Hierarchy Parent
        {
            get => FireCabinet;
            set
            {
                if (value is FireCabinet)
                    FireCabinet = (FireCabinet)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }

        /// <summary>
        /// Возвращает родительское помещение.
        /// </summary>
        public override Location GetLocation => FireCabinet.Location;

        /// <summary>
        /// Повреждение.
        /// </summary>
        [Column("Повреждение")]
        [Copying]
        [Control("CheckBox", false, 3)]
        public bool IsDamage { get; set; }

        /// <summary>
        /// Первичный ключ пожарного шкафа.
        /// </summary>
        public int FireCabinetId { get; set; }

        /// <summary>
        /// Пожарный шкаф.
        /// </summary>
        [Copying]
        [Column("Пожарный шкаф")]
        public virtual FireCabinet FireCabinet { get; set; }

        /// <summary>
        /// Возвращает именование в соответствии с шаблоном.
        /// </summary>
        public override string ToString() => GetterOfType.GetName(this);

    }
}
