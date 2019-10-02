using System.Collections.Generic;

namespace BL
{
    public class FireCabinet
    {
        public int FireCabinetId { get; set; }//ID
        public int LocationId { get; set; }

        [Prop("ComboBox", true, "Locations")]
        public virtual Location Location { get; set; }//Место установки
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }//Установленные огнетушители
        public virtual ICollection<Hose> Hoses { get; set; }//Установленные рукава
        public virtual Hydrant Hydrant { get; set; }//Установленный пожарный кран
        public string NumberSticker { get; set; }//Порядковый номер

        [Prop("CheckBox", false)]
        public bool IsDented { get; set; }//Повреждение корпуса

        [Prop("CheckBox", false)]
        public bool IsSticker { get; set; }//Наличие наклейки

        [Prop("Image", false)]
        public byte[] Image { get; set; }

        public FireCabinet()
        {
            IsSticker = true;
        }
        public override string ToString()
        {
            return FireCabinetId.ToString();
        }
    }
}
