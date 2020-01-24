using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Огнетушитель.
    /// </summary>
    [Table("Огнетушители")]
    public class Extinguisher : Equipment, ISticker
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Extinguisher()
        {
            DateProduction = DateTime.Now;
            DateRecharge = DateProduction.AddYears(5);
            IsHose = true;
            IsSticker = true;
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
                //else if (value is SpeciesExtinguisher)
                //    TypeExtinguisher = (SpeciesExtinguisher)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }

        /// <summary>
        /// Возвращает родительское помещение.
        /// </summary>
        public override Location GetLocation => FireCabinet.Location;

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
        /// Первичный ключ вида огнетушителя.
        /// </summary>
        public int KindExtinguisherId { get; set; }

        /// <summary>
        /// Вид огнетушителя.
        /// </summary>
        [Column("Тип огнетушителя")]
        [Control("ComboBox", true, 1)]
        public virtual KindExtinguisher KindExtinguisher { get; set; }

        /// <summary>
        /// Наличие наклейки.
        /// </summary>
        [Column("Наклейка")]
        [Control("CheckBox", false, 8)]
        public bool IsSticker { get; set; }

        /// <summary>
        /// Дата производства.
        /// </summary>
        [Column("Дата производства")]
        [Control("DateTimePicker", false, 6)]
        public DateTime DateProduction { get; set; }

        /// <summary>
        ///Дата перезарядки.
        /// </summary>
        [Column("Дата следующей перезарядки")]
        [Control("DateTimePicker", false, 7)]
        public DateTime DateRecharge { get; set; }

        /// <summary>
        /// Масса.
        /// </summary>
        [Column("Масса")]
        [Control("NumericUpDownDecimal", false, 4)]
        public double Weight { get; set; }

        /// <summary>
        /// Давление.
        /// </summary>
        [Column("Давление")]
        [Control("NumericUpDownDecimal", false, 5)]
        public double Pressure { get; set; }

        /// <summary>
        /// Заводской номер.
        /// </summary>
        [Column("Заводской номер")]
        [Control("NumericUpDown", false, 3)]
        public int SerialNumber { get; set; }

        /// <summary>
        /// Повреждение корпуса.
        /// </summary>
        [Column("Повреждение корпуса")]
        [Control("CheckBox", false, 11)]
        public bool IsDented { get; set; }

        /// <summary>
        /// Повреждение краски.
        /// </summary>
        [Column("Повреждение краски")]
        [Control("CheckBox", false, 12)]
        public bool IsPaintDamage { get; set; }

        /// <summary>
        /// Повреждение ЗПУ.
        /// </summary>
        [Column("Повреждение ЗПУ")]
        [Control("CheckBox", false, 13)]
        public bool IsHandleDamage { get; set; }

        /// <summary>
        /// Наличие шланга.
        /// </summary>
        [Column("Наличие шланга")]
        [Control("CheckBox", false, 9)]
        public bool IsHose { get; set; }

        /// <summary>
        /// Неисправность манометра.
        /// </summary>
        [Column("Неисправность манометра")]
        [Control("CheckBox", false, 10)]
        public bool IsPressureGaugeFault { get; set; }

        /// <summary>
        /// Повреждение этикетки.
        /// </summary>
        [Column("Повреждение этикетки")]
        [Control("CheckBox", false, 14)]
        public bool IsLabelDamage { get; set; }



        /// <summary>
        /// Возвращает именование в соответствии с шаблоном.
        /// </summary>
        public override string ToString()
        {
            var sample = Properties.Settings.Default.SampleNameExtinguishers;
            sample = sample.Replace("#L", ((Location)FireCabinet.Parent).Number.ToString());
            sample = sample.Replace("#F", FireCabinet.Number.ToString());
            sample = sample.Replace("#E", Number.ToString());
            return sample;
        }
    }
}
