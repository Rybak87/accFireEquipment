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
        private Func<Equipment, string> fireCabinetFunc;
        private Func<Equipment, string> extinguisherFunc;
        private Type lastType;
        Dictionary<Type, Action> dictReport;
        Dictionary<Type, Action> dictInitColums;

        ExcelStickers exl;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormStickers()
        {
            InitializeComponent();
            dictReport = new Dictionary<Type, Action>
            {
                [typeof(FireCabinet)] = () => listView.FillListView(typeof(FireCabinet), fName, filterFireCabinetSticker),
                [typeof(Extinguisher)] = () => listView.FillListView(typeof(Extinguisher), fName, fParent, filterExtinguisherSticker)
            };
            dictInitColums = new Dictionary<Type, Action>
            {
                [typeof(FireCabinet)] = () => listView.InitColumns("Тип", "Наклейка"),
                [typeof(Extinguisher)] = () => listView.InitColumns("Тип", "Пожарный шкаф", "Наклейка")
            };

            FireCabinetsMenu.Image = IconsGetter.GetIconImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = IconsGetter.GetIconImage(typeof(Extinguisher));
            FireCabinetsMenu.Click += (s, e) => FillListView(typeof(FireCabinet));
            ExtinguishersMenu.Click += (s, e) => FillListView(typeof(Extinguisher));

            txbFireCabinets.Tag = typeof(FireCabinet);
            txbExtinguishers.Tag = typeof(Extinguisher);
            txbFireCabinets.Text = Sett.Default.SampleNameFireCabinets;
            txbExtinguishers.Text = Sett.Default.SampleNameExtinguishers;

            fireCabinetFunc = eq => GetterOfType.GetName(eq, txbFireCabinets.Text);
            extinguisherFunc = eq => GetterOfType.GetName(eq, txbExtinguishers.Text);

            filterFireCabinetSticker = new Filter(true, new Instruction(NeedSticker, fireCabinetFunc));
            filterExtinguisherSticker = new Filter(true, new Instruction(NeedSticker, extinguisherFunc));

            var t = new ToolTip();
            t.SetToolTip(txbFireCabinets, "#N - номер пожарного шкафа\n#L - номер помещения");
            t.SetToolTip(txbExtinguishers, "#N - номер огнетушителя\n#L - номер помещения\n#F - номер пожарного шкафа");
        }

        private void FillListView(Type type)
        {
            if (type == null)
                return;
            dictInitColums[type].Invoke();
            lastType = type;
            dictReport[type].Invoke();
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            var textBoxes = new TextBox[] { txbFireCabinets, txbExtinguishers };
            if (!HelperControl.CorrectSample(textBoxes))
                return;

            FillListView(lastType);
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
                filterFireCabinetSticker = new Filter(true, new Instruction(NeedSticker, fireCabinetFunc));
                filterExtinguisherSticker = new Filter(true, new Instruction(NeedSticker, extinguisherFunc));
            }
            else
            {
                filterFireCabinetSticker = new Filter(true, new Instruction(fireCabinetFunc));
                filterExtinguisherSticker = new Filter(true, new Instruction(extinguisherFunc));
            }

            FillListView(lastType);
        }
    }
}
