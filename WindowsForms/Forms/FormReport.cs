using BL;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormReport : Form
    {
        private Filter fName = HelperListView.filterName;
        private Filter fParent = HelperListView.filterParent;
        private Filter fLocation = HelperListView.filterLocation;
        private Filter fFireCabinetFault = HelperListView.filterFireCabinetFault;
        private Filter fExtinguisherFault = HelperListView.filterExtinguisherFault;
        private Filter fHoseFault = HelperListView.filterHoseFault;
        private Filter fHydrantFault = HelperListView.filterHydrantFault;
        private Filter fExtinguisherRecharge = HelperListView.filterExtinguisherRecharge;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormReport()
        {
            InitializeComponent();
            FullMenu.Image = ImageSettings.IconsImage(typeof(Location));
            FireCabinetsMenu.Image = ImageSettings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));
            HosesMenu.Image = ImageSettings.IconsImage(typeof(Hose));
            HydrantsMenu.Image = ImageSettings.IconsImage(typeof(Hydrant));
            RechargeExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));

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
        /// Пос
        /// </summary>
        private Action lastReport;

        /// <summary>
        /// Событие по двойному клику мышью.
        /// </summary>
        public event Action<EntitySign> ListViewDoubleClick;

        /// <summary>
        /// Вывод полного отчета.
        /// </summary>
        private void FullReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), fLocation, fName, fName, fFireCabinetFault);
            listView.EntityReport(typeof(Extinguisher), fLocation, fName, fParent, fExtinguisherFault);
            listView.EntityReport(typeof(Hose), fLocation, fName, fParent, fHoseFault);
            listView.EntityReport(typeof(Hydrant), fLocation, fName, fParent, fHydrantFault);
        }

        /// <summary>
        /// Отчет по пожарным шкафам.
        /// </summary>
        private void FireCabinetsReport()
        {
            listView.InitColumns("Тип", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), fLocation, fName, fFireCabinetFault);
        }

        /// <summary>
        /// Отчет по огнетушителям.
        /// </summary>
        private void ExtinguishersReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Extinguisher), fLocation, fName, fParent, fExtinguisherFault);
        }

        /// <summary>
        /// Отчет по рукавам.
        /// </summary>
        private void HosesReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hose), fLocation, fName, fParent, fHoseFault);
        }

        /// <summary>
        /// Отчет по пожарным кранам.
        /// </summary>
        private void HydrantsReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hydrant), fLocation, fName, fParent, fHydrantFault);
        }

        /// <summary>
        /// Отчет по перезарядке огнетушителей.
        /// </summary>
        private void RechargeExtinguishersReport()
        {
            listView.InitColumns("Тип", "Пожарный шкаф", "Оставшийся срок (в месяцах)");
            listView.EntityReport(typeof(Extinguisher), fLocation, fName, fParent, fExtinguisherRecharge);
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
            ListViewDoubleClick?.Invoke(sign);
            lastReport?.Invoke();
        }
    }
}