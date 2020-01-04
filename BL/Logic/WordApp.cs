using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace BL
{
    public class WordApp
    {

        public void CreateNewDocument(Extinguisher extinguisher)
        {
            var wrd = new Word.Application();
            wrd.Visible = true;
            //var workBook = wrd.Documents.Add(Type.Missing);
            //Resources.Template.PassportExtinguisher;
            var x = AppDomain.CurrentDomain.BaseDirectory;

            x = Application.StartupPath.ToString();
            var workBook = wrd.Documents.Open(x + "\\PassportExtinguisher.dotx", false, true);
            var table = workBook.Tables[1];
            var bookmarks = workBook.Bookmarks.Cast<Word.Bookmark>();
            var bbb = bookmarks.Single(b => b.Name == "Number");
            //bookmarks.Range.InsertBefore( extinguisher.DateRecharge.ToShortDateString());
            //table.Cell(4, 1).Range.Text = "ку-ку";
            bbb.Range.Text = " №" + extinguisher.ToString() + "\t";
        }
    }
}
