using System;

namespace BL
{

    public class Hose
    {
        public int HoseId { get; set; }
        public int TypeHoseId{ get; set; }
        public int FireCabinetId { get; set; }

        [Prop("ComboBox", true, "TypeHoses")]
        public virtual TypeHose TypeHose { get; set; }

        [Prop("ComboBox", true, "FireCabinets")]
        public virtual FireCabinet FireCabinet { get; set; }//Шкаф

        [Prop("DateTimePicker", false)]
        public DateTime DateProduction { get; set; }//Дата производства

        [Prop("TextBox", false)]
        public string Label { get; set; }//Марка

        [Prop("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель

        [Prop("NumericUpDownDecimal", false)]
        public double Length { get; set; }//Длина

        [Prop("CheckBox", false)]
        public bool IsRagged { get; set; }//Наличие дыр

        public Hose(FireCabinet fireCabinet, TypeHose typeHose)
        {
            FireCabinet = fireCabinet;
            TypeHose = typeHose;
        }
        public Hose()
        {
            DateProduction = DateTime.Now;
        }
    }
}
