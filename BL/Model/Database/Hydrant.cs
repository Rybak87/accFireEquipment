using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Гидрант.
    /// </summary>
    [Table("Пожарные краны")]
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
        [Control("CheckBox", false, 3)]
        public bool IsDamage { get; set; }

        /// <summary>
        /// Первичный ключ пожарного шкафа.
        /// </summary>
        public int FireCabinetId { get; set; }

        /// <summary>
        /// Пожарный шкаф.
        /// </summary>
        [Column("Пожарный шкаф")]
        public virtual FireCabinet FireCabinet { get; set; }

        /// <summary>
        /// Возвращает именование в соответствии с шаблоном.
        /// </summary>
        public override string ToString()
        {
            var sample = Properties.Settings.Default.SampleNameHydrants;
            sample = sample.Replace("#L", ((Location)FireCabinet.Parent).Number.ToString());
            sample = sample.Replace("#F", FireCabinet.Number.ToString());
            sample = sample.Replace("#D", Number.ToString());
            return sample;
        }

    }
}
