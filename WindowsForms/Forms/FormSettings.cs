using BL;
using System;
using System.Linq;
using System.Windows.Forms;
using Sett = BL.Properties.Settings;

namespace WindowsForms
{
    public partial class FormSettings : Form
    {
        public event Action<Type> ChangeSample;
        public event Action ChangeIconSize;
        int prevAbsoluteIconsSize;
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
            if (CorrectSample(txbFireCabinets.Text, 'L', 'F'))
            {
                Sett.Default.SampleNameFireCabinets = txbFireCabinets.Text.Trim() == "" ? Sett.Default.DefaultSampleNameFireCabinets : txbFireCabinets.Text;
                ChangeSample?.Invoke(typeof(FireCabinet));
            }
            else
            {
                MessageBox.Show("Неккоректный шаблон: Пожарные шкафы");
                return;
            }

            if (CorrectSample(txbExtinguishers.Text, 'L', 'F', 'E'))
            {
                Sett.Default.SampleNameExtinguishers = txbExtinguishers.Text.Trim() == "" ? Sett.Default.DefaultSampleNameExtinguishers : txbExtinguishers.Text;
                ChangeSample?.Invoke(typeof(Extinguisher));
            }
            else
            {
                MessageBox.Show("Неккоректный шаблон: Огнетушители");
                return;
            }

            if (CorrectSample(txbHoses.Text, 'L', 'F', 'H'))
            {
                Sett.Default.SampleNameHoses = txbHoses.Text.Trim() == "" ? Sett.Default.DefaultSampleNameHoses : txbHoses.Text;
                ChangeSample?.Invoke(typeof(Hose));
            }
            else
            {
                MessageBox.Show("Неккоректный шаблон: Рукава");
                return;
            }

            if (CorrectSample(txbHydrants.Text, 'L', 'F', 'D'))
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

        private bool CorrectSample(string sourse, params char[] chars)
        {
            for (int i = 0; i < sourse.Length; i++)
            {
                var ch = sourse[i];
                if (ch == '#')
                {
                    if (i + 1 == sourse.Length)
                        return false;
                    if (!chars.Contains(sourse[i + 1]))
                        return false;
                }
            }
            return true;
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
