using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    [Table("TypeHoses")]
    public class TypeHose : EntityBase, ITypes
    {
        public virtual ICollection<Hose> Hoses { get; set; }

        [Column("Марка")]
        [Control("TextBox", true)]
        public string Name { get; set; }//Марка

        [Column("Производитель")]
        [Control("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель

        [Column("Вид")]
        [Control("TextBox", false)]
        public string Species { get; set; } //Вид

        //[Control("Image", false)]
        //public byte[] Image { get; set; }
        public override EntityBase Parent { get => null; set => throw new System.NotImplementedException("Нельзя назначить родителя"); }

        public TypeHose()
        { }
        public override string ToString()
        {
            return Manufacturer == null ? Name : Name + " (" + Manufacturer + ")";
        }
        public bool EqualsValues(EntityBase obj)
        {
            if (!(obj is TypeHose))
                return false;
            var th = (TypeHose)obj;
            if (Name == th.Name && Manufacturer == th.Manufacturer)
                return true;
            return false;
        }
    }
}
