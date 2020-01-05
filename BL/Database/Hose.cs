using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    [Table("Hoses")]
    public class Hose : Equipment, INumber//, IPoint
    {
        public int TypeHoseId { get; set; }
        public int FireCabinetId { get; set; }

        [Column("Пожарный шкаф")]
        [Control("ComboBox", true, "FireCabinets", true)]
        public virtual FireCabinet FireCabinet { get; set; }

        [Column("Тип рукава")]
        [Control("ComboBox", true, "TypeHoses")]
        public virtual SpeciesHose TypeHose { get; set; }

        [Column("Номер")]
        [Control("NumericUpDown", true)]
        public int Number { get; set; }

        [Column("Дата производства")]
        [Control("DateTimePicker", false)]
        public DateTime DateProduction { get; set; }//Дата производства

        [Column("Дата перекатки")]
        [Control("DateTimePicker", false)]
        public DateTime DateRolling { get; set; }//Дата перекатки

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
            var sample = Properties.Settings.Default.SampleNameHoses;
            sample = sample.Replace("#L", ((Location)FireCabinet.Parent).Number.ToString());
            sample = sample.Replace("#F", FireCabinet.Number.ToString());
            sample = sample.Replace("#H", Number.ToString());
            return sample;
        }

        public override Hierarchy Parent
        {
            get => FireCabinet;
            set
            {
                if (value is FireCabinet)
                    FireCabinet = (FireCabinet)value;
                //else if (value is SpeciesHose)
                //    TypeHose = (SpeciesHose)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }

        public override Location GetLocation => FireCabinet.Location;
    }
}
