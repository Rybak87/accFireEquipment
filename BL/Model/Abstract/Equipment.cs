using System;
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

        /// <summary>
        /// Возвращает изменения свойств.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<History> AddHistories()
        {
            var properties = Reflection.GetPropertiesWithControlAttribute(GetType());
            var dataChange = DateTime.Now;
            var newHistories = properties.Select(p => new History(this, p.Name, GetLastHistory(p.Name), GetCurrentValue(p), dataChange)).ToList();
            return newHistories.Where(h => h.PrevHistory?.Value != h.Value);
        }

        public IEnumerable<History> CreateHistories()
        {
            var properties = Reflection.GetPropertiesWithControlAttribute(GetType());
            var dataChange = DateTime.Now;
            var newHistories = properties.Select(p => new History(this, p.Name, null, GetCurrentValue(p), dataChange));
            return newHistories;
        }

        public IEnumerable<History> HistoriesOnDate(DateTime dataChange)
        {
            var properties = Reflection.GetPropertiesWithControlAttribute(GetType());
            return properties.Select(p => GetLastHistoryOnDate(p.Name, dataChange));
        }
    }
}
