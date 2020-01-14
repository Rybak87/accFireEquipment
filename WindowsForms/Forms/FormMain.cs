using BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Главная форма.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            MyInitializeComponent();
            SettingsOfType.Owner = this;
            SettingsOfType.TreeView = myTreeView;
            SettingsOfType.PictureContainer = picContainer;

            Dialogs.Owner = this;
            Dialogs.TreeView = myTreeView;
            Dialogs.PictureContainer = picContainer;

            using (var db = new BLContext())
            {
                db.Database.Initialize(false);
            }

            myTreeView.LoadFromContext();
            myTreeView.LeftMouseClick += picContainer.LoadImage;
            ReportMenu.Click += (s, e) => new FormReport().Show(this);
            TypesEquipmentMenu.Click += (s, e) => new FormKinds().ShowDialog(this);
            StickersMenu.Click += (s, e) => new FormStickers().Show(this);
            SettingsMenu.Click += SettingsMenu_Click;
        }

        /// <summary>
        /// Возвращает контекстное меню.
        /// </summary>
        /// <param name="finded">Элемент контекстного меню.</param>
        private ContextMenuStrip FindContextMenuStrip(ToolStripItem finded)
        {
            if (finded.Owner.GetType() == typeof(ContextMenuStrip))
                return (ContextMenuStrip)finded.Owner;
            else
                return FindContextMenuStrip(((ToolStripDropDownMenu)finded.Owner).OwnerItem);
        }

        /// <summary>
        /// Показать контекстное меню.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        /// <param name="e">Точка отрисовки меню.</param>
        public void ShowContextMenu(EntitySign sign, Point e)
        {
            SettingsOfType.GetMenu(sign.Type).Tag = sign;
            SettingsOfType.GetMenu(sign.Type).Show(e);
        }

        /// <summary>
        /// Обработчик меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormSettings();
            frm.ChangeSample += myTreeView.RenameNodesOfType;
            frm.ChangeSample += picContainer.DoRenameIcons;
            frm.ChangeIconSize += picContainer.DoCoerciveResize;
            frm.Show(this);
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePassportContext_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var sign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            using (var ec = new EntityController())
            {
                var ex = ec.GetEntity(sign) as Extinguisher;

                var path = Application.StartupPath.ToString();
                var wrd = new WordPassportExtinguisher(path + "\\PassportExtinguisher.dotx", true);
                wrd.CreatePassportExtinguisher(ex);
            }
        }

        /// <summary>
        /// Обработчик события изменения размеров формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Resize(object sender, EventArgs e)
        {
            if (picContainer?.Image == null)
                return;
            picContainer.ResizeRelativePosition();
        }
    }
}
