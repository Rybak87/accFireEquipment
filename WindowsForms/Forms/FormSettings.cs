using BL;
using System;
using System.Windows.Forms;
using Sett = BL.Properties.Settings;

namespace WindowsForms
{
    public partial class FormSettings : Form
    {
        public event Action<Type> ChangeSample;
        public event Action ChangeIconSize;

        private int prevAbsoluteIconsSize;
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
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (txbFireCabinets.Text.CorrectSample('L', 'F'))
            {
                Sett.Default.SampleNameFireCabinets = txbFireCabinets.Text.Trim() == "" ? Sett.Default.DefaultSampleNameFireCabinets : txbFireCabinets.Text;
                ChangeSample?.Invoke(typeof(FireCabinet));
            }
            else
            {
                MessageBox.Show("Неккоректный шаблон: Пожарные шкафы");
                return;
            }

            if (txbExtinguishers.Text.CorrectSample('L', 'F', 'E'))
            {
                Sett.Default.SampleNameExtinguishers = txbExtinguishers.Text.Trim() == "" ? Sett.Default.DefaultSampleNameExtinguishers : txbExtinguishers.Text;
                ChangeSample?.Invoke(typeof(Extinguisher));
            }
            else
            {
                MessageBox.Show("Неккоректный шаблон: Огнетушители");
                return;
            }

            if (txbHoses.Text.CorrectSample('L', 'F', 'H'))
            {
                Sett.Default.SampleNameHoses = txbHoses.Text.Trim() == "" ? Sett.Default.DefaultSampleNameHoses : txbHoses.Text;
                ChangeSample?.Invoke(typeof(Hose));
            }
            else
            {
                MessageBox.Show("Неккоректный шаблон: Рукава");
                return;
            }

            if (txbHydrants.Text.CorrectSample('L', 'F', 'D'))
            {
                Sett.Default.SampleNameHydrants = txbHydrants.Text.Trim() == "" ? Sett.Default.DefaultSampleNameHydrants : txbHydrants.Text;
                ChangeSample?.Invoke(typeof(Hydrant));
            }
            else
            {
                MessageBox.Show("Неккоректный шаблон: Пожарный кран");
                return;
            }
            prevAbsoluteIconsSize = scrIconSize.Value;
            Sett.Default.RatioIconSize = InverseIconSize(prevAbsoluteIconsSize);
            Sett.Default.Save();
            Close();
        }

        private void scrIconSize_ValueChanged(object sender, EventArgs e)
        {
            Sett.Default.RatioIconSize = InverseIconSize(scrIconSize.Value);
            ChangeIconSize?.Invoke();
            lblIconSize.Text = scrIconSize.Value.ToString();
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sett.Default.RatioIconSize = InverseIconSize(prevAbsoluteIconsSize);
            ChangeIconSize?.Invoke();
        }

        private int InverseIconSize(int size) => scrIconSize.Maximum + scrIconSize.Minimum - size;
    }
}
