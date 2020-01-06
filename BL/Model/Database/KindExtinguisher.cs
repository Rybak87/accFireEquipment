using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    [Table("SpeciesExtinguishers")]
    public class KindExtinguisher : KindBase//EntityBase, ITypes
    {
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }//Огнетушитель

        [Column("Вид")]
        [Control("TextBox", false)]
        public string Species { get; set; } //Вид

        [Column("Номинальная масса")]
        [Control("NumericUpDownDecimal", false)]
        public double NominalWeight { get; set; }//Номинальная масса

        [Column("Номинальное давление")]
        [Control("NumericUpDownDecimal", false)]
        public double NominalPressure { get; set; }//Номинальное давление

        [Column("Минимальная масса")]
        [Control("NumericUpDownDecimal", false)]
        public double MinWeight { get; set; }//Минимальная масса

        [Column("Минимальное давление")]
        [Control("NumericUpDownDecimal", false)]
        public double MinPressure { get; set; }//Минимальное давление

        [Column("Масса ОТВ")]
        [Control("NumericUpDownDecimal", false)]
        public double WeightExtinguishingAgent { get; set; }//Масса ОТВ

        [Column("Марка ОТВ")]
        [Control("TextBox", false)]
        public double BrandExtinguishingAgent { get; set; }//Масса ОТВ

        [Column("Объем")]
        [Control("NumericUpDownDecimal", false)]
        public double Volume { get; set; }//Объём

        [Column("Время выхода ОТВ")]
        [Control("NumericUpDownDecimal", false)]
        public double OutputTime { get; set; }//Время выхода ОТВ

        [Column("Длина струи")]
        [Control("NumericUpDownDecimal", false)]
        public double LengthStream { get; set; }//Длина струи

        [Column("Класс пожара")]
        [Control("TextBox", false)]
        public string FireClass { get; set; }//Класс пожара

        public override ICollection<EntityBase> Childs { get => Extinguishers.Cast<EntityBase>().ToList(); }

        public KindExtinguisher()
        { }
        public override string ToString()
        {
            return Manufacturer == null ? Name : Name + " (" + Manufacturer + ")";
        }
    }
}
