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
        public FormReport()
        {
            InitializeComponent();
            FullMenu.Image = Settings.IconsImage(typeof(Location));
            FireCabinetsMenu.Image = Settings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = Settings.IconsImage(typeof(Extinguisher));
            HosesMenu.Image = Settings.IconsImage(typeof(Hose));
            HydrantsMenu.Image = Settings.IconsImage(typeof(Hydrant));
            RechargeExtinguishersMenu.Image = Settings.IconsImage(typeof(Extinguisher));

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

            using (var ec = new EntityController())
            {
                var full = ec.FireCabinets.ToList();
                var selective = full.Where(f => f.IsDented || !f.IsSticker).ToList();
                var locationNames = selective.Select(e => e.Parent.ToString()).Distinct();

                foreach (var name in locationNames)
                {
                    var group = new ListViewGroup(name);
                    var selectiveThisParent = selective.Where(e => e.Parent.ToString() == name);
                    foreach (var fc in selectiveThisParent)
                    {
                        var item = new ListViewItem(fc.ToString(), group);
                        item.Tag = fc.GetSign();
                        string fault = "";
                        if (fc.IsDented)
                            fault += "Поврежден; ";
                        if (!fc.IsSticker)
                            fault += "Без наклейки; ";
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, fault));
                        listView.Items.Add(item);
                    }
                    listView.Groups.Add(group);
                }
            }
        }
        private void ExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            using (var ec = new EntityController())
            {
                var full = ec.Extinguishers.ToList();
                var selective = full.Where(e => e.IsDented || !e.IsSticker || !e.IsHose || e.IsLabelDamage || e.IsPaintDamage || e.IsPressureGaugeFault || e.Pressure < 4 || e.Weight < 5).ToList();
                var locationNames = selective.Select(e => e.Parent.Parent.ToString()).Distinct();

                foreach (var name in locationNames)
                {
                    var group = new ListViewGroup(name);
                    var selectiveThisParent = selective.Where(e => e.Parent.Parent.ToString() == name);
                    foreach (var ex in selectiveThisParent)
                    {
                        var item = new ListViewItem(ex.ToString(), group);
                        item.Tag = ex.GetSign();
                        string fault = "";
                        if (ex.IsDented)
                            fault += "Поврежден; ";
                        if (!ex.IsSticker)
                            fault += "Без наклейки; ";
                        if (!ex.IsHose)
                            fault += "Нет шланга; ";
                        if (ex.IsLabelDamage)
                            fault += "Повреждена этикетка; ";
                        if (ex.IsPaintDamage)
                            fault += "Повреждена краска; ";
                        if (ex.IsPressureGaugeFault)
                            fault += "Поврежден манометр; ";
                        if (ex.Pressure < 4)
                            fault += "Давление менее 4; ";
                        if (ex.Weight < 5)
                            fault += "Вес менее 5; ";
                        var subItems = new ListViewItem.ListViewSubItem[]
                            { new ListViewItem.ListViewSubItem(item, ex.Parent.ToString()),
                              new ListViewItem.ListViewSubItem(item, fault)};
                        item.SubItems.AddRange(subItems);
                        listView.Items.Add(item);
                    }
                    listView.Groups.Add(group);
                }
            }
        }
        private void HosesReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");

            using (var ec = new EntityController())
            {
                var full = ec.Hoses.ToList();
                var selective = full.Where(h => h.IsRagged || h.DateRolling.Subtract(DateTime.Now).Days < 30).ToList();
                var locationNames = selective.Select(e => e.Parent.Parent.ToString()).Distinct();

                foreach (var name in locationNames)
                {
                    var group = new ListViewGroup(name);
                    var selectiveThisParent = selective.Where(e => e.Parent.Parent.ToString() == name);
                    foreach (var hose in selectiveThisParent)
                    {
                        var item = new ListViewItem(hose.ToString(), group);
                        item.Tag = hose.GetSign();
                        string fault = "";
                        if (hose.IsRagged)
                            fault += "Порван; ";
                        if (hose.DateRolling.Subtract(DateTime.Now).Days < 30)
                            fault += "Необходима перекатка; ";
                        var subItems = new ListViewItem.ListViewSubItem[]
                            { new ListViewItem.ListViewSubItem(item, hose.Parent.ToString()),
                              new ListViewItem.ListViewSubItem(item, fault)};
                        item.SubItems.AddRange(subItems);
                        listView.Items.Add(item);
                    }
                    listView.Groups.Add(group);
                }
            }
        }
        private void HydrantsReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");

            using (var ec = new EntityController())
            {
                var full = ec.Hydrants.ToList();
                var selective = full.Where(h => h.IsDamage).ToList();
                var locationNames = selective.Select(e => e.Parent.Parent.ToString()).Distinct();

                foreach (var name in locationNames)
                {
                    var group = new ListViewGroup(name);
                    var selectiveThisParent = selective.Where(e => e.Parent.Parent.ToString() == name);
                    foreach (var hyd in selectiveThisParent)
                    {
                        var item = new ListViewItem(hyd.ToString(), group);
                        item.Tag = hyd.GetSign();
                        string fault = "";
                        if (hyd.IsDamage)
                            fault += "Поврежден; ";
                        var subItems = new ListViewItem.ListViewSubItem[]
                            { new ListViewItem.ListViewSubItem(item, hyd.Parent.ToString()),
                              new ListViewItem.ListViewSubItem(item, fault)};
                        item.SubItems.AddRange(subItems);
                        listView.Items.Add(item);
                    }
                    listView.Groups.Add(group);
                }
            }
        }
        private void RechargeExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Оставшийся срок (в месяцах)");

            List<Extinguisher> extinguishersFull;
            using (var ec = new EntityController())
            {
                extinguishersFull = ec.Extinguishers.ToList();
                var extinguishers = extinguishersFull.Where(e => e.DateRecharge.Subtract(DateTime.Now).Days < 365).ToList();
                var locationNames = extinguishers.Select(e => e.Parent.Parent.ToString()).Distinct();

                foreach (var name in locationNames)
                {
                    var group = new ListViewGroup(name);
                    var exs = extinguishers.Where(e => e.Parent.Parent.ToString() == name);
                    foreach (var e in exs)
                    {
                        var item = new ListViewItem(e.ToString(), group);
                        item.Tag = e.GetSign();
                        var subItems = new ListViewItem.ListViewSubItem[]
                            { new ListViewItem.ListViewSubItem(item, e.Parent.ToString()),
                              new ListViewItem.ListViewSubItem(item, e.DateRecharge.SubtractMonths(DateTime.Now).ToString())};
                        item.SubItems.AddRange(subItems);
                        listView.Items.Add(item);
                    }
                    listView.Groups.Add(group);
                }
            }
        }
        private void InitColumns(params string[] columnsNames)
        {
            listView.Clear();
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
        private void DamageEquipmentReport()
        {
            using (var ec = new EntityController())
            {
                var extinguishersFull = ec.Extinguishers.ToList();
                var extinguishers = extinguishersFull.Where(e => e.DateRecharge.Subtract(DateTime.Now).Days < 365).ToList();
                var locationNames = extinguishers.Select(e => e.Parent.Parent.ToString()).Distinct();

                foreach (var name in locationNames)
                {
                    var group = new ListViewGroup(name);
                    var exs = extinguishers.Where(e => e.Parent.Parent.ToString() == name);
                    foreach (var e in exs)
                    {
                        var item = new ListViewItem(e.ToString(), group);
                        item.Tag = e.GetSign();
                        var subItems = new ListViewItem.ListViewSubItem[]
                            { new ListViewItem.ListViewSubItem(item, e.Parent.ToString()),
                              new ListViewItem.ListViewSubItem(item, e.DateRecharge.SubtractMonths(DateTime.Now).ToString())};
                        item.SubItems.AddRange(subItems);
                        listView.Items.Add(item);
                    }
                    listView.Groups.Add(group);
                }
            }
        }
    }

}