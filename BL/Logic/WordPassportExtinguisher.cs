using System;
using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace BL
{
    /// <summary>
    /// Паспорт огнетушителя.
    /// </summary>
    public class WordPassportExtinguisher
    {
        private Word.Application word = new Word.Application();
        private Word.Document workBook;
        private IEnumerable<Word.Bookmark> bookmarks;
        private const int ROW_FILL_START = 3;
        private readonly Dictionary<string, int> column = new Dictionary<string, int>()
        {
            ["Date"] = 1,
            ["Status"] = 2,
            ["Weight"] = 3,
            ["Pressure"] = 4
        };

        /// <summary>
        /// Конструктор.
        /// </summary>
        public WordPassportExtinguisher()
        {
            var template = AppDomain.CurrentDomain.BaseDirectory + "Resources\\PassportExtinguisher.dotx";
            workBook = word.Documents.Add(template);
            bookmarks = workBook.Bookmarks.Cast<Word.Bookmark>();
        }

        /// <summary>
        /// Создать паспорт огнетушителя в приложении Word.
        /// </summary>
        /// <param name="ex">Огнетушитель.</param>
        public void CreatePassportExtinguisher(Extinguisher ex)
        {
            ReplaceBookmark("Number", ex.ToString() + "\t");
            ReplaceBookmark("DateCommissioning", ex.Histories.First().DateChange.ToShortDateString() + "\t");
            ReplaceBookmark("DateCreate", ex.DateProduction.ToShortDateString() + "\t");
            ReplaceBookmark("Location", ex.FireCabinet.ToString() + "\t");
            ReplaceBookmark("Manufacturer", ex.KindExtinguisher.Manufacturer + "\t");
            ReplaceBookmark("PlantNumber", "№ " + ex.SerialNumber + "\t");
            ReplaceBookmark("Specie", ex.KindExtinguisher.Name + "\t");
            ReplaceBookmark("WeightOTV", ex.KindExtinguisher.WeightExtinguishingAgent + " кг.\t");
            FillTable(ex);
            word.Visible = true;
        }

        /// <summary>
        /// Заполнить таблицу в паспорте огнетушителя.
        /// </summary>
        /// <param name="ex">Огнетушитель.</param>
        private void FillTable(Extinguisher ex)
        {
            var table = workBook.Tables[1];
            int row = ROW_FILL_START;

            //Учитывать ли время.
            var modeTime = Properties.Settings.Default.UseTime;
            Func<History, DateTime> datePicker;
            if (modeTime)
                datePicker = h => h.DateChange;
            else
                datePicker = h => h.DateChange.Date;
            //var dates = ex.Histories.Select(datePicker).Distinct();
            var datesHistories = ex.Histories.GroupBy(datePicker);

            foreach (var dateHistories in datesHistories)
            {
                table.Rows.Add();
                var tempStatus = "";
                var date = dateHistories.Key;

                foreach (var hys in dateHistories)
                {
                    if (hys.Property == "Weight")
                        table.Cell(row, column["Weight"]).Range.Text = hys.Value + "кг.";
                    else if (hys.Property == "Pressure")
                        table.Cell(row, column["Pressure"]).Range.Text = hys.Value + "кгс/см2";
                    else if (hys.Property == "IsDented" && hys.Value == "True")
                        tempStatus += "Поврежден корпус\n";
                    else if (hys.Property == "IsPaintDamage" && hys.Value == "True")
                        tempStatus += "Повреждена краска\n";
                    else if (hys.Property == "IsHandleDamage" && hys.Value == "True")
                        tempStatus += "Повреждено ЗПУ\n";
                    else if (hys.Property == "IsHose" && hys.Value == "False")
                        tempStatus += "Отсутствует шланг\n";
                    else if (hys.Property == "IsPressureGaugeFault" && hys.Value == "True")
                        tempStatus += "Поврежден манометр\n";
                    else if (hys.Property == "IsLabelDamage" && hys.Value == "True")
                        tempStatus += "Повреждена этикетка\n";
                }

                table.Cell(row, column["Date"]).Range.Text = date.ToShortDateString() + "г.";
                if (tempStatus == "")
                    table.Cell(row, column["Status"]).Range.Text = "Норма";
                else
                    table.Cell(row, column["Status"]).Range.Text = tempStatus.Trim();
                row++;

                //IEnumerable<History> oldHysOnDate = Enumerable.Empty<History>();

                //foreach (var date in dates)
                //{
                //    table.Rows.Add();
                //    IEnumerable<History> hysOnDate;
                //    if (modeTime)
                //        hysOnDate = GetLastHistoriesOnDate(ex, date);
                //    else
                //        hysOnDate = GetLastHistoriesOnDate(ex, date.AddDays(1));

                //    if (hysOnDate.SequenceEqual(oldHysOnDate))
                //        continue;
                //    oldHysOnDate = hysOnDate;
                //    var tempStatus = "";

                //    foreach (var hys in hysOnDate)
                //    {
                //        if (hys.Property == "Weight")
                //            table.Cell(row, numberOrderTable["Weight"]).Range.Text = hys.Value + "кг.";
                //        else if (hys.Property == "Pressure")
                //            table.Cell(row, numberOrderTable["Pressure"]).Range.Text = hys.Value + "кгс/см2";
                //        else if (hys.Property == "IsDented" && hys.Value == "True")
                //            tempStatus += "Поврежден корпус\n";
                //        else if (hys.Property == "IsPaintDamage" && hys.Value == "True")
                //            tempStatus += "Повреждена краска\n";
                //        else if (hys.Property == "IsHandleDamage" && hys.Value == "True")
                //            tempStatus += "Повреждено ЗПУ\n";
                //        else if (hys.Property == "IsHose" && hys.Value == "False")
                //            tempStatus += "Отсутствует шланг\n";
                //        else if (hys.Property == "IsPressureGaugeFault" && hys.Value == "True")
                //            tempStatus += "Поврежден манометр\n";
                //        else if (hys.Property == "IsLabelDamage" && hys.Value == "True")
                //            tempStatus += "Повреждена этикетка\n";
                //    }
                //    table.Cell(row, numberOrderTable["Date"]).Range.Text = date.ToShortDateString() + "г.";
                //    if (tempStatus == "")
                //        table.Cell(row, numberOrderTable["Status"]).Range.Text = "Норма";
                //    else
                //        table.Cell(row, numberOrderTable["Status"]).Range.Text = tempStatus.Trim();
                //    row++;
                //}
            }
        }
        /// <summary>
        /// Возвращает закладку в документе Word.
        /// </summary>
        /// <param name="name">Имя закладки.</param>
        private Word.Bookmark GetBookmark(string name) => bookmarks.First(b => b.Name == name);

        /// <summary>
        /// Меняет текст закладки.
        /// </summary>
        /// <param name="name">Имя закладки.</param>
        /// <param name="text">Текст для замены.</param>
        private void ReplaceBookmark(string name, string text) => GetBookmark(name).Range.Text = text;

        /// <summary>
        /// Возвращает коллекцию изменений огнетушителя на дату.
        /// </summary>
        /// <param name="ex">Огнетушитель.</param>
        /// <param name="dateTime">Дата.</param>
        public IEnumerable<History> GetLastHistoriesOnDate(Extinguisher ex, DateTime dateTime)
        {
            return ex.Histories.Where(h => h.DateChange <= dateTime).GroupBy(h => h.Property).Select(g => g.Last());
        }
    }
}
