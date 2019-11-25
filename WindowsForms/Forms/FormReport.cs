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
        public FormReport(string option)
        {
            InitializeComponent();
            switch (option)
            {
                case "RechargeExtinguishers":
                    RechargeExtinguishersReport();
                    break;
                case "DamageEquipment":
                    DamageEquipmentReport();
                    break;
                case "Full":
                    FullReport();
                    break;
                case "FireCabinets":
                    FireCabinetsReport();
                    break;
                case "Extinguishers":
                    ExtinguishersReport();
                    break;
                case "Hoses":
                    HosesEquipmentReport();
                    break;
                case "Hydrants":
                    HydrantsExtinguishersReport();
                    break;
            }
        }

        private void HydrantsExtinguishersReport()
        {
            throw new NotImplementedException();
        }

        private void HosesEquipmentReport()
        {
            ColumnHeader columnHeader0 = new ColumnHeader();
            columnHeader0.Text = "Тип";
            columnHeader0.Width = this.Width / 3 - 10;
            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Пожарный шкаф";
            columnHeader1.Width = this.Width / 3 - 10;
            ColumnHeader columnHeader2 = new ColumnHeader();
            columnHeader2.Text = "Недостатки";
            columnHeader2.Width = this.Width / 3 - 10;
            listView.Columns.AddRange(new ColumnHeader[]
            {columnHeader0, columnHeader1, columnHeader2});

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

        private void ExtinguishersReport()
        {
            ColumnHeader columnHeader0 = new ColumnHeader();
            columnHeader0.Text = "Тип";
            columnHeader0.Width = this.Width / 3 - 10;
            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Пожарный шкаф";
            columnHeader1.Width = this.Width / 3 - 10;
            ColumnHeader columnHeader2 = new ColumnHeader();
            columnHeader2.Text = "Недостатки";
            columnHeader2.Width = this.Width / 3 - 10;
            listView.Columns.AddRange(new ColumnHeader[]
            {columnHeader0, columnHeader1, columnHeader2});

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

        private void FireCabinetsReport()
        {
            ColumnHeader columnHeader0 = new ColumnHeader();
            columnHeader0.Text = "Тип";
            columnHeader0.Width = this.Width / 2 - 10;
            ColumnHeader columnHeader2 = new ColumnHeader();
            columnHeader2.Text = "Недостатки";
            columnHeader2.Width = this.Width / 2 - 10;
            listView.Columns.AddRange(new ColumnHeader[]
            {columnHeader0, columnHeader2});

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

        private void FullReport()
        {
            throw new NotImplementedException();
        }

        private void RechargeExtinguishersReport()
        {
            ColumnHeader columnHeader0 = new ColumnHeader();
            columnHeader0.Text = "Тип";
            columnHeader0.Width = this.Width / 3 - 10;
            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Пожарный шкаф";
            columnHeader1.Width = this.Width / 3 - 10;
            ColumnHeader columnHeader2 = new ColumnHeader();
            columnHeader2.Text = "Оставшийся срок (в месяцах)";
            columnHeader2.Width = this.Width / 3 - 10;
            listView.Columns.AddRange(new ColumnHeader[]
            {columnHeader0, columnHeader1, columnHeader2});

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