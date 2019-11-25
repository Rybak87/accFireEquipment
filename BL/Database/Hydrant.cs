using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BL
{
    [Table("Hydrants")]
    public class Hydrant : EntityBase, INumber, IPoint
    {
        public int FireCabinetId { get; set; }
        public ScalePoint Point { get; set; }

        [Column("Пожарный шкаф")]
        [Control("ComboBox", true, "FireCabinets", true)]
        public virtual FireCabinet FireCabinet { get; set; }

        [Column("Номер")]
        [Control("NumericUpDown", true)]
        public int Number { get; set; }

        [Column("Повреждение")]
        [Control("CheckBox", false)]
        public bool IsDamage { get; set; }


        [Control("CheckBox", false)]
        public bool IsNeed { get; set; }

        public Hydrant()
        {
            IsNeed = true;
            Point = new ScalePoint();
        }

        //public override object CreateController()
        //{
        //    if (controller == null)
        //        controller = new EntityController<Hydrant>();
        //    return controller;
        //}
        public override string ToString()
        {
            return $"Пожарный кран № {Number}";
        }
        public override EntityBase Parent
        {
            get => FireCabinet;
            set
            {
                if (value is FireCabinet)
                    FireCabinet = (FireCabinet)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }
    }
}
