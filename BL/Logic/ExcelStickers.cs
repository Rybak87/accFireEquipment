﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace BL
{
    /// <summary>
    /// Excel документ наклеек
    /// </summary>
    public class ExcelStickers
    {
        private Excel.Application excel;
        private Excel.Workbook workBook;
        private Excel.Worksheet sheet;
        int numColumns;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ExcelStickers()
        {
            try
            {
                excel = new Excel.Application();
            }
            catch
            {
                throw new Exception("Приложение Excel не найдено");
            }
            workBook = excel.Workbooks.Add(Type.Missing);
            excel.DisplayAlerts = false; //Отключить отображение окон с сообщениями
            sheet = (Excel.Worksheet)workBook.Worksheets[1];
            sheet.Name = "Наклейки";

            excel.ActiveWindow.Zoom = 70;
            excel.ActiveWindow.View = Excel.XlWindowView.xlPageLayoutView;

            sheet.Columns.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            sheet.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        /// <summary>
        /// Видимость.
        /// </summary>
        public bool Visible { get => excel.Visible; set => excel.Visible = value; }

        /// <summary>
        /// Установка высоты строк в Excel.
        /// </summary>
        /// <param name="numColumns">Количество столбцов.</param>
        private void SetColumnsWidth(int numColumns)
        {
            this.numColumns = numColumns;
            var pageWidth = excel.Application.CentimetersToPoints(21);
            var leftMargin = sheet.PageSetup.LeftMargin;
            var rightMargin = sheet.PageSetup.RightMargin;
            var contentWidth = pageWidth - leftMargin - rightMargin;
            var firstCell = (Excel.Range)sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 1]];
            var rate = (double)firstCell.Width / (double)firstCell.ColumnWidth;
            sheet.Columns.ColumnWidth = (contentWidth / rate / numColumns);
        }

        /// <summary>
        /// Установка высоты строк в Excel.
        /// </summary>
        /// <param name="numRows">Количество строк.</param>
        private void SetRowssWidth(int numRows)
        {
            var pageHeight = excel.Application.CentimetersToPoints(29.7);
            var topMargin = sheet.PageSetup.TopMargin;
            var bottomMargin = sheet.PageSetup.BottomMargin;
            var contentHeight = pageHeight - topMargin - bottomMargin;
            var firstCell = (Excel.Range)sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 1]];
            var rate = firstCell.Height / firstCell.RowHeight;
            sheet.Columns.RowHeight = (contentHeight / rate / numRows);
        }

        /// <summary>
        /// Заполнение листа Excel.
        /// </summary>
        /// <param name="numColumns">Количество столбцов.</param>
        /// <param name="numRows">Количество строк.</param>
        /// <param name="stickers">Наклейки.</param>
        public void FillWorkSheet(int numColumns, int numRows, IEnumerable<string> stickers)
        {
            SetColumnsWidth(numColumns);
            SetRowssWidth(numRows);
            int i = 1;
            int j = 1;
            foreach (var item in stickers)
            {
                sheet.Cells[i, j] = string.Format(item);
                j++;
                if (j > numColumns)
                {
                    j = 1; i++;
                }
            }
            double currWidth = sheet.Columns.ColumnWidth;
            var sizeFont = CalcFontSize(string.Format(stickers.Last()), currWidth);
            sheet.Columns.Font.Size = sizeFont;
        }

        /// <summary>
        /// Вычисление размера шрифта.
        /// </summary>
        /// <param name="template">Пример текста.</param>
        /// <param name="currWidth">Ширина столбцов.</param>
        /// <returns>Размера шрифта.</returns>
        private static int CalcFontSize(string template, double currWidth)
        {
            int currSizeFont = 8;
            int len;
            do
            {
                currSizeFont += 2;
                var f = new Font("Calibri", currSizeFont, FontStyle.Regular);
                len = System.Windows.Forms.TextRenderer.MeasureText(template, f).Width;
            } while (currWidth * 7 > len);
            return currSizeFont;
        }
    }
}
