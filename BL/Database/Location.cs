using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;

namespace BL
{
    [Table("Locations")]
    public class Location : EntityBase, INumber
    {
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }
        [Column ("Номер")]
        [Control("NumericUpDown", true)]

        public int Number { get; set; }
        [Column("Имя")]
        [Control("TextBox", true)]

        public string Name { get; set; }
        [Column("Изображение")]
        [Control("Image", false)]
        //public virtual ImageLocation Image { get; set; }
        public virtual byte[] Image { get; set; }

        public Location()
        { }
        public override string ToString()
        {
            return Name;
        }
        public override EntityBase Parent { get => null; set => Parent = value;}
    }
}
