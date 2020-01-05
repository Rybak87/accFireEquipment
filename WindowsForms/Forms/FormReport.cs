using BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormReport : Form
    {
        public event Action<EntitySign> EditEntity;
        private FilterSet filterName = HelperListView.filterName;
        private FilterSet filterParent = HelperListView.filterParent;
        private FilterSet filterLocation = HelperListView.filterLocation;
        private FilterSet filterFireCabinetFault = HelperListView.filterFireCabinetFault;
        private FilterSet filterExtinguisherFault = HelperListView.filterExtinguisherFault;
        private FilterSet filterHoseFault = HelperListView.filterHoseFault;
        private FilterSet filterHydrantFault = HelperListView.filterHydrantFault;
        private FilterSet filterExtinguisherRecharge = HelperListView.filterExtinguisherRecharge;

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
            FireCabinetsMenu.Click += (s, e) => FireCabinetsReport();
            ExtinguishersMenu.Click += (s, e) => ExtinguishersReport();
            HosesMenu.Click += (s, e) => HosesReport();
            HydrantsMenu.Click += (s, e) => HydrantsReport();
            RechargeExtinguishersMenu.Click += (s, e) => RechargeExtinguishersReport();
        }
        private void FullReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), filterLocation, filterName, filterName, filterFireCabinetFault);
            listView.EntityReport(typeof(Extinguisher), filterLocation, filterName, filterParent, filterExtinguisherFault);
            listView.EntityReport(typeof(Hose), filterLocation, filterName, filterParent, filterHoseFault);
            listView.EntityReport(typeof(Hydrant), filterLocation, filterName, filterParent, filterHydrantFault);
        }
        private void FireCabinetsReport()
        {
            InitColumns("Тип", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), filterLocation, filterName, filterFireCabinetFault);
        }
        private void ExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Extinguisher), filterLocation, filterName, filterParent, filterExtinguisherFault);
        }
        private void HosesReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hose), filterLocation, filterName, filterParent, filterHoseFault);
        }
        private void HydrantsReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hydrant), filterLocation, filterName, filterParent, filterHydrantFault);
        }
        private void RechargeExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Оставшийся срок (в месяцах)");
            listView.EntityReport(typeof(Extinguisher), filterLocation, filterName, filterParent, filterExtinguisherRecharge);
        }
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
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var item = listView.SelectedItems[0];
            var sign = (EntitySign)item.Tag;
            EditEntity?.Invoke(sign);
        }
    }
}