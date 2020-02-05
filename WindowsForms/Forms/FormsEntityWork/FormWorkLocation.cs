using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormWorkLocation : FormEntity
    {
        /// <summary>
        /// Текущее изображение плана.
        /// </summary>
        public byte[] currPlan;

        Strategy Strategy;
        Location currLocation;
        public FormWorkLocation()
        {
            InitializeComponent();
            Strategy = new AddStrategy(this);
            entityType = typeof(Location);
            currEntity = ec.CreateEntity(entityType);
            (currEntity as Location).Number = ec.GetNumber(currEntity as Location);
            PostConstruct();
        }
        public FormWorkLocation(EntitySign locSign)
        {
            InitializeComponent();
            Strategy = new EditStrategy(this);
            entityType = locSign.Type;
            currEntity = ec.GetEntity(locSign);
            PostConstruct();
        }

        private void PostConstruct()
        {
            currLocation = currEntity as Location;
            currPlan = currLocation.Plan;
            Text = Strategy.GetFormName(currEntity);
            var yPos = Strategy.CreateControls();
            var halfSize = new Size(75, 25);
            var centerLocation = new Point(200, yPos);
            var centerHalfLocation = new Point(275, yPos);
            CreateButtonsForImage(halfSize, centerLocation, centerHalfLocation);
            Height = Height + 25;
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
        /// Событие по добавлению сущности в БД.
        /// </summary>
        public event Action<byte[]> EntityChanged2;

        public override void BtnOK_Click(object sender, EventArgs e)
        {
            ((Location)currEntity).Plan = currPlan;
            Strategy.btnOK(sender, e);
            EntityChanged2?.Invoke(currPlan);
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
