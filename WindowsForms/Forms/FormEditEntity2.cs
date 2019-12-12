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

                            saveProperties.Add((prop, () => cntrl.Text));
                            LabelRequired(yPosControl, attr, cntrl);
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
                            List<EntityBase> entityTypes;
                            using (var ec = new EntityController())
                            {
                                entityTypes = ec.GetTableList(prop.PropertyType);
                            }


                            foreach (var item2 in entityTypes)
                                ((ComboBox)cntrl).Items.Add(item2);

                            var newProp = entityType.GetProperty(prop.Name + "Id");
                            saveProperties.Add((newProp, () => ((EntityBase)cntrl.SelectedItem).Id));
                            Controls.Add(cntrl);
                            if (prop.Name == "TypeExtinguisher")
                            {
                                ComboBox cbxType = (ComboBox)cntrl;
                                ((ComboBox)cntrl).SelectedIndexChanged += (s, e) => ComboBoxType_SelectedIndexChanged(cbxType);
                            }
                            LabelRequired(yPosControl, attr, cntrl);
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
                            Controls.Add(cntrl);
                            LabelRequired(yPosControl, attr, cntrl);
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
                            Controls.Add(cntrl);
                            LabelRequired(yPosControl, attr, cntrl);
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
                            saveProperties.Add((prop, () => (double)((NumericUpDown)cntrl).Value));
                            Controls.Add(cntrl);
                            if (entityType == typeof(Extinguisher))
                            {
                                if (prop.Name == "Weight")
                                    weight = (NumericUpDown)cntrl;
                                else if (prop.Name == "Pressure")
                                    pressure = (NumericUpDown)cntrl;
                            }
                            LabelRequired(yPosControl, attr, cntrl);
                            break;
                        }
                    case "DateTimePicker":
                        {
                            var cntrl = new DateTimePicker
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                            };
                            saveProperties.Add((prop, () => ((DateTimePicker)cntrl).Value));
                            Controls.Add(cntrl);
                            LabelRequired(yPosControl, attr, cntrl);
                            break;
                        }
                    case "Image":
                        {
                            var cntrl = new Button
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
                            saveProperties.Add((prop, () => currImage));
                            cntrl.Click += new EventHandler((s, e) => ImageDialog());
                            cntrl2.Click += new EventHandler((s, e) => ImageClear());
                            Controls.Add(cntrl);
                            Controls.Add(cntrl2);
                            break;
                        }
                }

                //LabelRequired(yPosControl, attr);
                yPosControl++;
            }
            this.Height = 25 * yPosControl + 100;
        }

        private void LabelRequired(int yPosControl, ControlAttribute attr, Control cntrl)
        {
            if (attr.IsRequired)
            {
                var lbl = new Label
                {
                    Text = "Обязательно заполнить",
                    AutoSize = true,
                    Location = new Point(400, 25 * yPosControl)
                };
                Controls.Add(lbl);
                needControls.Add(cntrl);
            }
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
            this.pressure.Value = (decimal)pressure;
        }
        private void ImageDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(dialog.FileName);
                currImage = data;
            }
        }
        private void ImageClear()
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
                currEntity.Parent = ec.GetEntity(parentSign);
                foreach (var item in saveProperties)
                    item.Item1.SetValue(currEntity, item.Item2());
                //EntityBase entity = (EntityBase)ec.GetTable(entityType).Attach(currEntity);
                ec.AddNewEntity(currEntity);
                ec.SaveChanges();
                var g = ec.CopyEntity(currEntity.GetSign());
                //ec.Entry(g).State = EntityState.Detached;
                //ec.SaveChanges();
                //EntityBase g2 = (EntityBase)ec.GetTable(entityType).Attach(g);
                ec.AddNewEntity(g);
                ec.SaveChanges();
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
