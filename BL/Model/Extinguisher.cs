using System;
using static System.Net.Mime.MediaTypeNames;

namespace BL
{
    public class Extinguisher
    {
        public int ExtinguisherId { get; set; }//ID
        public int FireCabinetId { get; set; }

        [Prop("ComboBox", true, "FireCabinets")]
        public virtual FireCabinet FireCabinet { get; set; }//Место установки
        public int TypeExtinguisherId { get; set; } //Тип

        [Prop("ComboBox", true, "TypeExtinguishers")]
        public virtual TypeExtinguisher Type { get; set; } //Тип
        public virtual StatusExtinguisher Status { get; set; }//Статус

        [Prop("NumericUpDownDecimal", false)]
        public double Weight { get; set; }//Масса

        [Prop("DateTimePicker", false)]
        public DateTime DateProduction { get; set; }//Дата производства

        [Prop("DateTimePicker", false)]
        public DateTime DateRecharge { get; set; }//Дата перезарядки

        [Prop("NumericUpDownDecimal", false)]
        public double Pressure { get; set; }//Давление

        [Prop("NumericUpDown", false)]
        public int SerialNumber { get; set; }//Заводской номер
        public string NumberSticker { get; set; }//Порядковый номер

        public Extinguisher()
        {
            DateProduction = DateTime.Now;
            DateRecharge = DateProduction.AddYears(5);
        }
    }
}
