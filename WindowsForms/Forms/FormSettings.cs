using BL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sett = BL.Properties.Settings;

namespace WindowsForms
{
    /// <summary>
    /// Форма настроек.
    /// </summary>
    public partial class FormSettings : Form
    {

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
        }

        /// <summary>
        /// Событие по изменению шаблона именования.
        /// </summary>
        public event Action<Type> ChangeSample;

        /// <summary>
        /// Событие по изменению настроек размеров иконок.
        /// </summary>
        public event Action ChangeIconSize;

        private int prevAbsoluteIconsSize;

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (Helper.CorrectSample(new TextBox[] { txbFireCabinets, txbExtinguishers, txbHoses, txbHydrants }) == 0)
                return;
            SetSampleName(txbFireCabinets);
            SetSampleName(txbExtinguishers);
            SetSampleName(txbHoses);
            SetSampleName(txbHydrants);

            prevAbsoluteIconsSize = scrIconSize.Value;
            Sett.Default.RatioIconSize = InverseIconSize(prevAbsoluteIconsSize);
            Sett.Default.Save();
            Close();
        }

        private void SetSampleName(TextBox textBox)
        {
            var type = textBox.Tag as Type;
            var value = textBox.Text.Trim();
            if (value != GetterOfType.GetSample(type))
            {
                var sample = value == string.Empty ? GetterOfType.GetDefaultSampleName(type) : textBox.Text;
                GetterOfType.SetSample(type, sample);
                ChangeSample?.Invoke(type);
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
        /// Обработчик события закрытия формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sett.Default.RatioIconSize = InverseIconSize(prevAbsoluteIconsSize);
            ChangeIconSize?.Invoke();
        }

        /// <summary>
        /// Инверсия размера иконок.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private int InverseIconSize(int size) => scrIconSize.Maximum + scrIconSize.Minimum - size;
    }
}
