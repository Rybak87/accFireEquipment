using BL;
using BL.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Контекстное меню и т.д.
    /// </summary>
    public static class SettingsOfType
    {

        private static Dictionary<Type, IconStruct> dictIcons;

        /// <summary>
        /// Конструктор.
        /// </summary>
        static SettingsOfType()
        {
            var contextMenuProject = new ContextMenuStrip();
            contextMenuProject.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Добавить локации", "Добавить", MenuAdd_MouseClick, typeof(Location))});

            var contextMenuLocation = new ContextMenuStrip();
            contextMenuLocation.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Добавить пожарные шкафы", "Добавить", MenuAdd_MouseClick, typeof(FireCabinet)),
                GetStripMenuItem("Редактировать", "Редактировать", MenuEdit_MouseClick),
                GetStripMenuItem("Удалить", "Удалить", MenuRemove_MouseClick)});

            var contextMenuFireCabinetAdd = GetStripMenuItem("Добавить", "Добавить");
            contextMenuFireCabinetAdd.DropDownItems.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Огнетушитель", "Огнетушитель", MenuAdd_MouseClick, typeof(Extinguisher)),
                GetStripMenuItem("Рукав", "Рукав", MenuAdd_MouseClick, typeof(Hose)),
                GetStripMenuItem("Пожарный кран", "Пожарный кран",MenuAdd_MouseClick, typeof(Hydrant))});
            var contextMenuFireCabinet = new ContextMenuStrip();
            contextMenuFireCabinet.Items.AddRange(new ToolStripItem[] {
            contextMenuFireCabinetAdd,
                GetStripMenuItem("Редактировать", "Редактировать", MenuEdit_MouseClick),
                GetStripMenuItem("Удалить", "Удалить", MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана", "Удалить с плана", MenuRemoveIcon_MouseClick)});

            var contextMenuExtinguisher = new ContextMenuStrip();
            contextMenuExtinguisher.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Создать паспорт", "Создать паспорт", CreatePassportContext_Click),
                GetStripMenuItem("Редактировать", "Редактировать", MenuEdit_MouseClick),
                GetStripMenuItem("Удалить", "Удалить", MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана", "Удалить с плана", MenuRemoveIcon_MouseClick)});

            var contextMenuHose = new ContextMenuStrip();
            contextMenuHose.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Редактировать", "Редактировать", MenuEdit_MouseClick),
                GetStripMenuItem("Удалить", "Удалить", MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана", "Удалить с плана", MenuRemoveIcon_MouseClick)});

            var contextMenuHydrant = new ContextMenuStrip();
            contextMenuHydrant.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Редактировать", "Редактировать", MenuEdit_MouseClick),
                GetStripMenuItem("Удалить", "Удалить", MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана", "Удалить с плана", MenuRemoveIcon_MouseClick)});

            dictIcons = new Dictionary<Type, IconStruct>
            {
                [typeof(Int32)] = new IconStruct(contextMenuProject, Icons.Location),
                [typeof(Location)] = new IconStruct(contextMenuLocation, Icons.Location),
                [typeof(FireCabinet)] = new IconStruct(contextMenuFireCabinet, Icons.FireCabinet),
                [typeof(Extinguisher)] = new IconStruct(contextMenuExtinguisher, Icons.Extinguisher),
                [typeof(Hose)] = new IconStruct(contextMenuHose, Icons.Hose),
                [typeof(Hydrant)] = new IconStruct(contextMenuHydrant, Icons.Hydrant)
            };

        }

        /// <summary>
        /// Обновляемый TreeView
        /// </summary>
        public static MyTreeView TreeView { get; set; }

        /// <summary>
        ///  Родительская форма для диалоговых окон.
        /// </summary>
        public static Form Owner { get; set; }

        /// <summary>
        /// Обновляемый план.
        /// </summary>
        public static PictureContainer PictureContainer { get; set; }

        private static ToolStripMenuItem GetStripMenuItem(string text, string name, EventHandler handler = null, Type type = null)
        {
            var result = new ToolStripMenuItem(text, null, handler, name);
            result.Tag = type;
            return result;
        }

        /// <summary>
        /// Возвращает меню по типу.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <returns></returns>
        public static ContextMenuStrip GetMenu(Type type) => dictIcons[type].contextMenu;

        /// <summary>
        /// Показать контекстное меню.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        /// <param name="e">Точка отрисовки меню.</param>
        public static void ShowContextMenu(EntitySign sign, Point e)
        {
            GetMenu(sign.Type).Tag = sign;
            GetMenu(sign.Type).Show(e);
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MenuAdd_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var parentSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            var typeNewEntity = (Type)menuItem.Tag;
            Dialogs.AddDialog(typeNewEntity, parentSign);
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MenuEdit_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var editSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            Dialogs.EditDialog(editSign);
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MenuRemove_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var removeSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            using (var ec = new EntityController())
            {
                ec.EntityRemove += TreeView.NodeRemove;
                ec.RemoveEntity(removeSign);
            }
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MenuRemoveIcon_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var removeSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            PictureContainer.RemoveOfPlan(removeSign);
        }

        /// <summary>
        /// Обработчик контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CreatePassportContext_Click(object sender, EventArgs e)
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

        private static ContextMenuStrip FindContextMenuStrip(ToolStripItem finded)
        {
            if (finded.Owner.GetType() == typeof(ContextMenuStrip))
                return (ContextMenuStrip)finded.Owner;
            else
                return FindContextMenuStrip(((ToolStripDropDownMenu)finded.Owner).OwnerItem);
        }
    }

    struct IconStruct
    {
        public ContextMenuStrip contextMenu;
        public Icon icon;

        public IconStruct(ContextMenuStrip contextMenu, Icon icon)
        {
            this.contextMenu = contextMenu;
            this.icon = icon;
        }
    }
}
