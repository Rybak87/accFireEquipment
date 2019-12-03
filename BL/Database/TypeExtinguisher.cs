using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BL
{
    [Table("TypeExtinguishers")]
    public class TypeExtinguisher: EntityBase, ITypes
    {
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }//Огнетушитель

        [Column("Марка")]
        [Control("TextBox", true)]
        public string Name { get; set; }//Марка

        [Column("Производитель")]
        [Control("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель

        [Column("Вид")]
        [Control("TextBox", false)]
        public string Species { get; set; } //Вид

        [Column("Номинальная масса")]
        [Control("NumericUpDownDecimal", false)]
        public double NominalWeight { get; set; }//Номинальная масса

        [Column("Номинальное давление")]
        [Control("NumericUpDownDecimal", false)]
        public double NominalPressure { get; set; }//Номинальная масса

        [Column("Масса ОТВ")]
        [Control("NumericUpDownDecimal", false)]
        public double WeightExtinguishingAgent { get; set; }//Масса ОТВ

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

        //[Control("Image", false)]
        //public byte[] Image { get; set; }
        public override EntityBase Parent { get => null; set => throw new System.NotImplementedException("Нельзя назначить родителя"); }

        public TypeExtinguisher()
        { }
        public override string ToString()
        {
            return Manufacturer == null ? Name : Name + " (" + Manufacturer + ")";
        }
    }
}
