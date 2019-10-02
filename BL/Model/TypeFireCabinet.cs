using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TypeFireCabinet
    {
        public int TypeFireCabinetId { get; set; }
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }

        [Prop("TextBox", false)]
        public string Species { get; set; } //Вид

        [Prop("TextBox", false)]
        public string Label { get; set; }//Марка

        [Prop("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель

        [Prop("Image", false)]
        public byte[] Image { get; set; }
        public TypeFireCabinet()
        { }
        public override string ToString()
        {
            return Label;
        }
    }
}
