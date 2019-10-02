using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BL
{
    public class StatusExtinguisher
    {
        [Key]
        [ForeignKey("Extinguisher")]
        public int ExtinguisherId { get; set; }//ID
        public virtual Extinguisher Extinguisher { get; set; }//Огнетушитель
        public bool IsDented { get; set; }//Повреждение корпуса
        public bool IsPaintDamage { get; set; }//Повреждение краски
        public bool IsHose { get; set; }//Наличие шланга
        public bool IsPressureGaugeFault { get; set; }//Неисправность манометра
        public bool IsLabelDamage { get; set; }//Повреждение этикетки

        public StatusExtinguisher()
        {
            IsHose = true;
        }
    }
}
