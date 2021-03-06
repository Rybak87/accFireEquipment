﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL
{
    /// <summary>
    /// Базовый класс для видов пожарного инвентаря.
    /// </summary>
    public abstract class KindBase : EntityBase
    {
        /// <summary>
        /// Марка пожарного инвентаря.
        /// </summary>
        [Column("Марка")]
        [Control("TextBox", true, 1)]
        public string Name { get; set; }

        /// <summary>
        /// Производитель пожарного инвентаря.
        /// </summary>
        [Column("Производитель")]
        [Control("TextBox", true, 2)]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Вид.
        /// </summary>
        [Column("Вид")]
        [Control("TextBox", false, 3)]
        public string Kind { get; set; }

        /// <summary>
        /// Коллекция пожарного инвентаря данного типа.
        /// </summary>
        [NotMapped]
        public abstract ICollection<EntityBase> Childs { get; }

        /// <summary>
        /// Возвращает именование.
        /// </summary>
        public override string ToString() => Manufacturer == null ? Name : Name + " (" + Manufacturer + ")";

        /// <summary>
        /// Сравнение типов пожарного инвентаря по типу, марке, производителю.
        /// </summary>
        public bool EqualsValues(KindBase obj)
        {
            if (GetType() != obj.GetType())
                return false;
            var th = obj;
            if (Name == th.Name && Manufacturer == th.Manufacturer)
                return true;
            return false;
        }
    }
}
