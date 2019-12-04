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
            listView.Clear();
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
        private void HydrantsReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            var dict = new Dictionary<Func<EntityBase, bool>, Func<EntityBase, string>>
            {
                [ent => ((Hydrant)ent).IsDamage] = ent => "Поврежден; "
            };
            EntityReport(typeof(Hydrant), dict);

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