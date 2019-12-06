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
    public partial class FormEditEntity2 : Form
    {
        //EntityController ec;
        Type entityType;
        EntitySign parentSign;
        public byte[] currImage;
        List<Control> needControls = new List<Control>();
        public EntityBase currEntity;
        NumericUpDown weight;
        NumericUpDown pressure;
        List<(PropertyInfo, Func<object>)> saveProperties = new List<(PropertyInfo, Func<object>)>();

        public FormEditEntity2(Type entityType, EntitySign parentSign)
        {
            InitializeComponent();
            CreateControls(entityType);
            this.entityType = entityType;
            this.parentSign = parentSign;
        }
        //public FormEditEntity2(EntityBase currEntity, EntityController ec, bool someComboBoxHide)
        //{
        //    InitializeComponent();
        //    typeEntity = currEntity.GetType();
        //    this.ec = ec;
        //    this.currEntity = currEntity;
        //    if (currEntity.GetType() == typeof(Location))
        //        currImage = ((Location)currEntity).Image;
        //    CreateControls(currEntity, someComboBoxHide);
        //}

        private List<(PropertyInfo, ControlAttribute, string)> GetProperties(Type entityType)
        {
            var result = new List<(PropertyInfo, ControlAttribute, string)>();
            foreach (PropertyInfo prop in entityType.GetProperties())
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
        private void CreateControls(Type entityType)
        {
            int yPosControl = 1;
            List<(PropertyInfo prop, ControlAttribute attr, string name)> properties = GetProperties(entityType);
            //Control cntrl = null;

            foreach (var item in properties)
            {
                var prop = item.prop;
                var attr = item.attr;
                var name = item.name;
                if (name == null)
                    name = prop.Name;
                CreateLabelNameProperty(yPosControl, name);

                switch (attr.Control)
                {
                    case "TextBox":
                        {
                            var cntrl = new TextBox
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25)
                            };
                            Controls.Add(cntrl);

                            //cntrl.DataBindings.Add("Text", bindSource, prop.Name);
                            saveProperties.Add((prop, () => cntrl.Text));
                            break;
                        }
                    case "ComboBox":
                        {
                            var cntrl = new ComboBox
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                                Sorted = true
                            };
                            List<EntityBase> parentEntity;
                            using (var ec = new EntityController())
                            {
                                parentEntity = ec.GetTableList(prop.PropertyType);
                            }
                                

                            foreach (var item2 in parentEntity)
                                ((ComboBox)cntrl).Items.Add(item2);

                            //var bind = new Binding("SelectedItem", bindSource, prop.Name, true, DataSourceUpdateMode.OnValidation);
                            //cntrl.DataBindings.Add(bind);
                            saveProperties.Add((prop, () => (((ComboBox)cntrl).SelectedItem)));

                            Controls.Add(cntrl);
                            //((ComboBox)cntrl).SelectedIndex = -1;
                            //if (prop.Name == "TypeExtinguisher")
                            //{
                            //    ComboBox cbxType = (ComboBox)cntrl;
                            //    ((ComboBox)cntrl).SelectedIndexChanged += (s, e) => ComboBoxType_SelectedIndexChanged(cbxType);
                            //}
                            break;
                        }
                    case "CheckBox":
                        {
                            var cntrl = new CheckBox
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25)
                            };
                            saveProperties.Add((prop, () => ((CheckBox)cntrl).Checked));
                            //cntrl.DataBindings.Add("Checked", bindSource, prop.Name);
                            Controls.Add(cntrl);
                            break;
                        }
                    case "NumericUpDown":
                        {
                            var cntrl = new NumericUpDown
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                                Maximum = Int32.MaxValue
                            };
                            saveProperties.Add((prop, () => (int)((NumericUpDown)cntrl).Value));
                            //cntrl.DataBindings.Add("Value", bindSource, prop.Name);
                            Controls.Add(cntrl);
                            break;
                        }
                    case "NumericUpDownDecimal":
                        {
                            var cntrl = new NumericUpDown
                            {
                                DecimalPlaces = 2,
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25)
                            };
                            //cntrl.DataBindings.Add("Value", bindSource, prop.Name);
                            saveProperties.Add((prop, () => (double)((NumericUpDown)cntrl).Value));
                            Controls.Add(cntrl);
                            //if (entity is Extinguisher)
                            //{
                            //    if (prop.Name == "Weight")
                            //        weight = (NumericUpDown)cntrl;
                            //    else if (prop.Name == "Pressure")
                            //        pressure = (NumericUpDown)cntrl;
                            //}
                            break;
                        }
                    case "DateTimePicker":
                        {
                            var cntrl = new DateTimePicker
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                            };
                            //cntrl.DataBindings.Add("Value", bindSource, prop.Name);
                            saveProperties.Add((prop, ()=>((DateTimePicker)cntrl).Value));
                            Controls.Add(cntrl);
                            break;
                        }
                    //case "Image":
                    //    {
                    //        cntrl = new Button
                    //        {
                    //            Location = new Point(200, 25 * yPosControl),
                    //            Size = new Size(75, 25),
                    //            Text = "..."
                    //        };
                    //        var cntrl2 = new Button
                    //        {
                    //            Location = new Point(275, 25 * yPosControl),
                    //            Size = new Size(75, 25),
                    //            Text = "Удалить"
                    //        };
                    //        cntrl.Click += new EventHandler((s, e) => ImageDialog((Location)entity));
                    //        cntrl2.Click += new EventHandler((s, e) => ImageClear((Location)entity));
                    //        Controls.Add(cntrl);
                    //        Controls.Add(cntrl2);
                    //        break;
                    //    }
                    case null:
                        break;
                    //default:
                    //    throw new ArgumentException();
                }

                //if (attr.IsRequired)
                //{
                //    var lbl = new Label
                //    {
                //        Text = "Обязательно заполнить",
                //        AutoSize = true,
                //        Location = new Point(400, 25 * yPosControl)
                //    };
                //    Controls.Add(lbl);
                //    needControls.Add(cntrl);
                //}
                yPosControl++;
            }
            this.Height = 25 * yPosControl + 100;
        }

        private void CreateLabelNameProperty(int yPosControl, string name)
        {
            var lbl = new Label
            {
                Text = name,
                Location = new Point(25, 25 * yPosControl),
                Size = new Size(175, 25)
            };
            Controls.Add(lbl);
        }

        private void ComboBoxType_SelectedIndexChanged(ComboBox cntrl)
        {
            double weight = ((TypeExtinguisher)(cntrl).SelectedItem).NominalWeight;
            double pressure = ((TypeExtinguisher)(cntrl).SelectedItem).NominalPressure;
            this.weight.Value = (decimal)weight;
            this.weight.DataBindings[0].WriteValue();
            this.pressure.Value = (decimal)pressure;
            this.pressure.DataBindings[0].WriteValue();
        }
        private void ImageDialog(Location entity)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(dialog.FileName);
                currImage = data;
            }
            else
            {
                currImage = entity.Image;
            }
        }
        private void ImageClear(Location entity)
        {
            currImage = null;
        }
        private void BtnOK_Click(object sender, EventArgs e)
        {
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
            using (var ec = new EntityController())
            {
                currEntity = ec.CreateEntity(entityType);
                foreach (var item in saveProperties)
                    item.Item1.SetValue(currEntity, item.Item2());
            }

            //if (typeEntity == typeof(Location))
            //{
            //    ((Location)currEntity).Image = currImage;
            //    ((FormMain)Owner).picContainer.LoadImage(currImage);
            //}

            void AbortDialogResult()
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Необходимо заполнить все поля");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object x;
            foreach (var item in saveProperties)
                x = item.Item2();
        }
    }
}
