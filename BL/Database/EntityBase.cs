using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    //[Table("EntityBase")]
    public class EntityBase
    {
        public int Id { get; set; }
        //[NotMapped]
        public virtual EntityBase Parent { get; set; }

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

    public class EntityEquipment: EntityBase, INumber
    {
        public virtual new EntityBase Parent { get; set; }

        [Column("Номер")]
        [Control("NumericUpDown", true)]
        public int Number { get; set; }

        public ScalePoint Point { get; set; }
    }
}
