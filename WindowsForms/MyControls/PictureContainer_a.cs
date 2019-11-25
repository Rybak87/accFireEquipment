//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Core.Objects;
//using System.Drawing;
//using System.IO;
//using System.Windows.Forms;

//namespace BL
//{
//    public class PictureContainer : PictureBox
//    {
//        public event Action<EntityBase> PicDrop;
//        public event Action<EntityBase> EditEntity;
//        ImageList imageList;
//        Dictionary<Type, (int indImage, ContextMenuStrip menu)> settings;
//        public PictureContainer(ImageList imageList, Dictionary<Type, (int indImage, ContextMenuStrip menu)> settings)
//        {
//            this.imageList = imageList;
//            this.settings = settings;
//            AllowDrop = true;
//            DragEnter += new DragEventHandler(picBoxMain_DragEnter);
//            DragDrop += new DragEventHandler(picBoxMain_DragDrop);
//        }
//        private void picBoxMain_DragEnter(object sender, DragEventArgs e)
//        {
//            if (e.Data.GetDataPresent(typeof(TreeNode)))
//            {
//                var entity = ((TreeNode)e.Data.GetData(typeof(TreeNode))).Tag;
//                if (entity == null)
//                    return;
//                if (ObjectContext.GetObjectType(entity.GetType()) == typeof(Location))
//                    return;
//                e.Effect = DragDropEffects.Move;
//            }
//            if (e.Data.GetDataPresent(typeof(PictureEntity)))
//            {
//                var entity = ((PictureEntity)e.Data.GetData(typeof(PictureEntity))).entity;
//                e.Effect = DragDropEffects.Move;
//            }
//        }
//        private void picBoxMain_DragDrop(object sender, DragEventArgs e)
//        {
//            PictureEntity pic = null;

//            if (e.Data.GetDataPresent(typeof(TreeNode)))
//            {
//                var dataNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
//                var entity = dataNode.Tag as IPoint;
//                var img = imageList.Images[dataNode.ImageIndex];

//                foreach (Control control in Controls)
//                {
//                    if (!(control is PictureEntity))
//                        continue;
//                    var pic2 = control as PictureEntity;
//                    if ((EntityBase)pic2.entity == (EntityBase)entity)
//                        pic = pic2;
//                }
//                if (pic != null)
//                {

//                    pic.Location = pic.Parent.PointToClient(new Point(e.X, e.Y));
//                    pic.BringToFront();
//                }
//                else
//                {
//                    pic = new PictureEntity(this, img, entity, e.X, e.Y);
//                    pic.MouseDoubleClick += new MouseEventHandler((s2, e2) => EditEntity((EntityBase)entity));
//                }

//            }
//            else if (e.Data.GetDataPresent(typeof(PictureEntity)))
//            {
//                pic = (PictureEntity)e.Data.GetData(typeof(PictureEntity));
//                pic.Location = pic.Parent.PointToClient(new Point(e.X, e.Y));

//            }
//            pic.SavePointEntity();
//            PicDrop?.Invoke(pic.entity as EntityBase);
//            DrawImage();

//        }
//        public void LoadImage(EntityBase entity)
//        {
//            this.SuspendDrawing();
//            var ec = new EntityController(entity);
//            var parentLocation = ec.ParentLocation(entity);
//            byte[] data = parentLocation?.Image?.Image;

//            if (data == null)
//            {
//                Image = null;
//                Tag = null;
//                Controls.Clear();
//                this.ResumeDrawing();
//                return;
//            }

//            //if (Image.FromStream(new MemoryStream(parentLocation.Image.Image)) == picBoxMain.Image)
//            //    return;

//            Controls.Clear();
//            Image = Image.FromStream(new MemoryStream(data));
//            Tag = parentLocation;


//            foreach (var fc in parentLocation.FireCabinets)
//            {
//                NewPic(fc);
//                foreach (var ex in fc.Extinguishers)
//                    NewPic(ex);
//                foreach (var hose in fc.Hoses)
//                    NewPic(hose);
//                foreach (var hyd in fc.Hydrants)
//                    NewPic(hyd);
//            }

//            DrawImage();
//            this.ResumeDrawing();

//        }
//        public void NewPic(IPoint entity, int X = 0, int Y = 0)
//        {
//            if (entity.Point.Empty)
//                return;

//            var typeEntity = ObjectContext.GetObjectType(entity.GetType());
//            /////////////
//            var indImage = settings[typeEntity].indImage;
//            var img = imageList.Images[indImage];
//            var newPic = new PictureEntity(this, img, entity);
//            if (X == 0 && Y == 0)
//            {
//                newPic.Width = (int)(Width * entity.Point.PercentWidth);
//                newPic.Height = (int)(Width * entity.Point.PercentHeight);
//            }
//            newPic.MouseDoubleClick += new MouseEventHandler((s, e) => EditEntity((EntityBase)entity));
//        }
//        public void DrawImage()
//        {
//            //picBoxMain.SuspendDrawing();
//            double ratioImage = (double)Image.Width / Image.Height;
//            double ratioFlowPanel = (double)Parent.Width / Parent.Height;
//            if (ratioImage > ratioFlowPanel)
//            {
//                Width = Parent.Width;
//                Height = (int)(Parent.Width / ratioImage);
//                Top = (Parent.Height - Height) / 2;
//                Left = 0;
//            }
//            else
//            {
//                Height = Parent.Height;
//                Width = (int)(Parent.Height * ratioImage);
//                Left = (Parent.Width - Width) / 2;
//                Top = 0;
//            }
//            foreach (Control control in Controls)
//            {
//                if (!(control is PictureEntity))
//                    continue;
//                var pic = control as PictureEntity;
//                var scale = pic.entity;
//                pic.Left = (int)(scale.Point.PercentLeft * Width);
//                pic.Top = (int)(scale.Point.PercentTop * Height);
//                pic.Width = (int)(scale.Point.PercentWidth * Width);
//                pic.Height = (int)(scale.Point.PercentHeight * Height);
//            }
//            //picBoxMain.ResumeDrawing();
//        }
//    }
//}
