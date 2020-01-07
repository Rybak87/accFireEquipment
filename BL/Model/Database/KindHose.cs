using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Вид рукава.
    /// </summary>
    [Table("SpeciesHoses")]
    public class KindHose : KindBase
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public KindHose()
        { }

        /// <summary>
        /// Коллекция пожарного инвентаря данного типа.
        /// </summary>
        public override ICollection<EntityBase> Childs { get => Hoses.Cast<EntityBase>().ToList(); }

        /// <summary>
        /// Коллекция рукавов данного типа.
        /// </summary>
        public virtual ICollection<Hose> Hoses { get; set; }
    }
}
