using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    [Table("Hoses")]
    public class Hose : EntityBase, INumber, IPoint
    {
        public int TypeHoseId { get; set; }
        public int FireCabinetId { get; set; }
        public ScalePoint Point { get; set; }

        [Column("Пожарный шкаф")]
        [Control("ComboBox", true, "FireCabinets", true)]
        public virtual FireCabinet FireCabinet { get; set; }

        [Column("Тип рукава")]
        [Control("ComboBox", true, "TypeHoses")]
        public virtual TypeHose TypeHose { get; set; }

        [Column("Номер")]
        [Control("NumericUpDown", true)]
        public int Number { get; set; }

        [Column("Дата производства")]
        [Control("DateTimePicker", false)]
        public DateTime DateProduction { get; set; }//Дата производства

        [Column("Дата перекатки")]
        [Control("DateTimePicker", false)]
        public DateTime DateRolling { get; set; }//Дата перекатки

        //[Column("Марка")]
        //[Control("TextBox", false)]
        //public string Label { get; set; }//Марка

        //[Column("Производитель")]
        //[Control("TextBox", false)]
        //public string Manufacturer { get; set; }//Производитель

        //[Column("Длина")]
        //[Control("NumericUpDownDecimal", false)]
        //public double Length { get; set; }//Длина

        [Column("Повреждения")]
        [Control("CheckBox", false)]
        public bool IsRagged { get; set; }//Наличие дыр

        public Hose()
        {
            DateProduction = DateTime.Now;
            DateRolling = DateProduction.AddYears(1);
            Point = new ScalePoint();
        }
        public override string ToString()
        {
            return $"Рукав № {Number.ToString()}" ;
        }

        //public override object CreateController()
        //{
        //    if (controller == null)
        //        controller = new EntityController<Hose>();
        //    return controller;
        //}
        public override EntityBase Parent
        {
            get => FireCabinet;
            set
            {
                if (value is FireCabinet)
                    FireCabinet = (FireCabinet)value;
                else if (value is TypeHose)
                    TypeHose = (TypeHose)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }
    }
}
