﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace BL
{
    /// <summary>
    /// Базовый класс для пожарного инвентаря.
    /// </summary>
    public abstract class Equipment : Hierarchy
    {
        /// <summary>
        /// Родитель.
        /// </summary>
        [NotMapped]
        public abstract Hierarchy Parent { get; set; }

        /// <summary>
        /// Относительная точка.
        /// </summary>
        public ScalePoint Point { get; set; }

        /// <summary>
        /// Коллекция изменений сущности.
        /// </summary>
        public virtual ICollection<History> Histories { get; set; }

        private History GetLastHistory(string propertyName) => Histories?.LastOrDefault(h => h.Property == propertyName);

        private History GetLastHistoryOnDate(string propertyName, DateTime dataChange)
        {
            var modeTime = Properties.Settings.Default.UseTime;
            Func<History, bool> datePicker;
            if (modeTime)
                datePicker = h => h.DateChange <= dataChange;
            else
                datePicker = h => h.DateChange.Date <= dataChange;


            return Histories?.Where(datePicker).LastOrDefault(h => h.Property == propertyName);
        }

        private string GetCurrentValue(PropertyInfo property) => property.GetValue(this).ToString();

        //public string SampleName(string sample)
        //{
        //    var type = this.GetType();
        //    var chars = GetterOfType.GetSampleChars(type);
        //    foreach (var ch in chars)
        //    {
        //        sample = sample.Replace("#" + ch, GetterOfType.charsSampleNaming[ch](this));
        //    }
        //    return sample;
        //}

        /// <summary>
        /// Возвращает изменения свойств.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<History> GetNewHistories()
        {
            var properties = Reflection.GetPropertiesWithControlAttribute(this);
            var dataChange = DateTime.Now;
            var newHistories = properties.Select(p => new History(this, p.Name, GetLastHistory(p.Name), GetCurrentValue(p), dataChange));
            return newHistories.Where(h => h.PrevHistory?.Value != h.Value);
        }

        public IEnumerable<History> GetCreateHistories()
        {
            var properties = Reflection.GetPropertiesWithControlAttribute(this);
            var dataChange = DateTime.Now;
            var newHistories = properties.Select(p => new History(this, p.Name, null, GetCurrentValue(p), dataChange));
            return newHistories;
        }

        public IEnumerable<History> GetHistoriesOnDate(DateTime dataChange)
        {
            var properties = Reflection.GetPropertiesWithControlAttribute(this);
            return properties.Select(p => GetLastHistoryOnDate(p.Name, dataChange));
        }

        public ICollection<History> CloneHistories()
        {
            var cloneHistories = new List<History>(Histories.Count);
            foreach (var hys in Histories)
            {
                var newHistory = new History
                {
                    Id = 0,
                    DateChange = hys.DateChange,
                    //Equipment = this,
                    //EquipmentId = this.Id,
                    Property = hys.Property,
                    Value = hys.Value
                };
            }
            return cloneHistories;
        }
    }
}
