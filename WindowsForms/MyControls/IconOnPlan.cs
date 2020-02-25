using BL;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Иконка на плане.
    /// </summary>
    public class IconOnPlan : PictureBox
    {
        private Point MouseDownPosition;
        private Label label;

        /// <summary>
        /// Относительная точка.
        /// </summary>
        public ScalePoint scalePoint;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parent">План.</param>
        /// <param name="image">Изображение.</param>
        /// <param name="sign">Идентификатор.</param>
        /// <param name="size">Размер.</param>
        /// <param name="scalePoint">Относительная точка.</param>
        /// <param name="textLabel">Текст под иконкой.</param>
        public IconOnPlan(Plan parent, EntitySign sign,  Size size, ScalePoint scalePoint, Image image, string textLabel)
        {
            Sign = sign;
            Parent = parent;
            this.scalePoint = scalePoint;
            Image = image;
            Size = size;
            BorderStyle = BorderStyle.FixedSingle;
            SizeMode = PictureBoxSizeMode.Zoom;

            Left = (int)(scalePoint.PercentLeft * parent.Width);
            Top = (int)(scalePoint.PercentTop * parent.Height);

            BringToFront();
            MouseDown += Icon_DragDropMove;
            MouseMove += Icon_MouseMove;
            MouseClick += Icon_MouseClick;
            MouseDoubleClick += new MouseEventHandler((s2, e2) => Dialogs.EditDialog(sign));
            Resize += (s, e) => LabelRedraw();
            Move += (s, e) => LabelRedraw();
            LabelInit(textLabel);
        }

        private double ScaleLeft { get => (double)Left / Parent.Width; }
        private double ScaleTop { get => (double)Top / Parent.Height; }

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public EntitySign Sign { get; }

        private void Icon_DragDropMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownPosition = e.Location;
        }

        private void Icon_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            int dx = e.X - MouseDownPosition.X;
            int dy = e.Y - MouseDownPosition.Y;
            if (Math.Abs(dx) >= SystemInformation.DoubleClickSize.Width || Math.Abs(dy) >= SystemInformation.DoubleClickSize.Height)
                DoDragDrop(sender, DragDropEffects.Move);
        }

        /// <summary>
        /// Обработчик события щелка мышкой по иконке.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var icon = (IconOnPlan)sender;
                var pointContextMenu = icon.PointToScreen(e.Location);
                ContextMenuGetter.ShowContextMenu(icon.Sign, pointContextMenu);
            }
        }

        private void LabelInit(string textLabel)
        {
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

        /// <summary>
        /// Текст под иконкой.
        /// </summary>
        public string TextIcon { get => label.Text; set => label.Text = value; }

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
        public void NewPosition(Point point)
        {
            Location = point;
            scalePoint.PercentLeft = (double)Left / Parent.Width;
            scalePoint.PercentTop = (double)Top / Parent.Height;
        }
    }
}
