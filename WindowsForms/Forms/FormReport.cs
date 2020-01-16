using BL;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Форма отчета.
    /// </summary>
    public partial class FormReport : Form
    {
        private Filter fName = Filters.filterName;
        private Filter fParent = Filters.filterParent;
        private Filter fFireCabinetFault = Filters.filterFireCabinetFault;
        private Filter fExtinguisherFault = Filters.filterExtinguisherFault;
        private Filter fHoseFault = Filters.filterHoseFault;
        private Filter fHydrantFault = Filters.filterHydrantFault;
        private Filter fExtinguisherRecharge = Filters.filterExtinguisherRecharge;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormReport()
        {
            InitializeComponent();
            FullMenu.Image = IconsGetter.GetIcon(typeof(Location));
            FireCabinetsMenu.Image = IconsGetter.GetIcon(typeof(FireCabinet));
            ExtinguishersMenu.Image = IconsGetter.GetIcon(typeof(Extinguisher));
            HosesMenu.Image = IconsGetter.GetIcon(typeof(Hose));
            HydrantsMenu.Image = IconsGetter.GetIcon(typeof(Hydrant));
            RechargeExtinguishersMenu.Image = IconsGetter.GetIcon(typeof(Extinguisher));

            FullMenu.Click += (s, e) => FullReport();
            FullMenu.Click += (s, e) => lastReport = () => FullReport();

            FireCabinetsMenu.Click += (s, e) => FireCabinetsReport();
            FireCabinetsMenu.Click += (s, e) => lastReport = () => FireCabinetsReport();

            ExtinguishersMenu.Click += (s, e) => ExtinguishersReport();
            ExtinguishersMenu.Click += (s, e) => lastReport = () => ExtinguishersReport();

            HosesMenu.Click += (s, e) => HosesReport();
            HosesMenu.Click += (s, e) => lastReport = () => HosesReport();

            HydrantsMenu.Click += (s, e) => HydrantsReport();
            HydrantsMenu.Click += (s, e) => lastReport = () => HydrantsReport();

            RechargeExtinguishersMenu.Click += (s, e) => RechargeExtinguishersReport();
            RechargeExtinguishersMenu.Click += (s, e) => lastReport = () => RechargeExtinguishersReport();
        }

        /// <summary>
        /// Последний отчет.
        /// </summary>
        private Action lastReport;

        /// <summary>
        /// Вывод полного отчета.
        /// </summary>
        private void FullReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), fName, fName, fFireCabinetFault);
            listView.EntityReport(typeof(Extinguisher), fName, fParent, fExtinguisherFault);
            listView.EntityReport(typeof(Hose), fName, fParent, fHoseFault);
            listView.EntityReport(typeof(Hydrant), fName, fParent, fHydrantFault);
        }

        /// <summary>
        /// Отчет по пожарным шкафам.
        /// </summary>
        private void FireCabinetsReport()
        {
            listView.InitColumns("Тип", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), fName, fFireCabinetFault);
        }

        /// <summary>
        /// Отчет по огнетушителям.
        /// </summary>
        private void ExtinguishersReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Extinguisher), fName, fParent, fExtinguisherFault);
        }

        /// <summary>
        /// Отчет по рукавам.
        /// </summary>
        private void HosesReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hose), fName, fParent, fHoseFault);
        }

        /// <summary>
        /// Отчет по пожарным кранам.
        /// </summary>
        private void HydrantsReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hydrant), fName, fParent, fHydrantFault);
        }

        /// <summary>
        /// Отчет по перезарядке огнетушителей.
        /// </summary>
        private void RechargeExtinguishersReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Оставшийся срок (в месяцах)");
            listView.EntityReport(typeof(Extinguisher), fName, fParent, fExtinguisherRecharge);
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
            lastReport?.Invoke();
        }
    }
}