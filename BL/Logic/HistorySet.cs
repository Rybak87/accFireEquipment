using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class HistorySet
    {
        public List<PropertyInfo> Properties { get; }
        public List<string> OldValues { get; private set; }
        public List<string> NewValues { get; private set; }
        private Equipment equipment;

        public HistorySet(Equipment equipment)
        {
            Properties = GetProperties(equipment);
            OldValues = GetValues(equipment);
            this.equipment = equipment;
        }
        private List<string> GetValues(Equipment currEntity)
        {
            var result = new List<string>();
            foreach (var pr in Properties)
                result.Add(pr.GetValue(currEntity).ToString());
            return result;
        }

        private List<PropertyInfo> GetProperties(Equipment entity)
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

            ControlAttribute GetControlAttribute(PropertyInfo pi)
            {
                foreach (var item in pi.GetCustomAttributes())
                    if (item.GetType() == typeof(ControlAttribute))
                        return (ControlAttribute)item;
                return null;
            }
        }

        public void SetOldValuesEmpty()
        {
            OldValues = new List<string>(Properties.Count);
            for (int i = 0; i < Properties.Count; i++)
                OldValues.Add("");
        }
        public void SetNewValues()
        {
            NewValues = GetValues(equipment);
        }

        public void Save()
        {
            using (var ec = new EntityController())
                Save(ec);
        }
        public void Save(EntityController ec)
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
                    //hy.OldValue = OldValues[i];
                    hy.NewValue = NewValues[i];
                    ec.AddEntity(hy);
                }
            }
            ec.SaveChanges();
        }
    }
}
