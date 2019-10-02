using System.Collections.Generic;

namespace BL
{
    public class TypeHose
    {
        public int TypeHoseId { get; set; }
        public virtual ICollection<Hose> Hoses { get; set; }

        [Prop("TextBox", false)]
        public string Species { get; set; } //Вид

        [Prop("TextBox", false)]
        public string Label { get; set; }//Марка

        [Prop("TextBox", false)]
        public string Manufacturer { get; set; }//Производитель

        [Prop("Image", false)]
        public byte[] Image { get; set; }
        public TypeHose()
        { }
        public override string ToString()
        {
            return TypeHoseId.ToString();
        }
    }
}
