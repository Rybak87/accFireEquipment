using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BL
{
    public class PictureContainer : PictureBox
    {
        //public event Action<EntitySign> PicDrop;
        public event Action<EntitySign> EditEntity;
        public event Action<EntitySign, Point> RightClick;
        public event Action CoerciveResize;

        public PictureContainer()
        {
            AllowDrop = true;
            DragEnter += new DragEventHandler(picBoxMain_DragEnter);
            DragDrop += new DragEventHandler(picBoxMain_DragDrop);
        }
        public void DoCoerciveResize()
        {
            CoerciveResize?.Invoke();
        }
        private void picBoxMain_DragEnter(object sender, DragEventArgs e)
        {
            var dataNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            var sign = dataNode.Tag as EntitySign;
            using (var ec = new EntityController())
            {
                var parentLocation = ((Hierarchy)ec.GetEntity(sign)).GetLocation;
                if (parentLocation.GetSign() == (EntitySign)Tag)
                    e.Effect = DragDropEffects.Move;
            }
        }
        private void picBoxMain_DragDrop(object sender, DragEventArgs e)
        {
            IconEntity icon = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                var dataNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                var sign = dataNode.Tag as EntitySign;
                icon = FindIcon(sign);
                if (icon == null)
                {
                    
                    icon = AddNewIcon(e, sign);
                }
                else
                {
                    icon.NewLocation(PointToClient(new Point(e.X, e.Y)));
                    icon.BringToFront();
                }
                //PicDrop?.Invoke(sign);
            }
            else if (e.Data.GetDataPresent(typeof(IconEntity)))
            {
                icon = (IconEntity)e.Data.GetData(typeof(IconEntity));
                icon.NewLocation(PointToClient(new Point(e.X, e.Y)));
                icon.BringToFront();
            }
            icon.SavePointEntity();
        }

        private IconEntity AddNewIcon(DragEventArgs e, EntitySign sign)
        {
            IconEntity icon;
            string textIcon;
            using (var ec = new EntityController())
            {
                var entity = ec.GetEntity(sign) as Equipment;
                textIcon = entity.ToString();
            }
            this.SuspendDrawing();
            icon = CreateIcon(sign, new ScalePoint(new Point(e.X, e.Y), this), ImageSettings.IconsImage(sign.Type), textIcon);
            this.ResumeDrawing();
            return icon;
        }

        public void LoadImage(EntitySign sign)
        {
            using (var ec = new EntityController())
            {
                var parentLocation = ec.GetParentLocation(sign);
                if (parentLocation.GetSign() == ((EntitySign)Tag))
                    return;
                Tag = parentLocation.GetSign();
                var byteImage = parentLocation.Plan;
                this.SuspendDrawing();
                Controls.Clear();
                if (byteImage == null)
                {
                    Image = null;
                    this.ResumeDrawing();
                    return;
                }

                Image = Image.FromStream(new MemoryStream(byteImage));
                ResizeRelativePosition();
                var drawEquipment = ec.GetDrawEquipment(parentLocation);
                foreach (var eq in drawEquipment)
                    CreateIcon(eq.GetSign(), eq.Point, ImageSettings.IconsImage(eq.GetType()), eq.ToString());
            }
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
        public IconEntity CreateIcon(EntitySign sign, ScalePoint scalePoint, Image img, string textLabel)
        {
            var icon = new IconEntity(this, img, sign, scalePoint, textLabel);
            icon.MouseDoubleClick += new MouseEventHandler((s2, e2) => EditEntity(sign));
            Resize += (s, e) => icon.Parent_Resize();
            CoerciveResize += icon.Parent_Resize;
            icon.MouseClick += Icon_MouseClick;
            return icon;
        }

        public void RemoveOfPlan(EntitySign removeSign)
        {
            var x = GetIcons();
            GetIcons().First(i => i.Sign == removeSign).Dispose();
        }

        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var y = ((IconEntity)sender).PointToScreen(e.Location);
                RightClick?.Invoke(((IconEntity)sender).Sign, y);
            }
        }

        private IEnumerable<IconEntity> GetIcons()
        {
            foreach (Control control in Controls)
            {
                if (!(control is IconEntity))
                    continue;
                yield return control as IconEntity;
            }
        }
        private IconEntity FindIcon(EntitySign sign)
        {
            foreach (IconEntity icon in GetIcons())
                if (icon.Sign == sign)
                    return icon;
            return null;
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
