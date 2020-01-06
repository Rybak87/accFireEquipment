using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    [Table("SpeciesHoses")]
    public class KindHose : KindBase//EntityBase, ITypes
    {
        public virtual ICollection<Hose> Hoses { get; set; }

        [Column("Вид")]
        [Control("TextBox", false)]
        public string Species { get; set; } //Вид
        public override ICollection<EntityBase> Childs { get => Hoses.Cast<EntityBase>().ToList(); }
        public KindHose()
        { }
        public override string ToString()
        {
            return Manufacturer == null ? Name : Name + " (" + Manufacturer + ")";
        }
    }
}
