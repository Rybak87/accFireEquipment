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
    public class PictureContainer : PictureBox
    {
        /// <summary>
        /// Относительный размер иконок на плане.
        /// </summary>
        public int RatioIconSize { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public PictureContainer()
        {
            AllowDrop = true;
            DragEnter += new DragEventHandler(picBoxMain_DragEnter);
            DragDrop += new DragEventHandler(picBoxMain_DragDrop);
            RatioIconSize = Properties.Settings.Default.RatioIconSize;
        }

        //public event Action<EntitySign> PicDrop;

        /// <summary>
        /// Событие изменения размеров иконок.
        /// </summary>
        private event Action<Size> IconsResize;

        /// <summary>
        /// Событие двойного клика по иконкам на плане.
        /// </summary>
        public event Action<EntitySign> IconsDoubleClick;

        /// <summary>
        /// Событие щелчка правой кнопки мыши по иконкам на плане.
        /// </summary>
        public event Action<EntitySign, Point> IconsRightClick;

        /// <summary>
        /// Возвращает размер иконок.
        /// </summary>
        private Size GetSizeIcons()
        {
            var side = (Width / RatioIconSize);
            return new Size(side, side);
        }

        /// <summary>
        /// Добавить новую иконку на план.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sign">Идентификатор сущности.</param>
        /// <returns></returns>
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
            icon = CreateIcon(sign, new ScalePoint(new Point(e.X, e.Y), this), IconsGetter.GetIcon(sign.Type), textIcon);
            this.ResumeDrawing();
            return icon;
        }

        /// <summary>
        /// Найти иконку по идентификатору.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        /// <returns></returns>
        private IconEntity FindIcon(EntitySign sign)
        {
            foreach (IconEntity icon in GetIcons())
                if (icon.Sign == sign)
                    return icon;
            return null;
        }

        /// <summary>
        /// Возвращает все иконки на плане.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IconEntity> GetIcons()
        {
            foreach (Control control in Controls)
            {
                if (!(control is IconEntity))
                    continue;
                yield return control as IconEntity;
            }
        }

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
            else if (e.Data.GetDataPresent(typeof(IconEntity)))
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

        /// <summary>
        /// Метод по событию щелка мышкой по иконке.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var y = ((IconEntity)sender).PointToScreen(e.Location);
                IconsRightClick?.Invoke(((IconEntity)sender).Sign, y);
                SettingsOfType.ShowContextMenu(((IconEntity)sender).Sign, y);
            }
        }

        /// <summary>
        /// Создать новую иконку.
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="scalePoint"></param>
        /// <param name="img"></param>
        /// <param name="textLabel"></param>
        /// <returns></returns>
        public IconEntity CreateIcon(EntitySign sign, ScalePoint scalePoint, Image img, string textLabel)
        {
            var icon = new IconEntity(this, img, sign, scalePoint, textLabel);
            icon.MouseDoubleClick += new MouseEventHandler((s2, e2) => Dialogs.EditDialog(sign));
            IconsResize += icon.Parent_Resize;
            icon.MouseClick += Icon_MouseClick;
            return icon;
        }

        /// <summary>
        /// Принудительное изменение размеров иконок.
        /// </summary>
        public void DoCoerciveResize()
        {
            RatioIconSize = Properties.Settings.Default.RatioIconSize;
            this.SuspendDrawing();
            IconsResize?.Invoke(GetSizeIcons());
            this.ResumeDrawing();
        }

        /// <summary>
        /// Переименовать иконки определенного типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        public void DoRenameIcons(Type type)
        {
            using (var ec = new EntityController())
            {
                ec.Set(type).Load();
                foreach (IconEntity icon in GetIcons())
                {

                    if (icon.Sign.Type == type)
                    {
                        var entity = ec.GetEntity(icon.Sign);
                        icon.TextIcon = entity.ToString();
                        icon.LabelRedraw();
                    }
                }
            }
        }

        /// <summary>
        /// Загрузить изображение.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
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
                    CreateIcon(eq.GetSign(), eq.Point, IconsGetter.GetIcon(eq.GetType()), eq.ToString());
            }
            this.ResumeDrawing();
        }

        /// <summary>
        /// Загрузить изображение.
        /// </summary>
        /// <param name="byteImage">Изображение.</param>
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

        /// <summary>
        /// Удалить иконку с плана.
        /// </summary>
        /// <param name="removeSign">Идентификатор.</param>
        public void RemoveOfPlan(EntitySign removeSign)
        {
            var x = GetIcons();
            GetIcons().First(i => i.Sign == removeSign).Dispose();
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
            IconsResize?.Invoke(GetSizeIcons());
        }
    }
}
