using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Collections;

namespace BL
{
    public class newEssenseControl<T> where T : class, new()
    {
        DbSet<T> table;
        public BLContext db = new BLContext();

        public T currEssense;
        public newEssenseControl()
        {
        }
        public Type EssenseType
        {
            get => typeof(T);
        }
        public void xxx()
        {
            var ggg = table.Create();
        }
        public void CreateEssense()
        {
            currEssense = new T();
            var currEssense2 = (typeof(T)).GetConstructor(new Type[] { }).Invoke(new object[] { });
        }
        public void GetEssense(int id) => currEssense = table.Find(id);
        public void GetEssense(T essense) => currEssense = essense;
        public void SetPropEssense(Dictionary<PropertyInfo, Control> dictControlsData)
        {
            foreach (var item in dictControlsData)
            {
                switch (GetPropAttribute(item.Key).Control)
                {
                    case "TextBox":
                        {
                            item.Key.SetValue(currEssense, item.Value.Text);
                            break;
                        }
                    case "ComboBox":
                        {
                            item.Key.SetValue(currEssense, ((ComboBox)item.Value).SelectedItem);
                            break;
                        }
                    case "CheckBox":
                        {
                            item.Key.SetValue(currEssense, ((CheckBox)item.Value).Checked);
                            break;
                        }
                    case "NumericUpDown":
                        {
                            item.Key.SetValue(currEssense, (int)((NumericUpDown)item.Value).Value);
                            break;
                        }
                    case "NumericUpDownDecimal":
                        {
                            item.Key.SetValue(currEssense, (double)((NumericUpDown)item.Value).Value);
                            break;
                        }
                    case "DateTimePicker":
                        {
                            item.Key.SetValue(currEssense, ((DateTimePicker)item.Value).Value);
                            break;
                        }
                    case null:
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

        }
        public void AddNewEssense(Dictionary<PropertyInfo, Control> controlsData)
        {
            CreateEssense();
            SetPropEssense(controlsData);
            db.Set<T>().Attach(currEssense);
            db.SaveChanges();
        }
        public void EditEssense(Dictionary<PropertyInfo, Control> controlsData)
        {
            SetPropEssense(controlsData);
            db.Entry(currEssense).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void RemoveEssense(int id)
        {
            table.Remove(table.Find(id));
            db.SaveChanges();
        }

        public List<object> GetListSourse(string name)
        {
            var list = new List<object>();

            foreach (var item in (IEnumerable)db.GetType().GetProperty(name).GetValue(db))
            {
                list.Add(item);
            }
            return list;
        }
        public PropAttribute GetPropAttribute(PropertyInfo pi)
        {
            return (PropAttribute)pi.GetCustomAttributes().FirstOrDefault(i => i.GetType() == typeof(PropAttribute));
        }


    }
}
