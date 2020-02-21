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
        /// Стратегия работы с сущностью.
        /// </summary>
        protected Strategy strategy;

        protected Size halfSize = new Size(75, 25);
        protected Size fullSize = new Size(150, 25);
        protected Size incSize = new Size(175, 25);

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormEntity(Strategy strategy)
        {
            InitializeComponent();
            this.strategy = strategy;
        }

        #region Создание контролов
        /// <summary>
        /// Создание TextBox
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
        protected virtual TextBox CreateTextBox(PropertyInfo prop, Size fullSize, Point location)
        {
            var cntrl = new TextBox
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
        protected virtual CheckBox CreateCheckBox(PropertyInfo prop, Size fullSize, Point location)
        {
            var cntrl = new CheckBox
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
        protected virtual ComboBox CreateComboBox(PropertyInfo prop, Size fullSize, Point location)
        {
            var cntrl = new ComboBox
            {
                Location = location,
                Size = fullSize,
                Sorted = true
            };
            var parents = ec.Set(prop.PropertyType);
            parents.Load();
            cntrl.DataSource = parents.Local;

            var bind = new Binding("SelectedItem", currEntity, prop.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            cntrl.DataBindings.Add(bind);
            cntrl.CreateControl();
            Controls.Add(cntrl);
            cntrl.DataBindings[0].ReadValue();
            prop.SetValue(currEntity, cntrl.SelectedItem);
            return cntrl;
        }

        /// <summary>
        /// Создание NumericUpDown
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
        protected virtual NumericUpDown CreateNumericUpDown(PropertyInfo prop, Size fullSize, Point location)
        {
            var cntrl = new NumericUpDown
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
        protected virtual NumericUpDown CreateNumericUpDownDecimal(PropertyInfo prop, Size fullSize, Point location)
        {
            var cntrl = new NumericUpDown
            {
                DecimalPlaces = 2,
                Location = location,
                Size = fullSize
            };
            Controls.Add(cntrl);
            cntrl.DataBindings.Add("Value", currEntity, prop.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            return cntrl;
        }

        /// <summary>
        /// Создание DateTimePicker
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
        protected virtual DateTimePicker CreateDateTimePicker(PropertyInfo prop, Size fullSize, Point location)
        {
            var cntrl = new DateTimePicker
            {
                Location = location,
                Size = fullSize,
            };
            Controls.Add(cntrl);
            cntrl.DataBindings.Add("Value", currEntity, prop.Name);
            return cntrl;
        }

        /// <summary>
        /// Создание метки названия.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fullSize"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        protected virtual Label CreateHeaderLabel(string name, Size fullSize, Point location)
        {
            var cntrl = new Label
            {
                Text = name,
                Location = location,
                Size = fullSize
            };
            Controls.Add(cntrl);
            return cntrl;
        }

        /// <summary>
        /// Создание метки обязательного заполнения.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        protected virtual Label CreateRequiredLabel(Point location)
        {
            var cntrl = new Label
            {
                Text = "Обязательно заполнить",
                AutoSize = true,
                Location = location
            };
            Controls.Add(cntrl);
            requiredControls.Add(cntrl);
            return cntrl;
        }

        protected virtual int CreateBeforeControls()
        {
            var beforeControls = strategy.GetBeforeControls();
            int yPos = 25;
            if (beforeControls != null)
            {
                Controls.AddRange(beforeControls);
                yPos = (beforeControls.Length / 2 + 1) * 25;
            }
            return yPos;
        }

        protected virtual int CreateAfterControls(int yPos)
        {
            return yPos;
        }

        protected Point LeftLocation(int yPos) => new Point(25, yPos);

        protected Point CenterLocation(int yPos) => new Point(200, yPos);

        protected Point CenterHalfLocation(int yPos) => new Point(275, yPos);

        protected Point RightLocation(int yPos) => new Point(400, yPos);
        #endregion

        /// <summary>
        /// Создание элементов формы.
        /// </summary>
        /// <param name="yPosControl"></param>
        protected void CreateControls()
        {
            int yPos = CreateBeforeControls();

            var editProperties = Reflection.GetEditProperties(currEntity);
            foreach (var item in editProperties)
            {
                var property = item.property;
                var attr = item.cntrlAttr;
                var name = item.name;

                var centerLocation = CenterLocation(yPos);
                if (name == null)
                    name = property.Name;

                CreateHeaderLabel(name, incSize, LeftLocation(yPos));
                switch (attr.control)
                {
                    case "TextBox":
                        {
                            CreateTextBox(property, fullSize, centerLocation);
                            break;
                        }
                    case "ComboBox":
                        {
                            CreateComboBox(property, fullSize, centerLocation);
                            break;
                        }
                    case "CheckBox":
                        {
                            CreateCheckBox(property, fullSize, centerLocation);
                            break;
                        }
                    case "NumericUpDown":
                        {
                            CreateNumericUpDown(property, fullSize, centerLocation);
                            break;
                        }
                    case "NumericUpDownDecimal":
                        {
                            CreateNumericUpDownDecimal(property, fullSize, centerLocation);
                            break;
                        }
                    case "DateTimePicker":
                        {
                            CreateDateTimePicker(property, fullSize, centerLocation);
                            break;
                        }
                }

                if (attr.isRequired)
                    CreateRequiredLabel(RightLocation(yPos));
                yPos += 25;
            }
            yPos = CreateAfterControls(yPos);
            Height = yPos + 100;
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

        private bool CheckNeedControls()
        {
            if (EmptyNeedControls())
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Необходимо заполнить все поля");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BtnOK_Click(object sender, EventArgs e)
        {
            if (!CheckNeedControls())
                return;
            strategy.ApplyChanged(currEntity, ec);
        }
    }
}
