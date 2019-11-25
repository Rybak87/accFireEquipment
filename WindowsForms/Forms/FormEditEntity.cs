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
        EntityController ec;
        BindingSource bindSource;
        Type typeEntity;
        public byte[] currImage;
        //public Image currImage;
        List<Control> needControls = new List<Control>();
        EntityBase currEntity;

        public FormEditEntity(EntityBase currEntity, EntityController ec) : this(currEntity, ec, false)
        {
        }
        public FormEditEntity(EntityBase currEntity, EntityController ec, bool someComboBoxHide)
        {
            InitializeComponent();
            typeEntity = currEntity.GetType();
            this.ec = ec;
            this.currEntity = currEntity;
            //byte[] locImage=null;
            if (currEntity.GetType() == typeof(Location))
                //currImage = ((Location)currEntity).Image?.Image;
                currImage = ((Location)currEntity).Image;
            //    locImage = ((Location)currEntity).Image;
            //if (locImage != null)
            //    currImage = Image.FromStream(new MemoryStream(((Location)currEntity).Image));

            bindSource = CreateBindSourse(currEntity, ec);
            CreateControls(currEntity, someComboBoxHide);
        }

        private BindingSource CreateBindSourse(EntityBase entity, EntityController ec)
        {
            var bindSource = new BindingSource();
            bindSource.DataSource = ec.GetCollection(entity.GetType()).Local;////
            if (bindSource.IndexOf(entity) < 1)
                bindSource.Add(entity);
            bindSource.Position = bindSource.IndexOf(entity);
            return bindSource;
        }
        private List<(PropertyInfo, ControlAttribute, string)> GetProperties(EntityBase entity, bool someComboBoxHide)
        {
            var result = new List<(PropertyInfo, ControlAttribute, string)>();
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var controlAttr = GetControlAttribute(prop);
                if (controlAttr == null)
                    continue;

                bool controlHide = controlAttr.IsCanHide && someComboBoxHide;
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
        private void CreateControls(EntityBase entity, bool someComboBoxHide)
        {
            int yPosControl = 1;
            List<(PropertyInfo prop, ControlAttribute attr, string name)> properties = GetProperties(entity, someComboBoxHide);
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
                            var parentEntity = ec.GetCollectionList(prop.PropertyType);

                            foreach (var item2 in parentEntity)
                                ((ComboBox)cntrl).Items.Add(item2);

                            var bind = new Binding("SelectedItem", bindSource, prop.Name, true, DataSourceUpdateMode.OnValidation);
                            cntrl.DataBindings.Add(bind);

                            Controls.Add(cntrl);
                            ((ComboBox)cntrl).SelectedIndex = -1;
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
                            break;
                        }
                    case "DateTimePicker":
                        {
                            cntrl = new DateTimePicker
                            {
                                Location = new Point(200, 25 * yPosControl),
                                Size = new Size(150, 25),
                                //Format = DateTimePickerFormat.Short
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
                            cntrl.Click += new EventHandler((s, e) => ImageDialog((Location)entity));
                            cntrl2.Click += new EventHandler((s, e) => ImageClear((Location)entity));
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
            if (typeEntity == typeof(Location))
            {
                ((Location)currEntity).Image = currImage;
                ((FormMain)Owner).picContainer.LoadImage(currImage);
            }

            void AbortDialogResult()
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Необходимо заполнить все поля");
            }
        }

        private void FormEditEntity_Load(object sender, EventArgs e)
        {

        }
    }
}
