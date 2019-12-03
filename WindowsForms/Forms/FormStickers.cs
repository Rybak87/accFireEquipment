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
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsForms
{
    public partial class FormStickers : Form
    {
        private Type lastType;
        public event Action<EntitySign> EditEntity;
        public FormStickers()
        {
            InitializeComponent();
            FireCabinetsMenu.Image = ImageSettings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));
            FireCabinetsMenu.Click += (s, e) => FireCabinetsReport();
            ExtinguishersMenu.Click += (s, e) => ExtinguishersReport();
        }

        private void FireCabinetsReport()
        {
            InitColumns("Тип", "Наклейка");
            lastType = typeof(FireCabinet);
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
                        string sticker = fc.ToString();
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
            lastType = typeof(Extinguisher);
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
                        string sticker = ex.ToString();
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
            var columnWidth = listView.Width / countColumns - 10;
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
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            var stickers = new List<string>();
            if (listView.Items.Count == 0)
                return;
            if (listView.SelectedItems.Count == 0)
                foreach (var item in listView.Items)
                {
                    var str = ((ListViewItem)item).SubItems[((ListViewItem)item).SubItems.Count - 1].Text;
                    stickers.Add(str);
                }
            else
                foreach (var item in listView.SelectedItems)
                {
                    var str = ((ListViewItem)item).SubItems[((ListViewItem)item).SubItems.Count - 1].Text;
                    stickers.Add(str);
                }

            var ex = new Excel.Application();
            ex.Visible = true;
            //Добавить рабочую книгу
            var workBook = ex.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            ex.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            var sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Наклейки";
            sheet.Columns.ColumnWidth = 80 / numColumns.Value;
            sheet.Rows.RowHeight = 732 / numRows.Value;
            ex.ActiveWindow.Zoom = 70;
            ex.ActiveWindow.View = Excel.XlWindowView.xlPageLayoutView;
            
            sheet.Columns.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            sheet.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            double currWidth = sheet.Columns.ColumnWidth;
            var sizeFont = CalcFontSize(string.Format(stickers[0]), currWidth);
            sheet.Columns.Font.Size = sizeFont;
            for (int i = 0; i < numRows.Value; i++)
                for (int j = 0; j < numColumns.Value; j++)
                {
                    int index = j + i * (j + 1);
                    if (index >= stickers.Count)
                        return;
                    sheet.Cells[i + 1, j + 1] = string.Format(stickers[index]);
                }

        }

        private static int CalcFontSize(string template, double currWidth)
        {
            int currSizeFont = 8;
            int len;
            do
            {
                currSizeFont += 2;
                var f = new Font("Calibri", currSizeFont, FontStyle.Regular);
                len = TextRenderer.MeasureText(template, f).Width;
            } while (currWidth * 7 > len);
            return currSizeFont;
        }

        private void chkWithoutStickers_CheckedChanged(object sender, EventArgs e)
        {
            if (lastType == typeof(FireCabinet))
                FireCabinetsReport();
            else if (lastType == typeof(Extinguisher))
                ExtinguishersReport();
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
