﻿using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BL
{
    [Table("Hydrants")]
    public class Hydrant : Equipment, INumber//, IPoint
    {
        public int FireCabinetId { get; set; }

        [Column("Пожарный шкаф")]
        [Control("ComboBox", true, "FireCabinets", true)]
        public virtual FireCabinet FireCabinet { get; set; }

        [Column("Номер")]
        [Control("NumericUpDown", true)]
        public int Number { get; set; }

        [Column("Повреждение")]
        [Control("CheckBox", false)]
        public bool IsDamage { get; set; }

        public Hydrant()
        {
            //IsNeed = true;
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
            var sample = Properties.Settings.Default.SampleNameHydrants;
            sample = sample.Replace("#L", ((Location)FireCabinet.Parent).Number.ToString());
            sample = sample.Replace("#F", FireCabinet.Number.ToString());
            sample = sample.Replace("#D", Number.ToString());
            return sample;
        }
        public override Hierarchy Parent
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

        public override Location GetLocation => FireCabinet.Location;
    }
}
