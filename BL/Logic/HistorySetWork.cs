using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BL
{
    /// <summary>
    /// Множество изменений пожарного инвентаря.
    /// </summary>
    public class HistorySetWork
    {
        /// <summary>
        /// Пожарный инвентарь.
        /// </summary>
        private Equipment equipment;

        private List<HistorySet> historySets;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="equipment">Пожарный инвентарь.</param>
        public HistorySetWork(Equipment equipment)
        {
            this.equipment = equipment;
            var properties = Reflection.GetPropertiesWithControlAttribute(equipment);
            historySets = properties.Select((p) => new HistorySet(p)).ToList();
            SetOldValues();
        }

        /// <summary>
        /// Возвращает текущие значения свойств пожарного инвентаря.
        /// </summary>
        /// <param name="currEntity">Пожарный инвентарь.</param>
        private List<string> GetValues(Equipment currEntity) => historySets.Select(hs => hs.property.GetValue(currEntity).ToString()).ToList(); //Properties.Select(p => p.GetValue(currEntity).ToString()).ToList();

        /// <summary>
        /// Устанавливает старые значения пожарного инвентаря пустыми.
        /// </summary>
        public void SetOldValuesEmpty()
        {
            for (int i = 0; i < historySets.Count; i++)
                historySets[i].oldvalue = string.Empty;
        }

        /// <summary>
        /// Устанавливает новые значения.
        /// </summary>
        public void SetNewValues()
        {
            var values = GetValues(equipment);
            for (int i = 0; i < historySets.Count; i++)
                historySets[i].newValue = values[i];
        }

        public void SetOldValues()
        {
            var values = GetValues(equipment);
            for (int i = 0; i < historySets.Count; i++)
                historySets[i].oldvalue = values[i];
        }

        /// <summary>
        /// Сохраняет множество изменений пожарного инвентаря. 
        /// </summary>
        /// <param name="ec">Контекст.</param>
        public void AddToDatabase(EntityController ec)
        {
            var datetime = DateTime.Now;
            var addHistorySet = historySets.Where(hs => hs.oldvalue != hs.newValue);
            foreach (var hs in addHistorySet)
            {
                var hy = ec.CreateEntity<History>();
                hy.EquipmentBase = equipment;
                hy.Property = hs.property.Name;
                hy.NewValue = hs.newValue;
                hy.DateChange = datetime;
                ec.AddEntity(hy);
            }
            ec.SaveChanges();
        }
    }

    public class HistorySet
    {
        public PropertyInfo property;
        public string oldvalue;
        public string newValue;

        public HistorySet(PropertyInfo property)
        {
            //this.oldvalue = string.Empty;
            //this.newValue = string.Empty;
            this.property = property;
        }
    }
}
