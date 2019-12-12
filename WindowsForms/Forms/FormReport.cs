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
        private FilterSet filterName = new FilterSet(ent => ent.ToString());
        private FilterSet filterParent = new FilterSet(ent => ent.Parent.ToString());
        private FilterSet filterParentParent = new FilterSet(ent => ent.Parent.Parent.ToString());
        private FilterSet filterFireCabinetFault = new FilterSet(true,
                                    new Filter(ent => ((FireCabinet)ent).IsDented, ent => "Поврежден; "),
                                    new Filter(ent => !((FireCabinet)ent).IsSticker, ent => "Без наклейки; ")
                                    );
        private FilterSet filterExtinguisherFault = new FilterSet(true,
                                    new Filter(ent => ((Extinguisher)ent).IsDented, ent => "Поврежден; "),
                                    new Filter(ent => !((Extinguisher)ent).IsSticker, ent => "Без наклейки; "),
                                    new Filter(ent => !((Extinguisher)ent).IsHose, ent => "Нет шланга; "),
                                    new Filter(ent => ((Extinguisher)ent).IsLabelDamage, ent => "Повреждена этикетка; "),
                                    new Filter(ent => ((Extinguisher)ent).IsPaintDamage, ent => "Повреждена краска; "),
                                    new Filter(ent => ((Extinguisher)ent).IsPressureGaugeFault, ent => "Поврежден манометр; "),
                                    new Filter(ent => ((Extinguisher)ent).Pressure < ((Extinguisher)ent).TypeExtinguisher.MinPressure, ent => "Давление менее допустимого; "),
                                    new Filter(ent => ((Extinguisher)ent).Weight < ((Extinguisher)ent).TypeExtinguisher.MinWeight, ent => "Вес менее допустимого; ")
                                    );
        private FilterSet filterHoseFault = new FilterSet(true,
                                    new Filter(ent => ((Hose)ent).IsRagged, ent => "Порван; "),
                                    new Filter(ent => ((Hose)ent).DateRolling.Subtract(DateTime.Now).Days < 30, ent => "Необходима перекатка; ")
                                    );
        private FilterSet filterHydrantFault = new FilterSet(ent => ((Hydrant)ent).IsDamage, ent => "Поврежден; ", true);
        private FilterSet filterExtinguisherRecharge = new FilterSet(true,
            new Filter(ent => ((Extinguisher)ent).DateRecharge.Subtract(DateTime.Now).Days < 365, ent => ((Extinguisher)ent).DateRecharge.SubtractMonths(DateTime.Now).ToString())
        );

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
            listView.EntityReport(typeof(FireCabinet), filterParent, filterName, filterName, filterFireCabinetFault);
            listView.EntityReport(typeof(Extinguisher), filterParentParent, filterName, filterParent, filterExtinguisherFault);
            listView.EntityReport(typeof(Hose), filterParentParent, filterName, filterParent, filterHoseFault);
            listView.EntityReport(typeof(Hydrant), filterParentParent, filterName, filterParent, filterHydrantFault);
        }
        private void FireCabinetsReport()
        {
            InitColumns("Тип", "Недостатки");
            listView.EntityReport(typeof(FireCabinet), filterParent, filterName, filterFireCabinetFault);
        }
        private void ExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Extinguisher), filterParentParent, filterName, filterParent, filterExtinguisherFault);
        }
        private void HosesReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hose), filterParentParent, filterName, filterParent, filterHoseFault);
        }
        private void HydrantsReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Недостатки");
            listView.EntityReport(typeof(Hydrant), filterParentParent, filterName, filterParent, filterHydrantFault);
        }
        private void RechargeExtinguishersReport()
        {
            InitColumns("Тип", "Пожарный шкаф", "Оставшийся срок (в месяцах)");
            listView.EntityReport(typeof(Extinguisher), filterParentParent, filterName, filterParent, filterExtinguisherRecharge);
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