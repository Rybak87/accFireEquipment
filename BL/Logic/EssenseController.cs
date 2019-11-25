using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Collections;

//namespace BL
//{
//    public class EssenseControl
//    {
//        public BLContext db = new BLContext();
//        public DbSet collEssenses;
//        public object essense;
//        public bool IsLoad
//        {
//            get => collEssenses != null;
//        }

        
//        public Type EssenseType
//        {
//            get => essense.GetType();
//        }
//        public void SetCollEssenses(DbSet collEssenses)
//        {
//            this.collEssenses = collEssenses;
//            this.collEssenses.Load();
//        }
//        public void CreateNewEssense()
//        {
//            essense = (collEssenses.ElementType).GetConstructor(new Type[] { }).Invoke(new object[] { });
//        }
//        public void GetEssense(int id) => essense = collEssenses.Find(id);
//        public void GetEssense(object essense) => this.essense = essense;
//        public void GetEssense(DbSet collEssenses)
//        {

//            //var x = db.GetType().GetProperties().Select(p => p.GetValue(db));//.Where(pv => (IEnumerable)pv. == essense);
//            //foreach (var item in (IEnumerable)x)
//            //{
//            //    if (item is IEnumerable)
//            //        if (((IEnumerable<object>)item).Contains(essense))
//            //            collEssenses = (DbSet<T>)item;
//            //}
//            //this.essense = essense;
//            this.collEssenses = collEssenses;
//        }
//        public void SetPropEssense(Dictionary<PropertyInfo, Control> controlsData)
//        {
//            foreach (var item in controlsData)
//            {
//                switch (GetPropAttribute(item.Key).Control)
//                {
//                    case "TextBox":
//                        {
//                            item.Key.SetValue(essense, item.Value.Text);
//                            break;
//                        }
//                    case "ComboBox":
//                        {
//                            item.Key.SetValue(essense, ((ComboBox)item.Value).SelectedItem);
//                            break;
//                        }
//                    case "CheckBox":
//                        {
//                            item.Key.SetValue(essense, ((CheckBox)item.Value).Checked);
//                            break;
//                        }
//                    case "NumericUpDown":
//                        {
//                            item.Key.SetValue(essense, (int)((NumericUpDown)item.Value).Value);
//                            break;
//                        }
//                    case "NumericUpDownDecimal":
//                        {
//                            item.Key.SetValue(essense, (double)((NumericUpDown)item.Value).Value);
//                            break;
//                        }
//                    case "DateTimePicker":
//                        {
//                            item.Key.SetValue(essense, ((DateTimePicker)item.Value).Value);
//                            break;
//                        }
//                    case null:
//                        break;
//                    default:
//                        throw new ArgumentException();
//                }
//            }

//        }
//        public void AddNewEssense(Dictionary<PropertyInfo, Control> controlsData)
//        {
//            SetPropEssense(controlsData);
//            collEssenses.Add(essense);
//            db.SaveChanges();
//        }
//        public void EditEssense(Dictionary<PropertyInfo, Control> controlsData)
//        {
//            SetPropEssense(controlsData);
//            db.Entry(essense).State = EntityState.Modified;
//            db.SaveChanges();
//        }
//        public void RemoveEssense(int id)
//        {
//            GetEssense(id);
//            collEssenses.Remove(essense);
//            db.SaveChanges();
//        }

//        public List<object> GetListSourse(string name)
//        {
//            var list = new List<object>();

//            foreach (var item in (IEnumerable)db.GetType().GetProperty(name).GetValue(db))
//            {
//                list.Add(item);
//            }
//            return list;
//        }
//        public PropAttribute GetPropAttribute(PropertyInfo pi)
//        {
//            return (PropAttribute)pi.GetCustomAttributes().FirstOrDefault(i => i.GetType() == typeof(PropAttribute));
            
//        }


//    }
//}
