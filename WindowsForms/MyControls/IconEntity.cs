using System;
using System.Data.Entity;
using System.Drawing;
using System.Windows.Forms;

namespace BL
{
    /// <summary>
    /// Иконка на плане.
    /// </summary>
    public class IconEntity : PictureBox, IDisposable
    {
        private Point MouseDownPosition;
        private ScalePoint scalePoint;
        private Label label;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parent">План.</param>
        /// <param name="image">Изображение.</param>
        /// <param name="sign">Идентификатор.</param>
        /// <param name="scalePoint">Относительная точка.</param>
        /// <param name="textLabel">Текст под иконкой.</param>
        public IconEntity(PictureBox parent, Image image, EntitySign sign, ScalePoint scalePoint, string textLabel)
        {
            Sign = sign;
            Parent = parent;
            this.scalePoint = scalePoint;
            BorderStyle = BorderStyle.FixedSingle;
            Image = image;
            var ratioIconSize = Properties.Settings.Default.RatioIconSize;
            int minSize = Math.Min(Parent.Width, Parent.Height);
            Size = new Size(minSize / ratioIconSize, minSize / ratioIconSize);
            SizeMode = PictureBoxSizeMode.Zoom;

            Left = (int)(scalePoint.PercentLeft * parent.Width);
            Top = (int)(scalePoint.PercentTop * parent.Height);

            BringToFront();
            MouseDown += PictureEntity_DragDropMove;
            MouseMove += PictureEntity_MouseMove;
            Resize += (s, e) => LabelRedraw();
            Move += (s, e) => LabelRedraw();

            label = new Label
            {
                BackColor = Color.Transparent,
                Text = textLabel,
            };
            label.Parent = Parent;
            LabelRedraw();
            label.BringToFront();
        }

        /// <summary>
        /// Освобождение неуправляемых ресурсов.
        /// </summary>
        public new void Dispose()
        {
            using (var ec = new EntityController())
            {
                var entity = ec.GetEntity(Sign);
                ((Equipment)entity).Point.Displayed = false;
                ec.Entry(entity).State = EntityState.Modified;
                ec.SaveChanges();
            }
            label.Dispose();
            base.Dispose();
        }

        private double ScaleLeft { get => (double)Left / Parent.Width; }
        private double ScaleTop { get => (double)Top / Parent.Height; }

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public EntitySign Sign { get; }

        /// <summary>
        /// Текст под иконкой.
        /// </summary>
        public string TextIcon { get => label.Text; set => label.Text = value; }

        private void PictureEntity_DragDropMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownPosition = e.Location;
        }
        private void PictureEntity_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            int dx = e.X - MouseDownPosition.X;
            int dy = e.Y - MouseDownPosition.Y;
            if (Math.Abs(dx) >= SystemInformation.DoubleClickSize.Width || Math.Abs(dy) >= SystemInformation.DoubleClickSize.Height)
                DoDragDrop(sender, DragDropEffects.Move);
        }

        ///
        /// ///
        /// 
        /// <summary>
        /// Изменение размера иконки.
        /// </summary>
        /// <param name="size">Размер иконки.</param>
        public void Parent_Resize(Size size)
        {
            if (Parent == null)
                return;
            Size = size;
            Left = (int)(scalePoint.PercentLeft * Parent.Width);
            Top = (int)(scalePoint.PercentTop * Parent.Height);
        }

        /// <summary>
        /// Перерисовка подписи под иконкой.
        /// </summary>
        public void LabelRedraw()
        {
            if (Width == 0 || Height == 0)
                return;
            label.Visible = false;
            float sizeFont = (float)Width / 3;
            label.Font = new Font(label.Font.FontFamily, sizeFont);
            label.Width = label.PreferredWidth;
            label.Height = label.PreferredHeight;
            label.Top = Top + Height;
            label.Left = Left + Width / 2 - label.Width / 2;
            label.Visible = true;
        }

        /// <summary>
        /// Сохранение относительной точки пожарного инвентаря в БД.
        /// </summary>
        public void SavePointEntity()
        {
            using (var ec = new EntityController())
            {
                var copyEntity = ec.GetEntity(Sign);
                ((Equipment)copyEntity).Point = new ScalePoint(ScaleLeft, ScaleTop, true);
                ec.Entry(copyEntity).State = EntityState.Modified;
                ec.SaveChanges();
            }
        }

        /// <summary>
        /// Изменение относительной точки.
        /// </summary>
        /// <param name="point">Расположение иконки на плане.</param>
        public void NewLocation(Point point)
        {
            Location = point;
            scalePoint.PercentLeft = (double)Left / Parent.Width;
            scalePoint.PercentTop = (double)Top / Parent.Height;
        }
    }
}
