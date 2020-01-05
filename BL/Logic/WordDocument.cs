using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace BL
{
    public class WordDocument
    {
        Word.Application word = new Word.Application();
        Word.Document workBook;
        IEnumerable<Word.Bookmark> bookmarks;

        public WordDocument()
        {
            workBook = word.Documents.Add(Type.Missing);
            word.Visible = true;
            bookmarks = workBook.Bookmarks.Cast<Word.Bookmark>();
        }

        public WordDocument(string file, bool readOnly = false)
        {
            workBook = word.Documents.Open(file, false, readOnly);
            word.Visible = true;
            bookmarks = workBook.Bookmarks.Cast<Word.Bookmark>();
        }
        public void CreatePassportExtinguisher(Extinguisher ex)
        {
            ReplaceBookmark("Number", ex.ToString() + "\t");
            ReplaceBookmark("DateCommissioning", ex.Histories.First().DateChange.ToShortDateString() + "\t");
            ReplaceBookmark("DateCreate", ex.DateProduction.ToShortDateString() + "\t");
            ReplaceBookmark("Location", ex.FireCabinet.ToString() + "\t");
            ReplaceBookmark("Manufacturer", ex.TypeExtinguisher.Manufacturer + "\t");
            ReplaceBookmark("PlantNumber", "№ " + ex.SerialNumber + "\t");
            ReplaceBookmark("Specie", ex.TypeExtinguisher.Name + "\t");
            ReplaceBookmark("WeightOTV", ex.TypeExtinguisher.WeightExtinguishingAgent + " кг.\t");
            FillTable(ex);
        }

        private void FillTable(Extinguisher ex)
        {
            var settTable = new Dictionary<string, int>()
            {
                ["Date"] = 1,
                ["Status"] = 2,
                ["Weight"] = 3,
                ["Pressure"] = 4
            };

            var table = workBook.Tables[1];
            int row = 3;
            var dates = ex.Histories.Select(h => h.DateChange).Distinct();
            IEnumerable<History> oldHysOnDate = Enumerable.Empty<History>();

            foreach (var date in dates)
            {
                table.Rows.Add();
                var hysOnDate = GetLastHistoriesOnDate(ex, date);
                if (hysOnDate.SequenceEqual(oldHysOnDate))
                    continue;
                oldHysOnDate = hysOnDate;
                var tempStatus = "";

                foreach (var hys in hysOnDate)
                {
                    if (hys.Property == "Weight")
                    {
                        table.Cell(row, settTable["Weight"]).Range.Text = hys.NewValue + "кг.";
                    }
                    else if (hys.Property == "Pressure")
                    {
                        table.Cell(row, settTable["Pressure"]).Range.Text = hys.NewValue + "кгс/см2";
                    }
                    else if (hys.Property == "IsDented" && hys.NewValue == "True")
                    {
                        tempStatus += "Поврежден корпус\n";
                    }
                    else if (hys.Property == "IsPaintDamage" && hys.NewValue == "True")
                    {
                        tempStatus += "Повреждена краска\n";
                    }
                    else if (hys.Property == "IsHandleDamage" && hys.NewValue == "True")
                    {
                        tempStatus += "Поврежден ЗПУ\n";
                    }
                    else if (hys.Property == "IsHose" && hys.NewValue == "False")
                    {
                        tempStatus += "Отсутствует шланг\n";
                    }
                    else if (hys.Property == "IsPressureGaugeFault" && hys.NewValue == "True")
                    {
                        tempStatus += "Поврежден манометр\n";
                    }
                    else if (hys.Property == "IsLabelDamage" && hys.NewValue == "True")
                    {
                        tempStatus += "Повреждена этикетка\n";
                    }
                }
                table.Cell(row, settTable["Date"]).Range.Text = date.ToShortDateString() + "г.";
                if (tempStatus == "")
                    table.Cell(row, settTable["Status"]).Range.Text = "Норма";
                else
                    table.Cell(row, settTable["Status"]).Range.Text = tempStatus.Trim();
                row++;
            }
        }

        private Word.Bookmark GetBookmark(string name) => bookmarks.First(b => b.Name == name);
        private void ReplaceBookmark(string name, string text) => GetBookmark(name).Range.Text = text;

        public IEnumerable<History> GetLastHistoriesOnDate(Extinguisher ex, DateTime dateTime)
        {
            //TODO: Дату прибавить на одну.
            return ex.Histories.Where(h => h.DateChange <= dateTime).GroupBy(h => h.Property).Select(g => g.Last());
        }
    }

    struct StatusExtinguisher
    {

    }
}
