using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BL
{
    public class TypeExtinguisher
    {
        public int TypeExtinguisherId { get; set; }//ID
        public virtual ICollection<Extinguisher> Extinguisher { get; set; }//Огнетушитель

        [Prop("TextBox", false)]
        public string Species { get; set; } //Вид

        [Prop("TextBox", true)]
        public string Label { get; set; }//Марка

        [Prop("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель

        [Prop("NumericUpDownDecimal", false)]
        public double NominalWeight { get; set; }//Номинальная масса

        [Prop("NumericUpDownDecimal", false)]
        public double NominalPressure { get; set; }//Номинальная масса

        [Prop("NumericUpDownDecimal", false)]
        public double MassExtinguishingAgent { get; set; }//Масса ОТВ

        [Prop("NumericUpDownDecimal", false)]
        public double Volume { get; set; }//Объём

        [Prop("NumericUpDownDecimal", false)]
        public double OutputTime { get; set; }//Время выхода ОТВ

        [Prop("NumericUpDownDecimal", false)]
        public double LengthStream { get; set; }//Длина струи

        [Prop("TextBox", false)]
        public string FireClass { get; set; }//Класс пожара

        [Prop("Image", false)]
        public byte[] Image { get; set; }
        public TypeExtinguisher()
        { }
    }
}
