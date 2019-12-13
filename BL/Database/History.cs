using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FireCabinetHistory
    {
        public virtual FireCabinet FireCabinet { get; set; }
        public int FireCabinetId { get; set; }
        public virtual Inspection Inspection { get; set; }
        public int InspectionId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChange { get; set; }
        public FireCabinetHistory()
        {
            DateChange = DateTime.Now;
        }
    }
    [Table("History")]
    public class History:EntityBase
    {
        //public int Id { get; set; }
        public virtual EquipmentBase EquipmentBase { get; set; }
        public int EquipmentBaseId { get; set; }
        //public virtual Inspection Inspection { get; set; }
        //public int InspectionId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChange { get; set; }
        public History()
        {
            DateChange = DateTime.Now;
        }
    }
}
