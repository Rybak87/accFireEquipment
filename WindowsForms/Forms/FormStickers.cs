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
    public partial class FormStickers : Form
    {
        public FormStickers()
        {
            InitializeComponent();
            FireCabinetsMenu.Image = Settings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = Settings.IconsImage(typeof(Extinguisher));
            FireCabinetsMenu.Click += (s, e) => FireCabinetsReport();
            ExtinguishersMenu.Click += (s, e) => ExtinguishersReport();
        }

        private void FireCabinetsReport()
        {
            InitColumns("Тип", "Наклейка");

            using (var ec = new EntityController())
            {
                var full = ec.FireCabinets.ToList();
                List<FireCabinet> selective;
                if (chkWithoutStickers.Checked)
                    selective = full.Where(f => !f.IsSticker).ToList();
                else
                    selective = full;
                var locationTuples = selective.Select(e => (e.Parent.ToString(), ((INumber)e.Parent).Number)).Distinct();

                foreach (var loc in locationTuples)
                {
                    var group = new ListViewGroup(loc.Item1);
                    var selectiveThisParent = selective.Where(f => f.Parent.ToString() == loc.Item1);
                    foreach (var fc in selectiveThisParent)
                    {
                        var item = new ListViewItem(fc.ToString(), group);
                        item.Tag = fc.GetSign();
                        string sticker = "ПК-" + loc.Number + "." + fc.Number;
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, sticker));
                        listView.Items.Add(item);
                    }
                    listView.Groups.Add(group);
                }
            }

        }
        private void ExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Наклейка");
            using (var ec = new EntityController())
            {
                var full = ec.Extinguishers.ToList();
                List<Extinguisher> selective;
                if (chkWithoutStickers.Checked)
                    selective = full.Where(e => !e.IsSticker).ToList();
                else
                    selective = full;
                var locationTuples = selective.Select(e => (e.Parent.Parent.ToString(), ((INumber)e.Parent.Parent).Number)).Distinct();

                foreach (var loc in locationTuples)
                {
                    var group = new ListViewGroup(loc.Item1);
                    var selectiveThisParent = selective.Where(e => e.Parent.Parent.ToString() == loc.Item1);
                    foreach (var ex in selectiveThisParent)
                    {
                        var item = new ListViewItem(ex.ToString(), group);
                        item.Tag = ex.GetSign();
                        string sticker = loc.Number + "." + ((INumber)ex.Parent).Number + "/" + ex.Number;
                        var subItems = new ListViewItem.ListViewSubItem[]
                            { new ListViewItem.ListViewSubItem(item, ex.Parent.ToString()),
                              new ListViewItem.ListViewSubItem(item, sticker)};
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
    }
}
