using BL;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormWorkExtinguisher : FormWorkEquipment
    {
        /// <summary>
        /// Вес.
        /// </summary>
        private NumericUpDown numWeight;

        /// <summary>
        /// Давление.
        /// </summary>
        private NumericUpDown numPressure;

        public FormWorkExtinguisher(Type childType, EntitySign parentSign) : base(childType, parentSign)
        {
            InitializeComponent();
            Text = Strategy.GetFormName(currEntity);
        }

        public FormWorkExtinguisher(EntitySign sign):base(sign)
        {
            InitializeComponent();
            Text = Strategy.GetFormName(currEntity);
        }

        protected override ComboBox CreateComboBox(Size fullSize, PropertyInfo prop, Point centerLocation)
        {
            var cbx = base.CreateComboBox(fullSize, prop, centerLocation);
            (cbx).SelectedIndexChanged += (s, e) => GetWeightPressure(cbx);
            return cbx;
        }

        protected override NumericUpDown CreateNumericUpDownDecimal(Size fullSize, PropertyInfo prop, Point location)
        {
            var num = base.CreateNumericUpDownDecimal(fullSize, prop, location);

            if (prop.Name == "Weight")
                numWeight = num;
            else if (prop.Name == "Pressure")
                numPressure = num;
            return num;
        }

        protected void GetWeightPressure(ComboBox cntrl)
        {
            double weight = ((KindExtinguisher)cntrl.SelectedItem).NominalWeight;
            double pressure = ((KindExtinguisher)cntrl.SelectedItem).NominalPressure;
            cntrl.DataBindings[0].WriteValue();
            numWeight.Value = (decimal)weight;
            numPressure.Value = (decimal)pressure;
            //this.weight.DataBindings[0].WriteValue();
            //this.pressure.DataBindings[0].WriteValue();
        }

    }
}
