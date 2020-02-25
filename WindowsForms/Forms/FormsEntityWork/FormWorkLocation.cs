using BL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Форма для работы с помещением.
    /// </summary>
    public partial class FormWorkLocation : FormEntity
    {
        /// <summary>
        /// Текущее изображение плана.
        /// </summary>
        public byte[] currPlan;

        Location currLocation;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="strategy"></param>
        public FormWorkLocation(Strategy strategy) : base(strategy)
        {
            InitializeComponent();
            entityType = typeof(Location);
            currEntity = ec.CreateEntity(entityType);
            (currEntity as Location).Number = ec.GetNumber(currEntity as Location);
            PostInitialize();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="locSign"></param>
        public FormWorkLocation(EntitySign locSign, Strategy strategy) : base(strategy)
        {
            InitializeComponent();
            entityType = locSign.Type;
            currEntity = ec.GetEntity(locSign);
            PostInitialize();
        }

        /// <summary>
        /// Событие по кнопке ОК.
        /// </summary>
        public event Action<byte[]> LocationEntityChanged;

        private void PostInitialize()
        {
            currLocation = currEntity as Location;
            currPlan = currLocation.Plan;
            Text = strategy.GetFormName(currEntity);
            CreateControls();
            Height += 25;
        }

        protected override int AfterCreateControls(int yPos)
        {
            CreateButtonsForImage(halfSize, CenterLocation(yPos - 25), CenterHalfLocation(yPos - 25));
            return yPos;
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
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            ((Location)currEntity).Plan = currPlan;
            base.BtnOK_Click(sender, e);
            LocationEntityChanged?.Invoke(currPlan);
        }
    }
}
