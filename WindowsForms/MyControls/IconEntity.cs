using System;
using System.Data.Entity;
using System.Drawing;
using System.Windows.Forms;

namespace BL
{
    public class IconEntity : PictureBox, IDisposable
    {
        //public IPoint entity;
        private Point MouseDownPosition;
        public Label label;
        public EntitySign Sign { get; }
        public string TextIcon { get => label.Text; set => label.Text = value; }
        private ScalePoint scalePoint;

        private double ScaleLeft { get => (double)Left / Parent.Width; }
        private double ScaleTop { get => (double)Top / Parent.Height; }

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

        public void Parent_Resize(Size size)
        {
            if (Parent == null)
                return;
            //var ratioIconSize = Properties.Settings.Default.RatioIconSize;
            //Size = new Size(Parent.Width / ratioIconSize, Parent.Width / ratioIconSize);////
            Size = size;
            Left = (int)(scalePoint.PercentLeft * Parent.Width);
            Top = (int)(scalePoint.PercentTop * Parent.Height);
        }

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

        private void PictureEntity_DragDropMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownPosition = e.Location;
        }
        //IconEntity temp;
        private void PictureEntity_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            int dx = e.X - MouseDownPosition.X;
            int dy = e.Y - MouseDownPosition.Y;
            if (Math.Abs(dx) >= SystemInformation.DoubleClickSize.Width || Math.Abs(dy) >= SystemInformation.DoubleClickSize.Height)
            {
                DoDragDrop(sender, DragDropEffects.Move);
                ////var icon = sender as IconEntity;
                ////icon.Location = Parent.PointToClient(e.Location);
                //if (temp == null)
                //    temp = new IconEntity((PictureBox)Parent, Image, null, scalePoint, "");
                //else
                //{
                //    temp.Location = ((IconEntity)sender).PointToClient(e.Location);
                //}
            }
        }

        public void SavePointEntity()
        {
            using (var ec = new EntityController())
            {
                var copyEntity = ec.GetEntity(Sign);
                ((Equipment)copyEntity).Point = new ScalePoint(ScaleLeft, ScaleTop, false);
                ec.Entry(copyEntity).State = EntityState.Modified;
                ec.SaveChanges();
            }
        }

        public void NewLocation(Point point)
        {
            Location = point;
            scalePoint.PercentLeft = (double)Left / Parent.Width;
            scalePoint.PercentTop = (double)Top / Parent.Height;
        }
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
    }
}
