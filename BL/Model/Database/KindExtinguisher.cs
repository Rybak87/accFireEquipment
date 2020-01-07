using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Вид огнетушителя.
    /// </summary>
    [Table("SpeciesExtinguishers")]
    public class KindExtinguisher : KindBase
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public KindExtinguisher()
        { }

        /// <summary>
        /// Коллекция пожарного инвентаря данного типа.
        /// </summary>
        public override ICollection<EntityBase> Childs { get => Extinguishers.Cast<EntityBase>().ToList(); }

        /// <summary>
        /// Коллекция огнетушителей данного типа.
        /// </summary>
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }

        /// <summary>
        /// Номинальная масса.
        /// </summary>
        [Column("Номинальная масса")]
        [Control("NumericUpDownDecimal", false)]
        public double NominalWeight { get; set; }

        /// <summary>
        /// Номинальное давление.
        /// </summary>
        [Column("Номинальное давление")]
        [Control("NumericUpDownDecimal", false)]
        public double NominalPressure { get; set; }

        /// <summary>
        /// Минимально допустимая масса.
        /// </summary>
        [Column("Минимальная масса")]
        [Control("NumericUpDownDecimal", false)]
        public double MinWeight { get; set; }

        /// <summary>
        /// Минимально допустимок давление.
        /// </summary>
        [Column("Минимальное давление")]
        [Control("NumericUpDownDecimal", false)]
        public double MinPressure { get; set; }

        /// <summary>
        /// Масса ОТВ.
        /// </summary>
        [Column("Масса ОТВ")]
        [Control("NumericUpDownDecimal", false)]
        public double WeightExtinguishingAgent { get; set; }

        /// <summary>
        /// Марка ОТВ.
        /// </summary>
        [Column("Марка ОТВ")]
        [Control("TextBox", false)]
        public double BrandExtinguishingAgent { get; set; }

        /// <summary>
        /// Объём.
        /// </summary>
        [Column("Объем")]
        [Control("NumericUpDownDecimal", false)]
        public double Volume { get; set; }

        /// <summary>
        /// Время выхода ОТВ.
        /// </summary>
        [Column("Время выхода ОТВ")]
        [Control("NumericUpDownDecimal", false)]
        public double OutputTime { get; set; }

        /// <summary>
        /// Длина струи.
        /// </summary>
        [Column("Длина струи")]
        [Control("NumericUpDownDecimal", false)]
        public double LengthStream { get; set; }

        /// <summary>
        /// Класс пожара.
        /// </summary>
        [Column("Класс пожара")]
        [Control("TextBox", false)]
        public string FireClass { get; set; }
    }
}
