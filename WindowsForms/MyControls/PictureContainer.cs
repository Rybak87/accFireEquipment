using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BL
{
    public class PictureContainer : PictureBox
    {
        public event Action<EntitySign> PicDrop;
        public event Action<EntitySign> EditEntity;
        public event Action CoerciveResize;

        public PictureContainer()
        {
            AllowDrop = true;
            DragEnter += new DragEventHandler(picBoxMain_DragEnter);
            DragDrop += new DragEventHandler(picBoxMain_DragDrop);
        }
        public virtual void DoCoerciveResize()
        {
            CoerciveResize?.Invoke();
        }
        private void picBoxMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                var sign = (EntitySign)((TreeNode)e.Data.GetData(typeof(TreeNode))).Tag;
                if (sign == null)
                    return;
                if (sign.typeEntity == typeof(Location))
                    return;
                e.Effect = DragDropEffects.Move;
            }
            else if (e.Data.GetDataPresent(typeof(PictureEntity)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        private void picBoxMain_DragDrop(object sender, DragEventArgs e)
        {
            PictureEntity icon = null;
            EntitySign sign = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                var dataNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                sign = dataNode.Tag as EntitySign;
                icon = FindPicture(sign);
                if (icon == null)
                {
                    string textIcon;
                    var img = ImageSettings.IconsImage(sign.typeEntity);
                    using (var ec = new EntityController())
                    {
                        var entity = ec.GetEntity(sign) as IPoint;
                        textIcon = entity.ToString();
                    }
                    icon = new PictureEntity(this, img, sign, new ScalePoint(new Point(e.X, e.Y), this), textIcon);
                    icon.MouseDoubleClick += new MouseEventHandler((s2, e2) => EditEntity(sign));
                    Resize += (s, e2) => icon.Parent_Resize();
                    CoerciveResize += icon.Parent_Resize;
                }
                else
                {
                    icon.Remove(PointToClient(new Point(e.X, e.Y)));
                    icon.BringToFront();
                }

            }
            else if (e.Data.GetDataPresent(typeof(PictureEntity)))
            {
                icon = (PictureEntity)e.Data.GetData(typeof(PictureEntity));
                icon.Remove(PointToClient(new Point(e.X, e.Y)));
            }
            icon.SavePointEntity();
            PicDrop?.Invoke(sign);
        }
        private IEnumerable<PictureEntity> GetPictureEntities()
        {
            foreach (Control control in Controls)
            {
                if (!(control is PictureEntity))
                    continue;
                yield return control as PictureEntity;
            }
        }
        private PictureEntity FindPicture(EntitySign sign)
        {
            foreach (PictureEntity pic in GetPictureEntities())
                if (pic.sign == sign)
                    return pic;
            return null;
        }
        public void LoadImage(EntitySign sign)
        {
            this.SuspendDrawing();
            var ec = new EntityController();
            var parentLocation = ec.ParentLocation(sign);
            //var byteImage = parentLocation?.Image?.Image;
            var byteImage = parentLocation?.Image;

            Controls.Clear();
            if (byteImage == null)
            {
                Image = null;
                this.ResumeDrawing();
                return;
            }

            Image = Image.FromStream(new MemoryStream(byteImage));
            //Image = byteImage;
            ResizeRelativePosition();
            CreateIcons(parentLocation);
            this.ResumeDrawing();
        }
        public void LoadImage(byte[] byteImage)
        {
            this.SuspendDrawing();
            if (byteImage == null)
                Image = null;
            else
                Image = Image.FromStream(new MemoryStream(byteImage));
            ResizeRelativePosition();
            this.ResumeDrawing();
        }
        public void LoadImage(Image byteImage)
        {
            this.SuspendDrawing();
            Image = byteImage;
            ResizeRelativePosition();
            this.ResumeDrawing();
        }
        private void CreateIcons(Location parentLocation)
        {
            foreach (var fc in parentLocation.FireCabinets)
            {
                NewPic(fc.GetSign(), fc.Point, ImageSettings.IconsImage(typeof(FireCabinet)), fc.ToString());
                foreach (var ex in fc.Extinguishers)
                    NewPic(ex.GetSign(), ex.Point, ImageSettings.IconsImage(typeof(Extinguisher)), ex.ToString());
                foreach (var hose in fc.Hoses)
                    NewPic(hose.GetSign(), hose.Point, ImageSettings.IconsImage(typeof(Hose)), hose.ToString());
                foreach (var hyd in fc.Hydrants)
                    NewPic(hyd.GetSign(), hyd.Point, ImageSettings.IconsImage(typeof(Hydrant)), hyd.ToString());
            }
        }
        public void NewPic(EntitySign sign, ScalePoint scalePoint, Image img, string textLabel)
        {
            if (!scalePoint.Empty)
                return;
            var newPic = new PictureEntity(this, img, sign, scalePoint, textLabel);
            newPic.MouseDoubleClick += new MouseEventHandler((s2, e2) => EditEntity(sign));
            Resize += (s, e) => newPic.Parent_Resize();
            CoerciveResize += newPic.Parent_Resize;
        }
        public void ResizeRelativePosition()
        {
            if (Image == null)
                return;
            double ratioImage = (double)Image.Width / Image.Height;
            double ratioFlowPanel = (double)Parent.Width / Parent.Height;
            if (ratioImage > ratioFlowPanel)
            {
                Width = Parent.Width;
                Height = (int)(Parent.Width / ratioImage);
                Top = (Parent.Height - Height) / 2;
                Left = 0;
            }
            else
            {
                Height = Parent.Height;
                Width = (int)(Parent.Height * ratioImage);
                Left = (Parent.Width - Width) / 2;
                Top = 0;
            }
        }
    }
}
