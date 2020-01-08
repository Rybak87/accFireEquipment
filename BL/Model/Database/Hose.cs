using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Рукав.
    /// </summary>
    [Table("Hoses")]
    public class Hose : Equipment
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Hose()
        {
            DateProduction = DateTime.Now;
            DateRolling = DateProduction.AddYears(1);
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
                //else if (value is SpeciesHose)
                //    TypeHose = (SpeciesHose)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }

        /// <summary>
        /// Возвращает родительское помещение.
        /// </summary>
        public override Location GetLocation => FireCabinet.Location;

        /// <summary>
        /// Первичный ключ вида рукава.
        /// </summary>
        public int KindHoseId { get; set; }

        /// <summary>
        /// Вид рукава.
        /// </summary>
        [Column("Тип рукава")]
        [Control("ComboBox", true, 1/*, "KindHoses"*/)]
        public virtual KindHose KindHose { get; set; }

        /// <summary>
        /// Дата производства
        /// </summary>
        [Column("Дата производства")]
        [Control("DateTimePicker", false, 3)]
        public DateTime DateProduction { get; set; }

        /// <summary>
        /// Дата перекатки
        /// </summary>
        [Column("Дата перекатки")]
        [Control("DateTimePicker", false, 4)]
        public DateTime DateRolling { get; set; }

        /// <summary>
        /// Повреждения
        /// </summary>
        [Column("Повреждения")]
        [Control("CheckBox", false, 5)]
        public bool IsRagged { get; set; }

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
            var sample = Properties.Settings.Default.SampleNameHoses;
            sample = sample.Replace("#L", ((Location)FireCabinet.Parent).Number.ToString());
            sample = sample.Replace("#F", FireCabinet.Number.ToString());
            sample = sample.Replace("#H", Number.ToString());
            return sample;
        }

    }
}
