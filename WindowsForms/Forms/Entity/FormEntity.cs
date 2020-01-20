using BL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Форма для работы с сущностью.
    /// </summary>
    public partial class FormEntity : Form
    {
        /// <summary>
        /// Коллекция обязательных элементов управления.
        /// </summary>
        private readonly List<Control> requiredControls = new List<Control>();

        /// <summary>
        /// Вес.
        /// </summary>
        private NumericUpDown weight;

        /// <summary>
        /// Давление.
        /// </summary>
        private NumericUpDown pressure;

        /// <summary>
        /// Текущий контекст.
        /// </summary>
        protected readonly EntityController ec = new EntityController();

        /// <summary>
        /// Текущий тип сущности.
        /// </summary>
        protected Type entityType;

        /// <summary>
        /// Текущая сущность.
        /// </summary>
        protected EntityBase currEntity;

        /// <summary>
        /// Текущее изображение плана.
        /// </summary>
        public byte[] currPlan;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormEntity()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание TextBox
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание CheckBox
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание ComboBox
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="centerLocation">Расположение.</param>
        /// <returns></returns>
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
            if (prop.Name == "KindExtinguisher")
            {
                ((ComboBox)cntrl).SelectedIndexChanged += (s, e) => GetWeightPressure((ComboBox)cntrl);
            }
            return cntrl;
        }

        /// <summary>
        /// Создание NumericUpDown
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание NumericUpDownDecimal
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
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
            cntrl.DataBindings.Add("Value", currEntity, prop.Name, true, DataSourceUpdateMode.Never);
            return cntrl;
        }

        /// <summary>
        /// Создание DateTimePicker
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание кнопок для загрузки и удаления изображений.
        /// </summary>
        /// <param name="halfSize"></param>
        /// <param name="centerLocation"></param>
        /// <param name="centerHalfLocation"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Проверка на корректные значения обязательных элементов управления.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Создание элементов формы.
        /// </summary>
        /// <param name="yPosControl"></param>
        protected virtual void CreateControls(int yPosControl)
        {
            var properties = Reflection.GetEditProperties(currEntity);
            Control cntrl = null;
            ComboBox cbxTypeExtinguisher = null;
            var incSize = new Size(175, 25);
            var fullSize = new Size(150, 25);
            var halfSize = new Size(75, 25);
            foreach (var item in properties)
            {
                var prop = item.prop;
                var attr = item.cntrlAttr;
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

                switch (attr.control)
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

                if (attr.isRequired)
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
            Height = yPosControl + 100;
            //if (cbxTypeExtinguisher != null)
            //    GetWeightPressure(cbxTypeExtinguisher);
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BtnOK_Click(object sender, EventArgs e)
        {
            if (EmptyNeedControls())
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }
        }

        /// <summary>
        /// Загрузка веса и давления по типу огнетушителя.
        /// </summary>
        /// <param name="cntrl"></param>
        private void GetWeightPressure(ComboBox cntrl)
        {
            double weight = ((KindExtinguisher)cntrl.SelectedItem).NominalWeight;
            double pressure = ((KindExtinguisher)cntrl.SelectedItem).NominalPressure;
            cntrl.DataBindings[0].WriteValue();
            this.weight.Value = (decimal)weight;
            this.pressure.Value = (decimal)pressure;
            this.weight.DataBindings[0].WriteValue();
            this.pressure.DataBindings[0].WriteValue();
        }

        /// <summary>
        /// Диалог выбора и загрузки изображения плана.
        /// </summary>
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

        /// <summary>
        /// Удаление изображения с плана.
        /// </summary>
        private void ImageClear() => currPlan = null;
    }
}
