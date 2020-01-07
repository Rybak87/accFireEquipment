using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Вид пожарного шкафа.
    /// </summary>
    [Table("SpeciesFireCabinets")]
    public class KindFireCabinet : KindBase
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public KindFireCabinet()
        { }

        /// <summary>
        /// Коллекция пожарного инвентаря данного типа.
        /// </summary>
        public override ICollection<EntityBase> Childs { get => FireCabinets.Cast<EntityBase>().ToList(); }

        /// <summary>
        /// Коллекция пожарных шкафов данного типа.
        /// </summary>
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }
    }
}
