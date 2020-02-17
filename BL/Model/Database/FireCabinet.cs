using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace BL
{
    /// <summary>
    /// Пожарный шкаф.
    /// </summary>
    [Table("ПожарныеШкафы")]
    public class FireCabinet : Equipment, ISticker
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public FireCabinet()
        {
            IsSticker = true;
            Point = new ScalePoint();
        }

        /// <summary>
        /// Родитель.
        /// </summary>
        public override Hierarchy Parent
        {
            get => Location;
            set
            {
                if (value is Location)
                    Location = (Location)value;
                else
                    throw new Exception("Нельзя преобразовать object");
            }
        }

        /// <summary>
        /// Возвращает родительское помещение.
        /// </summary>
        public override Location GetLocation => Location;

        /// <summary>
        /// Первичный ключ вида пожарного шкафа
        /// </summary>
        public int KindFireCabinetId { get; set; }

        /// <summary>
        /// Вид пожарного шкафа.
        /// </summary>
        [Column("Тип пожарного шкафа")]
        [Copying]
        [Control("ComboBox", true, 1)]
        public virtual KindFireCabinet KindFireCabinet { get; set; }

        /// <summary>
        /// Повреждение корпуса
        /// </summary>
        [Column("Повреждение")]
        [Copying]
        [Control("CheckBox", false, 3)]
        public bool IsDented { get; set; }

        /// <summary>
        /// Наличие наклейки
        /// </summary>
        [Column("Наклейка")]
        [Copying]
        [Control("CheckBox", false, 4)]
        public bool IsSticker { get; set; }

        /// <summary>
        /// Первичный ключ помещения.
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Помещение.
        /// </summary>
        [Column("Помещение")]
        [Copying]
        public virtual Location Location { get; set; }

        /// <summary>
        /// Установленные огнетушители.
        /// </summary>
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }

        /// <summary>
        /// Установленные рукава.
        /// </summary>
        public virtual ICollection<Hose> Hoses { get; set; }

        /// <summary>
        /// Установленные пожарные шкафы.
        /// </summary>
        public virtual ICollection<Hydrant> Hydrants { get; set; }

        /// <summary>
        /// Возвращает именование в соответствии с шаблоном.
        /// </summary>
        public override string ToString() => GetterOfType.GetName(this);
    }
}

