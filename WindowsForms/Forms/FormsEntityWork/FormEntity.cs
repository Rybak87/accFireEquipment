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
        public readonly List<Control> requiredControls = new List<Control>();

        /// <summary>
        /// Текущий контекст.
        /// </summary>
        public readonly EntityController ec = new EntityController();

        /// <summary>
        /// Текущий тип сущности.
        /// </summary>
        public Type entityType;

        /// <summary>
        /// Текущая сущность.
        /// </summary>
        public EntityBase currEntity;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormEntity()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие по добавлению сущности в БД.
        /// </summary>
        public event Action<EntityBase> EntityChanged;
        #region Создание контролов
        /// <summary>
        /// Создание TextBox
        /// </summary>
        /// <param name="fullSize">Размер.</param>
        /// <param name="prop">Свойство привязки.</param>
        /// <param name="location">Расположение.</param>
        /// <returns></returns>
        protected virtual TextBox CreateTextBox(Size fullSize, PropertyInfo prop, Point location)
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
        protected virtual CheckBox CreateCheckBox(Size fullSize, PropertyInfo prop, Point location)
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
        protected virtual ComboBox CreateComboBox(Size fullSize, PropertyInfo prop, Point centerLocation)
        {
            var cntrl = new ComboBox
            {
                Location = centerLocation,
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
        protected virtual NumericUpDown CreateNumericUpDown(Size fullSize, PropertyInfo prop, Point location)
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
        protected virtual NumericUpDown CreateNumericUpDownDecimal(Size fullSize, PropertyInfo prop, Point location)
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
        protected virtual DateTimePicker CreateDateTimePicker(Size fullSize, PropertyInfo prop, Point location)
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
        #endregion

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

        public bool CheckNeedControls()
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
        /// Создание элементов формы.
        /// </summary>
        /// <param name="yPosControl"></param>
        public virtual int CreateControls(int yPosControl)
        {
            var editProperties = Reflection.GetEditProperties(currEntity);
            Control cntrl = null;
            var incSize = new Size(175, 25);
            var fullSize = new Size(150, 25);
            foreach (var item in editProperties)
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
            return yPosControl - 25;
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void BtnOK_Click(object sender, EventArgs e)
        {
            CheckNeedControls();
        }

        public void EntityChangedInvoke(EntityBase entity) => EntityChanged?.Invoke(entity);
    }
}
