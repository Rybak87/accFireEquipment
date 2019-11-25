using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    [Table("TypeFireCabinets")]
    public class TypeFireCabinet : EntityBase, ITypes
    {
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }

        [Column("Марка")]
        [Control("TextBox", true)]
        public string Name { get; set; }//Марка

        [Column("Вид")]
        [Control("TextBox", false)]
        public string Species { get; set; } //Вид

        [Column("Производитель")]
        [Control("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель

        //[Control("Image", false)]
        //public byte[] Image { get; set; }
        public override EntityBase Parent { get => null; set => throw new System.NotImplementedException("Нельзя назначить родителя"); }

        public TypeFireCabinet()
        { }

        //public override object CreateController()
        //{
        //    if (controller == null)
        //        controller = new EntityController<TypeFireCabinet>();
        //    return controller;
        //}
        public override string ToString()
        {
            return Manufacturer == null ? Name : Name + " (" + Manufacturer + ")";
        }
    }
}
