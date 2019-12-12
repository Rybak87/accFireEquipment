using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormEditEntity : Form
    {
        private readonly EntityController ec;
        private readonly BindingSource bindSource;
        private readonly Type entityType;
        public byte[] currPlan;
        private readonly List<Control> needControls = new List<Control>();
        private readonly EntityBase currEntity;
        private NumericUpDown weight;
        private NumericUpDown pressure;
        private readonly string mode;
        public event Action<EntityBase> EntityAdd;
        public event Action<EntityBase> EntityEdit;
        int yPosControl;
        public int CountCopy { get => (int)numCountCopy.Value; }

        public FormEditEntity(EntitySign sign)
        {
            InitializeComponent();
            ec = new EntityController();
            mode = "Edit";

            yPosControl = 1;
            currEntity = ec.GetEntity(sign);
            entityType = currEntity.GetType();

            numCountCopy.Visible = false;
            lblCountCopy.Visible = false;
            Text = "Редактирование: " + currEntity.ToString();
            if (currEntity.GetType() == typeof(Location))
                currPlan = ((Location)currEntity).Plan;
            bindSource = CreateBindSourse(currEntity, ec);
            CreateControls(currEntity);
        }
        public FormEditEntity(Type entityType, EntitySign parentSign)
        {
            InitializeComponent();
            ec = new EntityController();
            mode = "Add";

            yPosControl = 2;
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;

            if (entityType == typeof(Location))
            {
                ((INumber)currEntity).Number = ec.GetNumber(currEntity);
                currPlan = ((Location)currEntity).Plan;
            }
            else
            {
                currEntity.Parent = ec.GetEntity(parentSign);
                ((INumber)currEntity).Number = ec.GetNumberChild(currEntity.Parent, entityType);
            }
            Text = "Добавить"; 
            bindSource = CreateBindSourse(currEntity, ec);
            CreateControls(currEntity);
        }
        public FormEditEntity(Type entityType)
        {
            InitializeComponent();

            ec = new EntityController();
            mode = "AddType";

            yPosControl = 2;
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;
            Text = "Добавить новый тип";

            bindSource = CreateBindSourse(currEntity, ec);
            CreateControls(currEntity);
        }

        private BindingSource CreateBindSourse(EntityBase entity, EntityController ec)
        {
            var bindSource = new BindingSource
            {
                DataSource = ec.GetTable(entity.GetType()).Local////
            };
            if (bindSource.IndexOf(entity) < 1)
                bindSource.Add(entity);
            bindSource.Position = bindSource.IndexOf(entity);
            return bindSource;
        }
        private List<(PropertyInfo, ControlAttribute, string)> GetProperties(EntityBase entity)
        {
            var result = new List<(PropertyInfo, ControlAttribute, string)>();
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var controlAttr = GetControlAttribute(prop);
                if (controlAttr == null)
                    continue;

                bool controlHide = controlAttr.IsCanHide;
                if (controlHide)
                    continue;

                var nameAttr = GetColumnAttribute(prop)?.Name;
                result.Add((prop, controlAttr, nameAttr));
            }
            return result;

            ControlAttribute GetControlAttribute(PropertyInfo pi)
            {
                foreach (var item in pi.GetCustomAttributes())
                    if (item.GetType() == typeof(ControlAttribute))
                        return (ControlAttribute)item;
                return null;
            }
            ColumnAttribute GetColumnAttribute(PropertyInfo pi)
            {
                foreach (var item in pi.GetCustomAttributes())
                    if (item.GetType() == typeof(ColumnAttribute))
                        return (ColumnAttribute)item;
                return null;
            }
        }
        private void CreateControls(EntityBase entity)
        {

            List<(PropertyInfo prop, ControlAttribute attr, string name)> properties = GetProperties(entity);
            Control cntrl = null;

            foreach (var item in properties)
            {
                var prop = item.prop;
                var attr = item.attr;
                var name = item.name;
                if (name == null)
                    name = prop.Name;


                var lbl = new Label
                {
                    Text = name,
                    Location = new Point(25, 25 * yPosControl),
                    Size = new Size(175, 25)
                };
                Controls.Add(lbl);


                switch (attr.Control)
                {
                    case "TextBox":
                        {
                            cntrl = new TextBox
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25)
                            };
                            Controls.Add(cntrl);

                            cntrl.DataBindings.Add("Text", bindSource, prop.Name);
                            break;
                        }
                    case "ComboBox":
                        {
                            cntrl = new ComboBox
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                                Sorted = true
                            };
                            var parentEntity = ec.GetTableList(prop.PropertyType);

                            foreach (var item2 in parentEntity)
                                ((ComboBox)cntrl).Items.Add(item2);

                            var bind = new Binding("SelectedItem", bindSource, prop.Name, true, DataSourceUpdateMode.OnValidation);
                            cntrl.DataBindings.Add(bind);

                            Controls.Add(cntrl);
                            ((ComboBox)cntrl).SelectedIndex = -1;
                            if (prop.Name == "TypeExtinguisher")
                            {
                                ComboBox cbxType = (ComboBox)cntrl;
                                ((ComboBox)cntrl).SelectedIndexChanged += (s, e) => ComboBoxType_SelectedIndexChanged(cbxType);
                            }
                            break;
                        }
                    case "CheckBox":
                        {
                            cntrl = new CheckBox
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25)
                            };
                            cntrl.DataBindings.Add("Checked", bindSource, prop.Name);
                            Controls.Add(cntrl);
                            break;
                        }
                    case "NumericUpDown":
                        {
                            cntrl = new NumericUpDown
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                                Maximum = Int32.MaxValue
                            };
                            cntrl.DataBindings.Add("Value", bindSource, prop.Name);
                            Controls.Add(cntrl);
                            break;
                        }
                    case "NumericUpDownDecimal":
                        {
                            cntrl = new NumericUpDown
                            {
                                DecimalPlaces = 2,
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25)
                            };
                            cntrl.DataBindings.Add("Value", bindSource, prop.Name);
                            Controls.Add(cntrl);
                            if (entity is Extinguisher)
                            {
                                if (prop.Name == "Weight")
                                    weight = (NumericUpDown)cntrl;
                                else if (prop.Name == "Pressure")
                                    pressure = (NumericUpDown)cntrl;
                            }
                            break;
                        }
                    case "DateTimePicker":
                        {
                            cntrl = new DateTimePicker
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                            };
                            cntrl.DataBindings.Add("Value", bindSource, prop.Name);
                            Controls.Add(cntrl);
                            break;
                        }
                    case "Image":
                        {
                            cntrl = new Button
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(75, 25),
                                Text = "..."
                            };
                            var cntrl2 = new Button
                            {
                                Location = new Point(275, 25 * yPosControl),
                                Size = new Size(75, 25),
                                Text = "Удалить"
                            };
                            cntrl.Click += new EventHandler((s, e) => ImageDialog());
                            cntrl2.Click += new EventHandler((s, e) => ImageClear());
                            Controls.Add(cntrl);
                            Controls.Add(cntrl2);
                            break;
                        }
                    case null:
                        break;
                    default:
                        throw new ArgumentException();
                }

                if (attr.IsRequired)
                {
                    lbl = new Label
                    {
                        Text = "Обязательно заполнить",
                        AutoSize = true,
                        Location = new Point(400, 25 * yPosControl)
                    };
                    Controls.Add(lbl);
                    needControls.Add(cntrl);
                }
                yPosControl++;
            }
            this.Height = 25 * yPosControl + 100;
        }

        private void ComboBoxType_SelectedIndexChanged(ComboBox cntrl)
        {
            double weight = ((SpeciesExtinguisher)(cntrl).SelectedItem).NominalWeight;
            double pressure = ((SpeciesExtinguisher)(cntrl).SelectedItem).NominalPressure;
            cntrl.DataBindings[0].WriteValue();
            this.weight.Value = (decimal)weight;
            this.weight.DataBindings[0].WriteValue();
            this.pressure.Value = (decimal)pressure;
            this.pressure.DataBindings[0].WriteValue();
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

        private void BtnOK_Click(object sender, EventArgs e)
        {
            ec.entityAdd += EntityAdd;
            ec.entityEdit += EntityEdit;
            foreach (var cntrl in needControls)
            {
                switch (cntrl.GetType().Name)
                {

                    case "ComboBox":
                        if (((ComboBox)cntrl).SelectedIndex == -1)
                        {
                            AbortDialogResult();
                            return;
                        }
                        break;
                    case "NumericUpDown":
                        if (((NumericUpDown)cntrl).Value == 0)
                        {
                            AbortDialogResult();
                            return;
                        }
                        break;
                    case "TextBox":
                        if (((TextBox)cntrl).Text == "")
                        {
                            AbortDialogResult();
                            return;
                        }
                        break;
                }
            }
            if (entityType == typeof(Location))
            {
                ((Location)currEntity).Plan = currPlan;
                ((FormMain)Owner).picContainer.LoadImage(currPlan);
            }
            if (mode == "Add")
                ec.AddRangeEntity(currEntity, CountCopy);
            else if (mode == "Edit")
                ec.EditEntity(currEntity.GetSign());
            else if (mode == "AddType")
                ec.AddEntity(currEntity);

            void AbortDialogResult()
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Необходимо заполнить все поля");
            }
        }
    }
}
