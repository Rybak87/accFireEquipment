﻿using BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Sett = BL.Properties.Settings;

namespace WindowsForms
{

    public partial class FormStickers : Form
    {
        private FilterSet filterName = new FilterSet(ent => ent.ToString());
        private FilterSet filterParent = new FilterSet(ent => ent.GetLocation.ToString());
        private FilterSet filterParentParent = new FilterSet(ent => ent.GetLocation.ToString());
        private FilterSet filterFireCabinetSticker;
        private FilterSet filterExtinguisherSticker;
        private Type lastType;
        public event Action<EntitySign> EditEntity;
        private Func<EntityBase, bool> NeedSticker = ent => !((ISticker)ent).IsSticker;
        public FormStickers()
        {
            InitializeComponent();
            FireCabinetsMenu.Image = ImageSettings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));
            FireCabinetsMenu.Click += (s, e) => FireCabinetsReport();
            ExtinguishersMenu.Click += (s, e) => ExtinguishersReport();
            txbFireCabinets.Text = Sett.Default.SampleNameFireCabinets;
            txbExtinguishers.Text = Sett.Default.SampleNameExtinguishers;
            filterFireCabinetSticker = new FilterSet(true, new Filter(NeedSticker, CreateStickerFireCabinet));
            filterExtinguisherSticker = new FilterSet(true, new Filter(NeedSticker, CreateStickerExtinguisher));
        }

        private void FireCabinetsReport()
        {
            InitColumns("Тип", "Наклейка");
            lastType = typeof(FireCabinet);
            listView.EntityReport(typeof(FireCabinet), filterParent, filterName, filterFireCabinetSticker);
        }
        private void ExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Наклейка");
            lastType = typeof(Extinguisher);
            listView.EntityReport(typeof(Extinguisher), filterParentParent, filterName, filterParent, filterExtinguisherSticker);
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

            var exl = new Excel.Application();
            exl.Visible = true;
            //Добавить рабочую книгу
            var workBook = exl.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            exl.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            var sheet = (Excel.Worksheet)exl.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Наклейки";

            //sheet.Columns.ColumnWidth = 80 / numColumns.Value;
            //sheet.Rows.RowHeight = 732 / numRows.Value;
            exl.ActiveWindow.Zoom = 70;
            exl.ActiveWindow.View = Excel.XlWindowView.xlPageLayoutView;

            sheet.Columns.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            sheet.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            int i = 1;
            int j = 1;
            foreach (var item in stickers)
            {
                sheet.Cells[i, j] = string.Format(item);
                j++;
                if (j > numColumns.Value)
                {
                    j = 1; i++;
                }
            }
            SetColumnsWidth(exl, sheet);
            SetRowssWidth(exl, sheet);
            double currWidth = sheet.Columns.ColumnWidth;
            var sizeFont = CalcFontSize(string.Format(stickers[0]), currWidth);
            sheet.Columns.Font.Size = sizeFont;
        }

        private void SetColumnsWidth(Excel.Application exl, Excel.Worksheet sheet)
        {
            var pageWidth = exl.Application.CentimetersToPoints(21);
            var leftMargin = sheet.PageSetup.LeftMargin;
            var rightMargin = sheet.PageSetup.RightMargin;
            var contentWidth = pageWidth - leftMargin - rightMargin;
            var firstCell = (Excel.Range)sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 1]];
            var rate = (double)firstCell.Width / (double)firstCell.ColumnWidth;
            sheet.Columns.ColumnWidth = (contentWidth / rate / (double)numColumns.Value);
        }
        private void SetRowssWidth(Excel.Application exl, Excel.Worksheet sheet)
        {
            var pageHeight = exl.Application.CentimetersToPoints(29.7);
            var topMargin = sheet.PageSetup.TopMargin;
            var bottomMargin = sheet.PageSetup.BottomMargin;
            var contentHeight = pageHeight - topMargin - bottomMargin;
            var firstCell = (Excel.Range)sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 1]];
            var rate = firstCell.Height / firstCell.RowHeight;
            sheet.Columns.RowHeight = (contentHeight / rate / (double)numRows.Value);
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
            if (chkWithoutStickers.Checked)
            {
                filterFireCabinetSticker = new FilterSet(true, new Filter(NeedSticker, CreateStickerFireCabinet));
                filterExtinguisherSticker = new FilterSet(true, new Filter(NeedSticker, CreateStickerExtinguisher));
            }
            else
            {
                filterFireCabinetSticker = new FilterSet(true, new Filter(CreateStickerFireCabinet));
                filterExtinguisherSticker = new FilterSet(true, new Filter(CreateStickerExtinguisher));
            }

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

        private string CreateStickerExtinguisher(EntityBase entityBase2)
        {
            Extinguisher entityBase = (Extinguisher)entityBase2;
            var sample = txbExtinguishers.Text;
            sample = sample.Replace("#L", (entityBase.GetLocation).Number.ToString());
            sample = sample.Replace("#F", ((FireCabinet)(entityBase.Parent)).Number.ToString());
            sample = sample.Replace("#E", entityBase.Number.ToString());
            return sample;
        }

        private string CreateStickerFireCabinet(EntityBase entityBase2)
        {
            FireCabinet entityBase = (FireCabinet)entityBase2;
            var sample = txbFireCabinets.Text;
            sample = sample.Replace("#L", ((Location)entityBase.Parent).Number.ToString());
            sample = sample.Replace("#F", entityBase.Number.ToString());
            return sample;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!txbFireCabinets.Text.CorrectSample('L', 'F'))
            {
                MessageBox.Show("Неккоректный шаблон: Пожарные шкафы");
                return;
            }
            if (!txbExtinguishers.Text.CorrectSample('L', 'F', 'E'))
            {
                MessageBox.Show("Неккоректный шаблон: Огнетушители");
                return;
            }

            if (lastType == typeof(FireCabinet))
                FireCabinetsReport();
            else if (lastType == typeof(Extinguisher))
                ExtinguishersReport();
        }
    }
}
