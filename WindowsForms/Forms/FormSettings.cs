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
        public FormSettings()
        {
            InitializeComponent();
            txbFireCabinets.Text = Sett.Default.SampleNameFireCabinets;
            txbExtinguishers.Text = Sett.Default.SampleNameExtinguishers;
            txbHoses.Text = Sett.Default.SampleNameHoses;
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
            Sett.Default.Save();
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
    }
}
