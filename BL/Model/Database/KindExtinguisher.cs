﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Вид огнетушителя.
    /// </summary>
    [Table("Виды огнетушителей")]
    public class KindExtinguisher : KindBase
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public KindExtinguisher()
        { }

        /// <summary>
        /// Коллекция пожарного инвентаря данного типа.
        /// </summary>
        public override ICollection<EntityBase> Childs { get => Extinguishers.Cast<EntityBase>().ToList(); }

        /// <summary>
        /// Коллекция огнетушителей данного типа.
        /// </summary>
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }

        /// <summary>
        /// Номинальная масса.
        /// </summary>
        [Column("Номинальная масса")]
        [Control("NumericUpDownDecimal", false, 4)]
        public double NominalWeight { get; set; }

        /// <summary>
        /// Номинальное давление.
        /// </summary>
        [Column("Номинальное давление")]
        [Control("NumericUpDownDecimal", false, 5)]
        public double NominalPressure { get; set; }

        /// <summary>
        /// Минимально допустимая масса.
        /// </summary>
        [Column("Минимальная масса")]
        [Control("NumericUpDownDecimal", false, 6)]
        public double MinWeight { get; set; }

        /// <summary>
        /// Минимально допустимок давление.
        /// </summary>
        [Column("Минимальное давление")]
        [Control("NumericUpDownDecimal", false, 7)]
        public double MinPressure { get; set; }

        /// <summary>
        /// Масса ОТВ.
        /// </summary>
        [Column("Масса ОТВ")]
        [Control("NumericUpDownDecimal", false, 8)]
        public double WeightExtinguishingAgent { get; set; }

        /// <summary>
        /// Марка ОТВ.
        /// </summary>
        [Column("Марка ОТВ")]
        [Control("TextBox", false, 9)]
        public string BrandExtinguishingAgent { get; set; }

        /// <summary>
        /// Объём.
        /// </summary>
        [Column("Объем")]
        [Control("NumericUpDownDecimal", false, 10)]
        public double Volume { get; set; }

        /// <summary>
        /// Время выхода ОТВ.
        /// </summary>
        [Column("Время выхода ОТВ")]
        [Control("NumericUpDownDecimal", false, 11)]
        public double OutputTime { get; set; }

        /// <summary>
        /// Длина струи.
        /// </summary>
        [Column("Длина струи")]
        [Control("NumericUpDownDecimal", false, 12)]
        public double LengthStream { get; set; }

        /// <summary>
        /// Класс пожара.
        /// </summary>
        [Column("Класс пожара")]
        [Control("TextBox", false, 13)]
        public string FireClass { get; set; }
    }
}
