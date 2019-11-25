using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    [Table("Extinguishers")]
    public class Extinguisher : EntityBase, INumber, IPoint
    {
        public int FireCabinetId { get; set; }
        public int TypeExtinguisherId { get; set; }
        //public virtual StatusExtinguisher Status { get; set; }//Статус
        public ScalePoint Point { get; set; }

        [Column("Пожарный шкаф")]
        [Control("ComboBox", true, "FireCabinets", true)]
        public virtual FireCabinet FireCabinet { get; set; }

        [Column("Тип огнетушителя")]
        [Control("ComboBox", true, "TypeExtinguishers")]
        public virtual TypeExtinguisher TypeExtinguisher { get; set; }

        [Column("Номер")]
        [Control("NumericUpDown", true)]
        public int Number { get; set; }

        [Column("Дата производства")]
        [Control("DateTimePicker", false)]
        public DateTime DateProduction { get; set; }//Дата производства

        [Column("Дата следующей перезарядки")]
        [Control("DateTimePicker", false)]
        public DateTime DateRecharge { get; set; }//Дата перезарядки

        [Column("Масса")]
        [Control("NumericUpDownDecimal", false)]
        public double Weight { get; set; }//Масса

        [Column("Давление")]
        [Control("NumericUpDownDecimal", false)]
        public double Pressure { get; set; }//Давление

        [Column("Заводской номер")]
        [Control("NumericUpDown", false)]
        public int SerialNumber { get; set; }//Заводской номер

        [Column("Повреждение корпуса")]
        [Control("CheckBox", false)]
        public bool IsDented { get; set; }//Повреждение корпуса

        [Column("Наклейка")]
        [Control("CheckBox", false)]
        public bool IsSticker { get; set; }//Наличие наклейки

        [Column("Повреждение краски")]
        [Control("CheckBox", false)]
        public bool IsPaintDamage { get; set; }//Повреждение краски

        [Column("Повреждение ЗПУ")]
        [Control("CheckBox", false)]
        public bool IsHandleDamage { get; set; }//Повреждение ЗПУ

        [Column("Наличие шланга")]
        [Control("CheckBox", false)]
        public bool IsHose { get; set; }//Наличие шланга

        [Column("Неисправность манометра")]
        [Control("CheckBox", false)]
        public bool IsPressureGaugeFault { get; set; }//Неисправность манометра

        [Column("Повреждение этикетки")]
        [Control("CheckBox", false)]
        public bool IsLabelDamage { get; set; }//Повреждение этикетки
        public override EntityBase Parent
        {
            get => FireCabinet;
            set
            {
                if (value is FireCabinet)
                    FireCabinet = (FireCabinet)value;
                else if (value is TypeExtinguisher)
                    TypeExtinguisher = (TypeExtinguisher)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }

        //public string NumberSticker { get; set; }//Порядковый номер

        public Extinguisher()
        {
            DateProduction = DateTime.Now;
            DateRecharge = DateProduction.AddYears(5);
            IsHose = true;
            IsSticker = true;
            Point = new ScalePoint();
        }

        public override string ToString()
        {
            return $"Огнетушитель № {Number.ToString()}";
        }
    }
}
