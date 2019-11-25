using System;
using System.Data.Entity;
using System.Drawing;
using System.Windows.Forms;

namespace BL
{
    public class PictureEntity : PictureBox
    {
        //public IPoint entity;
        private Point MouseDownPosition;
        private Label label;
        public EntitySign sign;
        ScalePoint scalePoint;

        double ScaleLeft { get => (double)Left / (double)Parent.Width; }
        double ScaleTop { get => (double)Top / (double)Parent.Height; }

        public PictureEntity(PictureBox parentPicBox, Image image, EntitySign sign, ScalePoint scalePoint, string textLabel)
        {
            this.sign = sign;
            Parent = parentPicBox;
            this.scalePoint = scalePoint;
            BorderStyle = BorderStyle.FixedSingle;
            Image = image;
            Size = new Size(Parent.Width / 50, Parent.Width / 50);
            SizeMode = PictureBoxSizeMode.Zoom;

            Left = (int)(scalePoint.PercentLeft * parentPicBox.Width);
            Top = (int)(scalePoint.PercentTop * parentPicBox.Height);


            BringToFront();
            MouseDown += PictureEntity_DragDropMove;
            MouseMove += PictureEntity_MouseMove;
            Resize += PictureEntity_ResizeMove;
            Move += PictureEntity_ResizeMove;

            label = new Label
            {
                BackColor = Color.Transparent,
                Text = textLabel,
                
            };
            label.Parent = Parent;
            LabelRedraw();
            label.BringToFront();
        }

        public void Parent_Resize(object sender, EventArgs e)
        {
            if (Parent == null)
                return;
            Size = new Size(Parent.Width / 50, Parent.Width / 50);
            Left = (int)(scalePoint.PercentLeft * Parent.Width);
            Top = (int)(scalePoint.PercentTop * Parent.Height);
        }

        private void PictureEntity_ResizeMove(object sender, EventArgs e)
        {
            LabelRedraw();
        }

        private void LabelRedraw()
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
        private void PictureEntity_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            int dx = e.X - MouseDownPosition.X;
            int dy = e.Y - MouseDownPosition.Y;
            if (Math.Abs(dx) >= SystemInformation.DoubleClickSize.Width || Math.Abs(dy) >= SystemInformation.DoubleClickSize.Height)
            {
                DoDragDrop(sender, DragDropEffects.Move);
            }
        }
        public void SavePointEntity()
        {
            using (var ec = new EntityController())
            {
                var copyEntity = ec.GetEntity(sign);
                ((IPoint)copyEntity).Point = new ScalePoint(ScaleLeft, ScaleTop, true);
                ec.Entry(copyEntity).State = EntityState.Modified;
                ec.SaveChanges();
            }
        }

        public void Remove(Point point)
        {
            Location = point;
            scalePoint.PercentLeft = (double)Left / Parent.Width;
            scalePoint.PercentTop = (double)Top / Parent.Height;
        }
    }
}
