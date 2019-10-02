using System.Collections;
using System.Collections.Generic;

namespace BL
{
    public class Location : IEnumerable
    {
        public int LocationId { get; set; }//ID
        [Prop("TextBox", true)]
        public string Name { get; set; }//Название помещения

        [Prop("NumericUpDown", true)]
        public int Number { get; set; }//Название помещения
        public virtual ICollection<FireCabinet> FireCabinets { get; set; }

        [Prop("Image", false)]
        public byte[] Image { get; set; }
        public Location()
        { }
        public override string ToString()
        {
            return Name;
        }

        public IEnumerator GetEnumerator()
        {
            FireCabinets.GetEnumerator();
            return ((IEnumerable)FireCabinets).GetEnumerator();
        }
    }
}
