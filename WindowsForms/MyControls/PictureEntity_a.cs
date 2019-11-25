//using System;
//using System.Data.Entity;
//using System.Drawing;
//using System.Windows.Forms;

//namespace BL
//{
//    public class PictureEntity : PictureBox
//    {
//        public IPoint entity;
//        private Point MouseDownPosition;
//        private Label label;
//        double ScaleLeft { get => (double)Left / (double)Parent.Width; }
//        double ScaleTop { get => (double)Top / (double)Parent.Height; }
//        double ScaleWidth { get => (double)Width / (double)Parent.Width; }
//        double ScaleHeight { get => (double)Height / (double)Parent.Height; }
//        public PictureEntity(PictureBox parentPicBox, Image image, IPoint entity, int X = 0, int Y = 0)
//        {
//            this.entity = entity;
//            Parent = parentPicBox;
//            BorderStyle = BorderStyle.FixedSingle;
//            Image = image;
//            Size = new Size(Parent.Width / 50, Parent.Width / 50);
//            SizeMode = PictureBoxSizeMode.Zoom;
//            if (X == 0 && Y == 0)
//            {
//                Left = (int)(entity.Point.PercentLeft * parentPicBox.Width);
//                Top = (int)(entity.Point.PercentTop * parentPicBox.Height);
//            }
//            else
//            {
//                Location = PointToClient(new Point(X, Y));
//            }

//            BringToFront();
//            MouseDown += PictureEntity_Move;
//            MouseMove += PictureEntity_MouseMove;
//            Resize += PictureEntity_ResizeMove;
//            Move += PictureEntity_ResizeMove;

//            label = new Label
//            {
//                BackColor = Color.Transparent,
//                Text = entity.ToString(),
//                Parent = this.Parent
//            };
//            label.Location = parentPicBox.PointToClient(new Point(this.Location.X, this.Location.Y + this.Size.Height));
//            label.BringToFront();
//        }
//        private void PictureEntity_ResizeMove(object sender, EventArgs e)
//        {
//            if (Width == 0 || Height==0)
//                return;
//            label.Visible = false;
//            float sizeFont = (float)Width / 3;
//            label.Font = new Font(label.Font.FontFamily, sizeFont);
//            label.Width = label.PreferredWidth;
//            label.Height = label.PreferredHeight;
//            label.Top = Top + Height;
//            label.Left = Left + Width / 2 - label.Width / 2;
//            label.Visible = true;
//        }
//        private void PictureEntity_Move(object sender, MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Left)
//                MouseDownPosition = e.Location;
//        }
//        private void PictureEntity_MouseMove(object sender, MouseEventArgs e)
//        {
//            if (e.Button != MouseButtons.Left)
//                return;

//            int dx = e.X - MouseDownPosition.X;
//            int dy = e.Y - MouseDownPosition.Y;
//            if (Math.Abs(dx) >= SystemInformation.DoubleClickSize.Width || Math.Abs(dy) >= SystemInformation.DoubleClickSize.Height)
//            {
//                DoDragDrop(sender, DragDropEffects.Move);
//            }
//        }
//        public void SavePointEntity()
//        {
//            using (var ec = new EntityController(entity as EntityBase))
//            {
//                var copyEntity = ec.AttachEntity((EntityBase)entity);
//                ((IPoint)copyEntity).Point = new ScalePoint(ScaleLeft, ScaleTop, ScaleWidth, ScaleHeight);
//                ec.Entry(copyEntity).State = EntityState.Modified;
//                ec.SaveChanges();
//                entity = (IPoint)copyEntity;
//            }
//        }
//    }
//}
