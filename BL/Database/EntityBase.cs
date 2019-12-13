﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;

namespace BL
{
    abstract public class EntityBase
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (ObjectContext.GetObjectType(obj.GetType()) != ObjectContext.GetObjectType(this.GetType()))
                return false;
            else
                if (this.Id == ((EntityBase)obj).Id)
                return true;
            return false;
        }
        public EntitySign GetSign()
        {
            return new EntitySign(GetType(), Id);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(EntityBase e1, object e2)
        {
            return Equals(e1, e2);
        }
        public static bool operator !=(EntityBase e1, object e2)
        {
            return !Equals(e1, e2);
        }

        public new Type GetType() => ObjectContext.GetObjectType(base.GetType());
    }
    //[Table("EquipmentBase")]
    abstract public class EquipmentBase : EntityBase
    {
        [NotMapped]
        abstract public EntityBase Parent { get; set; }
        [NotMapped]
        abstract public Location GetLocation { get; }
        public virtual ICollection<History> Histories { get; set; }
    }
    abstract public class SpeciesBase : EntityBase
    {
        [Column("Марка")]
        [Control("TextBox", true)]
        public string Name { get; set; }//Марка

        [Column("Производитель")]
        [Control("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель
        public bool EqualsValues(SpeciesBase obj)
        {
            if (GetType() != obj.GetType())
                return false;
            var th = obj;
            if (Name == th.Name && Manufacturer == th.Manufacturer)
                return true;
            return false;
        }
        [NotMapped]
        abstract public ICollection<EntityBase> Childs { get; }
    }
}
