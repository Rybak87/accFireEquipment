using BL;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormReport : Form
    {
        private FilterSet fName = HelperListView.filterName;
        private FilterSet fParent = HelperListView.filterParent;
        private FilterSet fLocation = HelperListView.filterLocation;
        private FilterSet fFireCabinetFault = HelperListView.filterFireCabinetFault;
        private FilterSet fExtinguisherFault = HelperListView.filterExtinguisherFault;
        private FilterSet fHoseFault = HelperListView.filterHoseFault;
        private FilterSet fHydrantFault = HelperListView.filterHydrantFault;
        private FilterSet fExtinguisherRecharge = HelperListView.filterExtinguisherRecharge;

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
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
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
            InitColumns("Тип", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), fLocation, fName, fFireCabinetFault);
        }

        /// <summary>
        /// Отчет по огнетушителям.
        /// </summary>
        private void ExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Extinguisher), fLocation, fName, fParent, fExtinguisherFault);
        }

        /// <summary>
        /// Отчет по рукавам.
        /// </summary>
        private void HosesReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hose), fLocation, fName, fParent, fHoseFault);
        }

        /// <summary>
        /// Отчет по пожарным кранам.
        /// </summary>
        private void HydrantsReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hydrant), fLocation, fName, fParent, fHydrantFault);
        }

        /// <summary>
        /// Отчет по перезарядке огнетушителей.
        /// </summary>
        private void RechargeExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Оставшийся срок (в месяцах)");
            listView.EntityReport(typeof(Extinguisher), fLocation, fName, fParent, fExtinguisherRecharge);
        }

        /// <summary>
        /// Инициализация колонок в ListView.
        /// </summary>
        /// <param name="columnsNames"></param>
        private void InitColumns(params string[] columnsNames)
        {
            listView.Clear();
            listView.Groups.Clear();
            var countColumns = columnsNames.Count();
            var columnWidth = Width / countColumns - 10;
            var columnHeaders = new ColumnHeader[countColumns];
            int count = 0;
            foreach (var name in columnsNames)
            {
                var columnHeader = new ColumnHeader()
                {
                    Text = name,
                    Width = columnWidth
                };
                columnHeaders[count] = columnHeader;
                count++;
            }
            listView.Columns.AddRange(columnHeaders);
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