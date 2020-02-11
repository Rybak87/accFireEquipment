using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsForms;

namespace BL
{
    /// <summary>
    /// План.
    /// </summary>
    public class Plan : PictureBox
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Plan()
        {
            AllowDrop = true;
            DragEnter += picBoxMain_DragEnter;
            DragDrop += picBoxMain_DragDrop;
        }

        /// <summary>
        /// Добавить новую иконку на план при перетаскивании.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sign">Идентификатор сущности.</param>
        /// <returns></returns>
        private IconOnPlan AddNewIcon(DragEventArgs e, EntitySign sign)
        {
            IconOnPlan icon;
            string textIcon;
            using (var ec = new EntityController())
            {
                var entity = ec.GetEntity(sign) as Equipment;
                textIcon = entity.ToString();
            }
            this.SuspendDrawing();
            icon = new IconOnPlan(this, sign, GetSizeIcons(), new ScalePoint(new Point(e.X, e.Y), this), IconsGetter.GetIconImage(sign.Type), textIcon);
            this.ResumeDrawing();
            return icon;
        }

        /// <summary>
        /// Найти иконку по идентификатору.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        /// <returns></returns>
        private IconOnPlan FindIcon(EntitySign sign) => GetIcons().SingleOrDefault(i => i.Sign == sign);

        /// <summary>
        /// Возвращает все иконки на плане.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IconOnPlan> GetIcons() => Controls.Cast<Control>().Where(c => c is IconOnPlan).Cast<IconOnPlan>().ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
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
            else if (e.Data.GetDataPresent(typeof(IconOnPlan)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxMain_DragDrop(object sender, DragEventArgs e)
        {
            IconOnPlan icon = null;
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
                    icon.NewPosition(PointToClient(new Point(e.X, e.Y)));
                    icon.BringToFront();
                }
            }
            else if (e.Data.GetDataPresent(typeof(IconOnPlan)))
            {
                icon = (IconOnPlan)e.Data.GetData(typeof(IconOnPlan));
                icon.NewPosition(PointToClient(new Point(e.X, e.Y)));
                icon.BringToFront();
            }
            icon.SavePointEntity();
        }

        /// <summary>
        /// Возвращает размер иконок.
        /// </summary>
        public Size GetSizeIcons()
        {
            var ratioIconSize = Properties.Settings.Default.RatioIconSize;
            int minSize = Math.Min(Width, Height);
            var side = minSize / ratioIconSize;
            return new Size(side, side);
        }

        /// <summary>
        /// Загрузка изображения.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        public void LoadImage(EntitySign sign)
        {
            using (var ec = new EntityController())
            {
                var parentLocation = ec.GetParentLocation(sign);
                if (parentLocation.GetSign() == ((EntitySign)Tag))// В теге хранится текущий идентификатор помещения на плане.
                    return;
                Tag = parentLocation.GetSign();
                var byteImage = parentLocation.Plan;
                this.SuspendDrawing();
                Controls.Clear();
                if (byteImage == null)
                {
                    Image = null;
                }
                else
                {
                    Image = Image.FromStream(new MemoryStream(byteImage));
                    ResizeRelativePosition();
                    var drawEquipment = ec.GetDrawEquipment(parentLocation);
                    var sizeIcons = GetSizeIcons();
                    foreach (var eq in drawEquipment)
                        new IconOnPlan(this, eq.GetSign(), sizeIcons, eq.Point, IconsGetter.GetIconImage(eq.GetType()), eq.ToString());
                }
            }
            this.ResumeDrawing();
        }

        /// <summary>
        /// Загрузка изображения.
        /// </summary>
        /// <param name="byteImage">Изображение.</param>
        public void LoadImage(byte[] byteImage)
        {
            this.SuspendDrawing();
            if (byteImage == null)
            {
                Image = null;
                RemoveAllIcons();
            }
            else
                Image = Image.FromStream(new MemoryStream(byteImage));
            ResizeRelativePosition();
            this.ResumeDrawing();
        }

        /// <summary>
        /// Удалить иконку с плана.
        /// </summary>
        /// <param name="removeSign">Идентификатор.</param>
        public void RemoveIcon(EntitySign removeSign)
        {
            var icon = GetIcons().FirstOrDefault(i => i.Sign == removeSign);
            icon?.Dispose();
        }

        /// <summary>
        /// Удаление всех иконок с плана.
        /// </summary>
        public void RemoveAllIcons()
        {
            foreach (var icon in GetIcons())
                icon.Dispose();
        }

        /// <summary>
        /// Переименовать иконки определенного типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        public void RenameIcons(Type type)
        {
            using (var ec = new EntityController())
            {
                var icons = GetIcons().Where(i => i.Sign.Type == type);
                foreach (var icon in icons)
                {
                    var entity = ec.GetEntity(icon.Sign);
                    icon.TextIcon = entity.ToString();
                    icon.LabelRedraw();
                }
            }
        }

        /// <summary>
        /// Изменение размера иконки.
        /// </summary>
        /// <param name="size">Изменение размеров иконок.</param>
        public void ResizeIcons()
        {
            var size = GetSizeIcons();
            this.SuspendDrawing();
            foreach (var icon in GetIcons())
            {
                icon.Size = size;
                icon.Left = (int)(icon.scalePoint.PercentLeft * Width);
                icon.Top = (int)(icon.scalePoint.PercentTop * Height);
            }
            this.ResumeDrawing();
        }

        /// <summary>
        /// Изменить размер плана, по изменению размеров формы.
        /// </summary>
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
            ResizeIcons();
        }
    }
}
