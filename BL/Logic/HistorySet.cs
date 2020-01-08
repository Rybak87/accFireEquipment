using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BL
{
    /// <summary>
    /// Множество изменений пожарного инвентаря.
    /// </summary>
    public class HistorySet
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="equipment">Пожарный инвентарь.</param>
        public HistorySet(Equipment equipment)
        {
            Properties = GetPropertiesWithControlAttribute(equipment);
            OldValues = GetValues(equipment);
            this.equipment = equipment;
        }

        /// <summary>
        /// Коллекция свойств.
        /// </summary>
        public List<PropertyInfo> Properties { get; }

        /// <summary>
        /// Старые значения.
        /// </summary>
        public List<string> OldValues { get; private set; }

        /// <summary>
        /// Новые значения.
        /// </summary>
        public List<string> NewValues { get; private set; }

        /// <summary>
        /// Пожарный инвентарь.
        /// </summary>
        private Equipment equipment;

        /// <summary>
        /// Возвращает текущие значения свойств пожарного инвентаря.
        /// </summary>
        /// <param name="currEntity">Пожарный инвентарь.</param>
        private List<string> GetValues(Equipment currEntity)
        {
            var result = new List<string>();
            foreach (var pr in Properties)
                result.Add(pr.GetValue(currEntity).ToString());
            return result;
        }

        /// <summary>
        /// Возвращает свойства пожарного инвентаря с атрибутом создания элементов.
        /// </summary>
        /// <param name="entity">Пожарный инвентарь.</param>
        private List<PropertyInfo> GetPropertiesWithControlAttribute(Equipment entity)
        {
            var result = new List<PropertyInfo>();
            foreach (PropertyInfo prop in entity?.GetType().GetProperties())
            {
                var controlAttr = GetControlAttribute(prop);
                if (controlAttr == null)
                    continue;

                result.Add(prop);
            }
            return result;

            
        }

        /// <summary>
        /// Возвращает атрибут создания элементов.
        /// </summary>
        /// <param name="pi">Свойство.</param>
        ControlAttribute GetControlAttribute(PropertyInfo pi)
        {
            foreach (var item in pi.GetCustomAttributes())
                if (item.GetType() == typeof(ControlAttribute))
                    return (ControlAttribute)item;
            return null;
        }

        /// <summary>
        /// Устанавливает старые значения пожарного инвентаря пустыми.
        /// </summary>
        public void SetOldValuesEmpty()
        {
            OldValues = new List<string>(Properties.Count);
            for (int i = 0; i < Properties.Count; i++)
                OldValues.Add("");
        }

        /// <summary>
        /// Устанавливает новые значения.
        /// </summary>
        public void SetNewValues()
        {
            NewValues = GetValues(equipment);
        }

        ///// <summary>
        ///// Сохраняет множество изменений пожарного инвентаря. 
        ///// </summary>
        //public void Save()
        //{
        //    using (var ec = new EntityController())
        //    {
        //        Save(ec);
        //    }
        //}

        /// <summary>
        /// Сохраняет множество изменений пожарного инвентаря. 
        /// </summary>
        /// <param name="ec">Контекст.</param>
        public void AddToDatabase(EntityController ec)
        {
            var datetime = DateTime.Now;
            for (int i = 0; i < OldValues.Count(); i++)
            {
                if (OldValues[i] != NewValues[i])
                {
                    var hy = (History)ec.CreateEntity(typeof(History));
                    hy.EquipmentBase = equipment;
                    hy.Property = Properties[i].Name;
                    hy.DateChange = datetime;
                    hy.NewValue = NewValues[i];
                    ec.AddEntity(hy);
                }
            }
            //ec.SaveChanges();
        }
    }
}
