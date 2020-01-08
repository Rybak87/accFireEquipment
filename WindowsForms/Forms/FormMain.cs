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
        private readonly Dictionary<Type, ContextMenuStrip> dictMenu;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            dictMenu = new Dictionary<Type, ContextMenuStrip>
            {
                [0.GetType()] = (contextMenuProject),
                [typeof(Location)] = (contextMenuLocation),
                [typeof(FireCabinet)] = (contextMenuFireCabinet),
                [typeof(Extinguisher)] = (contextMenuExtinguisher),
                [typeof(Hose)] = (contextMenuEquipment),
                [typeof(Hydrant)] = (contextMenuEquipment)
            };
            MyInitializeComponent();

            using (var db = new BLContext())
            {
                db.Database.Initialize(false);
            }

            myTreeView.LoadFromContext();
            myTreeView.LeftMouseClick += picContainer.LoadImage;
            myTreeView.LeftMouseDoubleClick += EditDialog;
            picContainer.IconsDoubleClick += EditDialog;
            picContainer.IconsRightClick += ShowContextMenu;
            ReportMenu.Click += ReportMenu_Click;
            TypesEquipmentMenu.Click += (s, e) => new FormKinds().ShowDialog(this);
            StickersMenu.Click += StickersMenu_Click;
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
            dictMenu[sign.Type].Tag = sign;
            dictMenu[sign.Type].Show(e);
        }

        /// <summary>
        /// Обработчик меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StickersMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormStickers();
            frm.ListViewDoubleClick += EditDialog;
            frm.Show(this);
        }

        /// <summary>
        /// Обработчик меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormReport();
            frm.ListViewDoubleClick += EditDialog;
            frm.Show(this);
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
        /// Обработчик меню.
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

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAdd_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var parentSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            var typeNewEntity = (Type)menuItem.Tag;
            AddDialog(typeNewEntity, parentSign);
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEdit_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var editSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            EditDialog(editSign);
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuRemove_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var removeSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            using (var ec = new EntityController())
            {
                ec.EntityRemove += myTreeView.NodeRemove;
                ec.RemoveEntity(removeSign);
            }
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuRemoveIcon_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var removeSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            picContainer.RemoveOfPlan(removeSign);
        }

        /// <summary>
        /// Диалог добавления сущности в БД.
        /// </summary>
        /// <param name="typeEntity">Тип сущности.</param>
        /// <param name="parentSign">Идентификатор родителя сущности.</param>
        private void AddDialog(Type typeEntity, EntitySign parentSign)
        {
            var AddEssForm = new FormAddHierarchy(typeEntity, parentSign);
            AddEssForm.EntityAdd += ent => myTreeView.NodeAdd(ent as Hierarchy);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
        }

        /// <summary>
        /// Диалог изменения сущности в БД.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        public void EditDialog(EntitySign sign)
        {
            var AddEssForm = new FormEditEntity(sign);
            AddEssForm.EntityEdit += myTreeView.NodeMove;
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
        }
    }
}
