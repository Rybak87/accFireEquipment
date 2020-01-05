using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormEntity : Form
    {
        protected readonly EntityController ec = new EntityController();
        private readonly List<Control> requiredControls = new List<Control>();
        protected Type entityType;
        protected EntityBase currEntity;
        public byte[] currPlan;

        private NumericUpDown weight;
        private NumericUpDown pressure;

        public FormEntity()
        {
            InitializeComponent();
        }
        protected virtual void CreateControls(int yPosControl)
        {
            List<(PropertyInfo prop, ControlAttribute attr, string name)> properties = ec.GetEditProperties(currEntity);
            Control cntrl = null;
            ComboBox cbxTypeExtinguisher = null;
            var incSize = new Size(175, 25);
            var fullSize = new Size(150, 25);
            var halfSize = new Size(75, 25);
            foreach (var item in properties)
            {
                var prop = item.prop;
                var attr = item.attr;
                var name = item.name;
                var leftLocation = new Point(25, yPosControl);
                var centerLocation = new Point(200, yPosControl);
                var centerHalfLocation = new Point(275, yPosControl);
                var rightLocation = new Point(400, yPosControl);
                if (name == null)
                    name = prop.Name;

                var lbl = new Label
                {
                    Text = name,
                    Location = leftLocation,
                    Size = incSize
                };
                Controls.Add(lbl);

                switch (attr.Control)
                {
                    case "TextBox":
                        {
                            cntrl = CreateTextBox(fullSize, prop, centerLocation);
                            break;
                        }
                    case "ComboBox":
                        {
                            cntrl = CreateComboBox(fullSize, prop, centerLocation);
                            if (prop.Name == "TypeExtinguisher")
                                cbxTypeExtinguisher = (ComboBox)cntrl;
                            break;
                        }
                    case "CheckBox":
                        {
                            cntrl = CreateCheckBox(fullSize, prop, centerLocation);
                            break;
                        }
                    case "NumericUpDown":
                        {
                            cntrl = CreateNumericUpDown(fullSize, prop, centerLocation);
                            break;
                        }
                    case "NumericUpDownDecimal":
                        {
                            cntrl = CreateNumericUpDownDecimal(fullSize, prop, centerLocation);
                            break;
                        }
                    case "DateTimePicker":
                        {
                            cntrl = CreateDateTimePicker(fullSize, prop, centerLocation);
                            break;
                        }
                    case "Image":
                        {
                            cntrl = CreateButtonsForImage(halfSize, centerLocation, centerHalfLocation);
                            break;
                        }
                }

                if (attr.IsRequired)
                {
                    lbl = new Label
                    {
                        Text = "Обязательно заполнить",
                        AutoSize = true,
                        Location = rightLocation
                    };
                    Controls.Add(lbl);
                    requiredControls.Add(cntrl);
                }
                yPosControl += 25;
            }
            this.Height = yPosControl + 100;
            //if (cbxTypeExtinguisher != null)
            //    GetWeightPressure(cbxTypeExtinguisher);
        }

        private Control CreateButtonsForImage(Size halfSize, Point centerLocation, Point centerHalfLocation)
        {
            Control cntrl = new Button
            {
                Location = centerLocation,
                Size = halfSize,
                Text = "..."
            };
            var cntrl2 = new Button
            {
                Location = centerHalfLocation,
                Size = halfSize,
                Text = "Удалить"
            };
            cntrl.Click += new EventHandler((s, e) => ImageDialog());
            cntrl2.Click += new EventHandler((s, e) => ImageClear());
            Controls.Add(cntrl);
            Controls.Add(cntrl2);
            return cntrl;
        }
        private Control CreateComboBox(Size fullSize, PropertyInfo prop, Point centerLocation)
        {
            Control cntrl = new ComboBox
            {
                Location = centerLocation,
                Size = fullSize,
                Sorted = true
            };
            var parents = ec.Set(prop.PropertyType);
            parents.Load();
            ((ComboBox)cntrl).DataSource = parents.Local;

            Controls.Add(cntrl);
            var bind = new Binding("SelectedItem", currEntity, prop.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            cntrl.DataBindings.Add(bind);
            cntrl.DataBindings[0].WriteValue();
            if (prop.Name == "TypeExtinguisher")
            {
                ((ComboBox)cntrl).SelectedIndexChanged += (s, e) => GetWeightPressure((ComboBox)cntrl);
            }
            return cntrl;
        }
        private Control CreateNumericUpDownDecimal(Size fullSize, PropertyInfo prop, Point location)
        {
            Control cntrl = new NumericUpDown
            {
                DecimalPlaces = 2,
                Location = location,
                Size = fullSize
            };
            if (entityType == typeof(Extinguisher))
            {
                if (prop.Name == "Weight")
                    weight = (NumericUpDown)cntrl;
                else if (prop.Name == "Pressure")
                    pressure = (NumericUpDown)cntrl;
            }
            Controls.Add(cntrl);
            cntrl.DataBindings.Add("Value", currEntity, prop.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            return cntrl;
        }
        private Control CreateDateTimePicker(Size fullSize, PropertyInfo prop, Point location)
        {
            Control cntrl = new DateTimePicker
            {
                Location = location,
                Size = fullSize,
            };
            Controls.Add(cntrl);
            cntrl.DataBindings.Add("Value", currEntity, prop.Name);
            return cntrl;
        }
        private Control CreateNumericUpDown(Size fullSize, PropertyInfo prop, Point location)
        {
            Control cntrl = new NumericUpDown
            {
                Location = location,
                Size = fullSize,
                Maximum = Int32.MaxValue
            };
            Controls.Add(cntrl);
            cntrl.DataBindings.Add("Value", currEntity, prop.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            return cntrl;
        }
        private Control CreateCheckBox(Size fullSize, PropertyInfo prop, Point location)
        {
            Control cntrl = new CheckBox
            {
                Location = location,
                Size = fullSize
            };
            Controls.Add(cntrl);
            cntrl.DataBindings.Add("Checked", currEntity, prop.Name);
            return cntrl;
        }
        private Control CreateTextBox(Size fullSize, PropertyInfo prop, Point location)
        {
            Control cntrl = new TextBox
            {
                Location = location,
                Size = fullSize
            };
            Controls.Add(cntrl);
            cntrl.DataBindings.Add("Text", currEntity, prop.Name);
            return cntrl;
        }

        private void GetWeightPressure(ComboBox cntrl)
        {
            double weight = ((SpeciesExtinguisher)cntrl.SelectedItem).NominalWeight;
            double pressure = ((SpeciesExtinguisher)cntrl.SelectedItem).NominalPressure;
            cntrl.DataBindings[0].WriteValue();
            this.weight.Value = (decimal)weight;
            this.pressure.Value = (decimal)pressure;
        }
        private void ImageDialog()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var data = File.ReadAllBytes(dialog.FileName);
                    currPlan = data;
                }
            }
        }
        private void ImageClear()
        {
            currPlan = null;
        }

        private bool EmptyNeedControls()
        {
            foreach (var cntrl in requiredControls)
            {
                switch (cntrl.GetType().Name)
                {

                    case "ComboBox":
                        if (((ComboBox)cntrl).SelectedIndex == -1)
                            return true;
                        break;
                    case "NumericUpDown":
                        if (((NumericUpDown)cntrl).Value == 0)
                            return true;
                        break;
                    case "TextBox":
                        if (((TextBox)cntrl).Text == "")
                            return true;
                        break;
                }
            }
            return false;
        }
        protected virtual void BtnOK_Click(object sender, EventArgs e)
        {
            //ec.entityAdd += EntityAdd;
            if (EmptyNeedControls())
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }

            if (entityType == typeof(Location))
            {
                ((Location)currEntity).Plan = currPlan;
                ((FormMain)Owner).picContainer.LoadImage(currPlan);
            }
        }
    }
}
