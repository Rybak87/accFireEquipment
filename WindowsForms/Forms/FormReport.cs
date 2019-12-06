using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormReport : Form
    {
        public event Action<EntitySign> EditEntity;
        public FormReport()
        {
            InitializeComponent();
            FullMenu.Image = ImageSettings.IconsImage(typeof(Location));
            FireCabinetsMenu.Image = ImageSettings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));
            HosesMenu.Image = ImageSettings.IconsImage(typeof(Hose));
            HydrantsMenu.Image = ImageSettings.IconsImage(typeof(Hydrant));
            RechargeExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));

            FullMenu.Click += (s, e) => FullReport();
            FireCabinetsMenu.Click += (s, e) => FireCabinetsReport();
            ExtinguishersMenu.Click += (s, e) => ExtinguishersReport();
            HosesMenu.Click += (s, e) => HosesReport();
            HydrantsMenu.Click += (s, e) => HydrantsReport();
            RechargeExtinguishersMenu.Click += (s, e) => RechargeExtinguishersReport();
        }
        private void FullReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.ToString()
            };
            var dict1 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.ToString()
            };
            var dict2 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((FireCabinet)ent).IsDented] = ent => "Поврежден; ",
                [ent => !((FireCabinet)ent).IsSticker] = ent => "Без наклейки; "
            };
            EntityReport2(typeof(FireCabinet), ent => ent.Parent.ToString(), 3, dict, dict1, dict2);

            dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.ToString()
            };
            dict1 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.Parent.ToString()
            };
            dict2 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Extinguisher)ent).IsDented] = ent => "Поврежден; ",
                [ent => !((Extinguisher)ent).IsSticker] = ent => "Без наклейки; ",
                [ent => !((Extinguisher)ent).IsHose] = ent => "Нет шланга; ",
                [ent => ((Extinguisher)ent).IsLabelDamage] = ent => "Повреждена этикетка; ",
                [ent => ((Extinguisher)ent).IsPaintDamage] = ent => "Повреждена краска; ",
                [ent => ((Extinguisher)ent).IsPressureGaugeFault] = ent => "Поврежден манометр; ",
                [ent => ((Extinguisher)ent).Pressure < 4] = ent => "Давление менее 4; ",
                [ent => ((Extinguisher)ent).Weight < 5] = ent => "Вес менее 5; "
            };
            EntityReport2(typeof(Extinguisher), ent => ent.Parent.Parent.ToString(), 3, dict, dict1, dict2);

            dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.ToString()
            };
            dict1 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.Parent.ToString()
            };
            dict2 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Hose)ent).IsRagged] = ent => "Поврежден; ",
                [ent => ((Hose)ent).DateRolling.Subtract(DateTime.Now).Days < 30] = ent => "Необходима перекатка; "
            };
            EntityReport2(typeof(Hose), ent => ent.Parent.Parent.ToString(), 3, dict, dict1, dict2);

            dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.ToString()
            };
            dict1 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.Parent.ToString()
            };
            dict2 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Hydrant)ent).IsDamage] = ent => "Поврежден; "
            };
            EntityReport2(typeof(Hydrant), ent => ent.Parent.Parent.ToString(), 3, dict, dict1, dict2);
        }
        private void FireCabinetsReport()
        {
            InitColumns("Тип", "Недостатки");
            var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((FireCabinet)ent).IsDented] = ent => "Поврежден; ",
                [ent => !((FireCabinet)ent).IsSticker] = ent => "Без наклейки; "
            };
            EntityReport(typeof(FireCabinet), dict);
        }
        private void ExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Extinguisher)ent).IsDented] = ent => "Поврежден; ",
                [ent => !((Extinguisher)ent).IsSticker] = ent => "Без наклейки; ",
                [ent => !((Extinguisher)ent).IsHose] = ent => "Нет шланга; ",
                [ent => ((Extinguisher)ent).IsLabelDamage] = ent => "Повреждена этикетка; ",
                [ent => ((Extinguisher)ent).IsPaintDamage] = ent => "Повреждена краска; ",
                [ent => ((Extinguisher)ent).IsPressureGaugeFault] = ent => "Поврежден манометр; ",
                [ent => ((Extinguisher)ent).Pressure < 4] = ent => "Давление менее 4; ",
                [ent => ((Extinguisher)ent).Weight < 5] = ent => "Вес менее 5; "
            };
            EntityReport(typeof(Extinguisher), dict);
        }
        private void HosesReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Hose)ent).IsRagged] = ent => "Поврежден; ",
                [ent => ((Hose)ent).DateRolling.Subtract(DateTime.Now).Days < 30] = ent => "Необходима перекатка; "
            };
            EntityReport(typeof(Hose), dict);
        }
        //private void HydrantsReport()
        //{
        //    InitColumns("Тип", "Пожарный шкаф", "Недостатки");
        //    var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
        //    {
        //        [ent => ((Hydrant)ent).IsDamage] = ent => "Поврежден; "
        //    };
        //    EntityReport(typeof(Hydrant), dict);

        //}
        private void HydrantsReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.ToString()
            };
            var dict1 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = ent => ent.Parent.ToString()
            };
            var dict2 = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Hydrant)ent).IsDamage] = ent => "Поврежден; "
            };
            EntityReport2(typeof(Hydrant), ent => ent.Parent.Parent.ToString(), 3, dict, dict1, dict2);
            //var filter0 = new Filter(ent => true, ent => ent.Parent.Parent.ToString());
            //var filter1 = new FilterSet(ent => true, ent => ent.ToString());
            //var filter2 = new FilterSet(ent => true, ent => ent.Parent.ToString());
            //var filter3 = new FilterSet(ent => ((Hydrant)ent).IsDamage, ent => "Поврежден; ", true);
            //EntityReport3(typeof(Hydrant), filter0, filter1, filter2, filter3);
        }
        private void EntityReport(Type type, Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>> dict)
        {
            using (var ec = new EntityController())
            {
                var full = ec.GetTableList(type);
                if (type == typeof(FireCabinet))
                    full = full.OrderBy(f => f.Parent.ToString()).ToList();
                else
                    full = full.OrderBy(f => f.Parent.Parent.ToString()).ToList();
                ListViewGroup group;
                foreach (var entity in full)
                {
                    string fault = "";
                    bool IsAdd = false;
                    foreach (var item in dict)
                    {
                        if (item.Key(entity))
                        {
                            fault += item.Value(entity);
                            IsAdd = true;
                        }
                    }
                    if (IsAdd)
                    {
                        group = GetGroup(entity);
                        var item = GetItem(entity, group, fault);
                        listView.Items.Add(item);
                    }
                }
            }
            ListViewGroup GetGroup(EntityBase entity)
            {
                string name;
                if (entity is FireCabinet)
                    name = entity.Parent.ToString();
                else
                    name = entity.Parent.Parent.ToString();

                ListViewGroup group;
                if (listView.Groups[name] == null)
                {
                    group = new ListViewGroup(name);
                    group.Name = name;
                    listView.Groups.Add(group);
                }
                else
                    group = listView.Groups[name];
                return group;
            }
            ListViewItem GetItem(EntityBase entity, ListViewGroup group, string fault)
            {
                var item = new ListViewItem(entity.ToString(), group);
                item.Tag = entity.GetSign();
                ListViewItem.ListViewSubItem[] subItems;
                if (entity is FireCabinet)
                    subItems = new ListViewItem.ListViewSubItem[]
                        {
                            new ListViewItem.ListViewSubItem(item, fault)
                        };
                else
                    subItems = new ListViewItem.ListViewSubItem[]
                        {
                            new ListViewItem.ListViewSubItem(item, entity.Parent.ToString()),
                            new ListViewItem.ListViewSubItem(item, fault)
                        };
                item.SubItems.AddRange(subItems);
                return item;
            }
        }
        private void EntityReport2(Type type, Func<EntityBase, string> nameGroups, int requiredDict, params Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>[] dicts)
        {
            if (dicts == null)
                return;//////////
            var list = dicts.ToList();
            var newDict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => true] = nameGroups
            };
            list.Insert(0, newDict);
            var res = CreateResultStringsTable(type, requiredDict, list);
            var uniqueGroupNames = res.Select(arr => arr[0]).Distinct().OrderBy(n => n);
            foreach (var name in uniqueGroupNames)
                if (listView.Groups[name] == null)
                    listView.Groups.Add(new ListViewGroup(name, name));
            foreach (var str in res)
            {
                var nameGroup = listView.Groups[str[0]];
                var item = new ListViewItem(str[1], nameGroup);
                //item.Tag =
                var countSbItems = dicts.Length - 1;
                var subItems = new ListViewItem.ListViewSubItem[countSbItems];
                for (int i = 0; i < countSbItems; i++)
                {
                    var newSubitem = new ListViewItem.ListViewSubItem(item, str[i + 2]);
                    subItems[i] = newSubitem;
                }
                item.SubItems.AddRange(subItems);
                listView.Items.Add(item);
            }
        }
        //private void EntityReport3(Type type, Filter nameGroups, params FilterSet[] filterSet)
        //{
        //    if (filterSet == null)
        //        return;//////////
        //    IEnumerable<string> uniqueGroupNames;
        //    using (var ec = new EntityController())
        //    {
        //        uniqueGroupNames = FilterWork.CreateResultStringsTable(type, new FilterSet(nameGroups)).Select(arr=>arr[0]);
        //    }
        //    var res = FilterWork.CreateResultStringsTable(type, filterSet);
        //    foreach (var name in uniqueGroupNames)
        //        if (listView.Groups[name] == null)
        //            listView.Groups.Add(new ListViewGroup(name, name));
        //    foreach (var str in res)
        //    {
        //        var nameGroup = listView.Groups[str[0]];
        //        var item = new ListViewItem(str[1], nameGroup);
        //        //item.Tag =
        //        var countSbItems = filterSet.Length - 1;
        //        var subItems = new ListViewItem.ListViewSubItem[countSbItems];
        //        for (int i = 0; i < countSbItems; i++)
        //        {
        //            var newSubitem = new ListViewItem.ListViewSubItem(item, str[i + 2]);
        //            subItems[i] = newSubitem;
        //        }
        //        item.SubItems.AddRange(subItems);
        //        listView.Items.Add(item);
        //    }
        //}
        private string CreateResultString(EntityBase entity, Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>> dict)
        {
            string result = string.Empty;
            foreach (var item in dict)
            {
                if (item.Key(entity))
                {
                    result += item.Value(entity);
                }
            }
            return result;
        }
        private string[] CreateResultStrings(EntityBase entity, int requiredDict, List<Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>> dicts)
        {
            var countDicts = dicts.Count();
            string[] result = new string[countDicts];
            bool isEmpty = true;
            for (int i = 0; i < countDicts; i++)
            {
                var str = CreateResultString(entity, dicts[i]);
                result[i] = CreateResultString(entity, dicts[i]);
                if (requiredDict == i && str != string.Empty)
                    isEmpty = false;
            }
            if (isEmpty)
                return null;
            else
                return result;
        }
        private List<string[]> CreateResultStringsTable(Type type, int requiredDict, List<Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>> dicts)
        {
            List<string[]> result = new List<string[]>();
            using (var ec = new EntityController())
            {
                var full = ec.GetTableList(type);
                foreach (var ent in full)
                {
                    var str = CreateResultStrings(ent, requiredDict, dicts);
                    if (str != null)
                        result.Add(str);
                }
            }
            return result;
        }
        private void RechargeExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Оставшийся срок (в месяцах)");
            var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Extinguisher)ent).DateRecharge.Subtract(DateTime.Now).Days < 365] = ent => ((Extinguisher)ent).DateRecharge.SubtractMonths(DateTime.Now).ToString()
            };
            EntityReport(typeof(Extinguisher), dict);
        }
        private void InitColumns(params string[] columnsNames)
        {
            listView.Clear();
            listView.Groups.Clear();
            var countColumns = columnsNames.Count();
            var columnWidth = Width / countColumns - 10;
            var columnHeaders = new ColumnHeader[countColumns];
            int count = 0;
            foreach (var name in columnsNames)
            {
                var columnHeader = new ColumnHeader()
                {
                    Text = name,
                    Width = columnWidth
                };
                columnHeaders[count] = columnHeader;
                count++;
            }
            listView.Columns.AddRange(columnHeaders);
        }
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var item = listView.SelectedItems[0];
            var sign = (EntitySign)item.Tag;
            EditEntity?.Invoke(sign);
        }
    }
}