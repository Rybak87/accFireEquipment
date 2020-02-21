using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sett = BL.Properties.Settings;

namespace WindowsForms
{
    /// <summary>
    /// Форма настроек.
    /// </summary>
    public partial class FormSettings : Form
    {

        private int prevAbsoluteIconsSize;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormSettings()
        {
            InitializeComponent();
            txbFireCabinets.Text = Sett.Default.SampleNameFireCabinets;
            txbExtinguishers.Text = Sett.Default.SampleNameExtinguishers;
            txbHoses.Text = Sett.Default.SampleNameHoses;
            txbHydrants.Text = Sett.Default.SampleNameHydrants;
            prevAbsoluteIconsSize = InverseIconSize(Sett.Default.RatioIconSize);
            scrIconSize.Value = prevAbsoluteIconsSize;
            lblIconSize.Text = prevAbsoluteIconsSize.ToString();

            txbFireCabinets.Tag = typeof(FireCabinet);
            txbExtinguishers.Tag = typeof(Extinguisher);
            txbHoses.Tag = typeof(Hose);
            txbHydrants.Tag = typeof(Hydrant);

            var t = new ToolTip();
            t.SetToolTip(txbFireCabinets, "#N - номер пожарного шкафа\n#L - номер помещения");
            t.SetToolTip(txbExtinguishers, "#N - номер огнетушителя\n#L - номер помещения\n#F - номер пожарного шкафа");
            t.SetToolTip(txbHoses, "#N - номер рукава\n#L - номер помещения\n#F - номер пожарного шкафа");
            t.SetToolTip(txbHydrants, "#N - номер пожарного крана\n#L - номер помещения\n#F - номер пожарного шкафа");
        }

        /// <summary>
        /// Событие по изменению шаблона именования.
        /// </summary>
        public event Action<Type> ChangeSample;

        /// <summary>
        /// Событие по изменению настроек размеров иконок.
        /// </summary>
        public event Action ChangeIconSize;

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            var textBoxes = new TextBox[] { txbFireCabinets, txbExtinguishers, txbHoses, txbHydrants };
            if (!HelperControl.CorrectSample(textBoxes))
                return;
            SetSampleName(textBoxes);

            prevAbsoluteIconsSize = scrIconSize.Value;
            Sett.Default.RatioIconSize = InverseIconSize(prevAbsoluteIconsSize);
            Sett.Default.Save();
            ChangeIconSize?.Invoke();
            Close();
        }

        private void SetSampleName(IEnumerable<TextBox> textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                var type = textBox.Tag as Type;
                var value = textBox.Text.Trim();
                if (value != GetterOfType.GetSampleNaming(type))
                {
                    var sample = value == string.Empty ? GetterOfType.GetDefaultSampleNaming(type) : textBox.Text;
                    GetterOfType.SetSampleNaming(type, sample);
                    ChangeSample?.Invoke(type);
                }
            }
        }

        /// <summary>
        /// Обработчик события скролла размера иконок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrIconSize_ValueChanged(object sender, EventArgs e)
        {
            Sett.Default.RatioIconSize = InverseIconSize(scrIconSize.Value);
            ChangeIconSize?.Invoke();
            lblIconSize.Text = scrIconSize.Value.ToString();
        }

        /// <summary>
        /// Инверсия размера иконок.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private int InverseIconSize(int size) => scrIconSize.Maximum + scrIconSize.Minimum - size;
    }
}
