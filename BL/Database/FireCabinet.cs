using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace BL
{
    [Table("FireCabinets")]
    public class FireCabinet : Equipment,  INumber/*, IPoint*/, ISticker
    {
        public int LocationId { get; set; }
        public int TypeFireCabinetId { get; set; }
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }//Установленные огнетушители
        public virtual ICollection<Hose> Hoses { get; set; }//Установленные рукава
        public virtual ICollection<Hydrant> Hydrants { get; set; }//Установленный пожарный кран
        //public virtual ICollection<FireCabinetHistory> Histories { get; set; }
        
        //public ScalePoint Point { get; set; }

        [Column("Помещение")]
        [Control("ComboBox", true, "Locations", true)]
        public virtual Location Location { get; set; }

        [Column("Тип пожарного шкафа")]
        [Control("ComboBox", true, "TypeFireCabinets")]
        public virtual SpeciesFireCabinet TypeFireCabinet { get; set; }

        [Column("Номер")]
        [Control("NumericUpDown", true)]
        public int Number { get; set; }

        [Column("Повреждение")]
        [Control("CheckBox", false)]
        public bool IsDented { get; set; }//Повреждение корпуса

        [Column("Наклейка")]
        [Control("CheckBox", false)]
        public bool IsSticker { get; set; }//Наличие наклейки

        public FireCabinet()
        {
            IsSticker = true;
            Point = new ScalePoint();
        }
        public override string ToString()
        {
            var sample = Properties.Settings.Default.SampleNameFireCabinets;
            sample = sample.Replace("#L", Location.Number.ToString());
            sample = sample.Replace("#F", Number.ToString());
            return sample;
        }

        public override Hierarchy Parent
        {
            get => Location;
            set
            {
                if (value is Location)
                    Location = (Location)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }

        public override Location GetLocation => Location;
    }
}

