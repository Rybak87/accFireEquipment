using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Вид пожарного шкафа.
    /// </summary>
    [Table("SpeciesFireCabinets")]
    public class KindFireCabinet : KindBase// EntityBase, ITypes
    {
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }

        [Column("Вид")]
        [Control("TextBox", false)]
        public string Species { get; set; } //Вид

        public KindFireCabinet()
        { }
        public override string ToString()
        {
            return Manufacturer == null ? Name : Name + " (" + Manufacturer + ")";
        }
        public override ICollection<EntityBase> Childs { get => FireCabinets.Cast<EntityBase>().ToList(); }
    }
}
