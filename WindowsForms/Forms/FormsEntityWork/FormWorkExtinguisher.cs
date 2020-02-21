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

        /// <summary>
        /// Вид огнетушителя.
        /// </summary>
        private ComboBox kindComboBox;

        /// <summary>
        /// Конструктор по добавлению нового огнетушителя.
        /// </summary>
        /// <param name="childType"></param>
        /// <param name="parentSign"></param>
        /// <param name="strategy"></param>
        public FormWorkExtinguisher(Type childType, EntitySign parentSign, Strategy strategy) : base(childType, parentSign, strategy)
        {
            InitializeComponent();
            Text = strategy.GetFormName(currEntity);
            Load += (s, e) => GetWeightPressure(kindComboBox);
        }

        /// <summary>
        /// Конструктор по редактирования огнетушителя.
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="strategy"></param>
        public FormWorkExtinguisher(EntitySign sign, Strategy strategy) : base(sign, strategy)
        {
            InitializeComponent();
            Text = strategy.GetFormName(currEntity);
        }

        /// <summary>
        /// Создание ComboBox
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="centerLocation">Расположение.</param>
        /// <returns></returns>
        protected override ComboBox CreateComboBox(PropertyInfo prop, Size fullSize, Point centerLocation)
        {
            var cbx = base.CreateComboBox(prop, fullSize, centerLocation);
            (cbx).SelectedIndexChanged += (s, e) => GetWeightPressure(cbx);
            kindComboBox = cbx;
            return cbx;
        }

        /// <summary>
        /// Создание NumericUpDownDecimal
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
        protected override NumericUpDown CreateNumericUpDownDecimal(PropertyInfo prop, Size fullSize, Point location)
        {
            var num = base.CreateNumericUpDownDecimal(prop, fullSize, location);

            if (prop.Name == "Weight")
                numWeight = num;
            else if (prop.Name == "Pressure")
                numPressure = num;
            return num;
        }

        /// <summary>
        /// Загрузка типовых массы и давления.
        /// </summary>
        /// <param name="cntrl">ComboBox определяющий вид огнетушителя.</param>
        protected void GetWeightPressure(ComboBox cntrl)
        {
            double weight = ((KindExtinguisher)cntrl.SelectedItem).NominalWeight;
            double pressure = ((KindExtinguisher)cntrl.SelectedItem).NominalPressure;
            cntrl.DataBindings[0].WriteValue();
            numWeight.Value = (decimal)weight;
            numPressure.Value = (decimal)pressure;
        }
    }
}
