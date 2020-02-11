using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sett = BL.Properties.Settings;

namespace WindowsForms
{
    /// <summary>
    /// Форма наклеек.
    /// </summary>
    public partial class FormStickers : Form
    {
        private Filter fName = Filters.filterName;
        private Filter fParent = Filters.filterParent;
        private Filter filterFireCabinetSticker;
        private Filter filterExtinguisherSticker;
        private Func<EntityBase, bool> NeedSticker = ent => !((ISticker)ent).IsSticker;
        private Type lastType;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormStickers()
        {
            InitializeComponent();
            FireCabinetsMenu.Image = IconsGetter.GetIconImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = IconsGetter.GetIconImage(typeof(Extinguisher));
            FireCabinetsMenu.Click += (s, e) => FireCabinetsReport();
            ExtinguishersMenu.Click += (s, e) => ExtinguishersReport();
            txbFireCabinets.Text = Sett.Default.SampleNameFireCabinets;
            txbExtinguishers.Text = Sett.Default.SampleNameExtinguishers;
            filterFireCabinetSticker = new Filter(true, new Instruction(NeedSticker, CreateStickerFireCabinet));
            filterExtinguisherSticker = new Filter(true, new Instruction(NeedSticker, CreateStickerExtinguisher));

            txbFireCabinets.Tag = typeof(FireCabinet);
            txbExtinguishers.Tag = typeof(Extinguisher);
        }

        /// <summary>
        /// Вывод пожарных шкафов в ListView.
        /// </summary>
        private void FireCabinetsReport()
        {
            listView.InitColumns("Тип", "Наклейка");
            lastType = typeof(FireCabinet);
            listView.EntityReport(typeof(FireCabinet), fName, filterFireCabinetSticker);
        }

        /// <summary>
        /// Вывод огнетушителей в ListView.
        /// </summary>
        private void ExtinguishersReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Наклейка");
            lastType = typeof(Extinguisher);
            listView.EntityReport(typeof(Extinguisher), fName, fParent, filterExtinguisherSticker);
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Helper.CorrectSample(new TextBox[] { txbFireCabinets, txbExtinguishers }) == 0)
                return;

            if (lastType == typeof(FireCabinet))
                FireCabinetsReport();
            else if (lastType == typeof(Extinguisher))
                ExtinguishersReport();
        }


        private List<string> GetStickers()
        {
            List<string> stickers = new List<string>();

            IEnumerable<ListViewItem> listViewCollection;
            if (listView.SelectedItems.Count == 0)
                listViewCollection = listView.Items.Cast<ListViewItem>();
            else
                listViewCollection = listView.SelectedItems.Cast<ListViewItem>();

            foreach (ListViewItem item in listViewCollection)
            {
                var str = item.SubItems[item.SubItems.Count - 1].Text;
                stickers.Add(str);
            }
            return stickers;
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count == 0)
                return;

            ExcelStickers exl;
            try
            {
                exl = new ExcelStickers();
            }
            catch (Exception exept)
            {
                MessageBox.Show(exept.Message);
                return;
            }
            exl.FillWorkSheet((int)numColumns.Value, (int)numRows.Value, GetStickers());
            exl.Visible = true;
        }

        /// <summary>
        /// Обработчик события двойного клика ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var item = listView.SelectedItems[0];
            var sign = (EntitySign)item.Tag;
            Dialogs.EditDialog(sign);
        }

        /// <summary>
        /// Обработчик события изменения значения CheckBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWithoutStickers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithoutStickers.Checked)
            {
                filterFireCabinetSticker = new Filter(true, new Instruction(NeedSticker, CreateStickerFireCabinet));
                filterExtinguisherSticker = new Filter(true, new Instruction(NeedSticker, CreateStickerExtinguisher));
            }
            else
            {
                filterFireCabinetSticker = new Filter(true, new Instruction(CreateStickerFireCabinet));
                filterExtinguisherSticker = new Filter(true, new Instruction(CreateStickerExtinguisher));
            }

            if (lastType == typeof(FireCabinet))
                FireCabinetsReport();
            else if (lastType == typeof(Extinguisher))
                ExtinguishersReport();
        }

        /// <summary>
        /// Возвращает строку шаблона именования огнетушителя.
        /// </summary>
        /// <param name="entityBase2"></param>
        /// <returns></returns>
        private string CreateStickerExtinguisher(EntityBase entityBase2)
        {
            Extinguisher entityBase = (Extinguisher)entityBase2;
            var sample = txbExtinguishers.Text;
            sample = sample.Replace("#L", (entityBase.GetLocation).Number.ToString());
            sample = sample.Replace("#F", ((FireCabinet)(entityBase.Parent)).Number.ToString());
            sample = sample.Replace("#E", entityBase.Number.ToString());
            return sample;
        }

        /// <summary>
        /// Возвращает строку шаблона именования пожарного шкафа.
        /// </summary>
        /// <param name="entityBase2"></param>
        /// <returns></returns>
        private string CreateStickerFireCabinet(EntityBase entityBase2)
        {
            FireCabinet entityBase = (FireCabinet)entityBase2;
            var sample = txbFireCabinets.Text;
            sample = sample.Replace("#L", ((Location)entityBase.Parent).Number.ToString());
            sample = sample.Replace("#F", entityBase.Number.ToString());
            return sample;
        }
    }
}
