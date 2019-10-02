using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BL
{
    public class Hydrant
    {
        [Key]
        [ForeignKey("FireCabinet")]
        public int FireCabinetId { get; set; }//ID

        [Prop("ComboBox", true, "FireCabinets")]
        public virtual FireCabinet FireCabinet { get; set; }//Шкаф

        [Prop("CheckBox", false)]
        public bool IsDamage { get; set; }

        [Prop("CheckBox", false)]
        public bool IsNeed { get; set; }
        public Hydrant()
        {
            IsNeed = true;
        }
    }
}
