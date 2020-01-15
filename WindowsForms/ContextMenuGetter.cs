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
    public static class ContextMenuGetter
    {
        private static Dictionary<Type, ContextMenuStrip> dictContextMenu;

        /// <summary>
        /// Конструктор.
        /// </summary>
        static ContextMenuGetter()
        {
            //Контекстное меню проекта.
            var contextMenuProject = new ContextMenuStrip();
            contextMenuProject.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Добавить локации", MenuAdd_MouseClick, typeof(Location))});

            //Контекстное меню помещения.
            var contextMenuLocation = new ContextMenuStrip();
            contextMenuLocation.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Добавить пожарные шкафы", MenuAdd_MouseClick, typeof(FireCabinet)),
                GetStripMenuItem("Редактировать",  MenuEdit_MouseClick),
                GetStripMenuItem("Удалить",  MenuRemove_MouseClick)});

            //Контекстное меню пожарного шкафа.
            var contextMenuFireCabinetAdd = GetStripMenuItem("Добавить");
            contextMenuFireCabinetAdd.DropDownItems.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Огнетушитель", MenuAdd_MouseClick, typeof(Extinguisher)),
                GetStripMenuItem("Рукав", MenuAdd_MouseClick, typeof(Hose)),
                GetStripMenuItem("Пожарный кран", MenuAdd_MouseClick, typeof(Hydrant))});
            var contextMenuFireCabinet = new ContextMenuStrip();
            contextMenuFireCabinet.Items.AddRange(new ToolStripItem[] {
            contextMenuFireCabinetAdd,
                GetStripMenuItem("Редактировать",  MenuEdit_MouseClick),
                GetStripMenuItem("Удалить",  MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана",  MenuRemoveIcon_MouseClick)});

            //Контекстное меню огнетушителя.
            var contextMenuExtinguisher = new ContextMenuStrip();
            contextMenuExtinguisher.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Создать паспорт",  CreatePassportContext_Click),
                GetStripMenuItem("Редактировать",  MenuEdit_MouseClick),
                GetStripMenuItem("Удалить",  MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана",  MenuRemoveIcon_MouseClick)});

            //Контекстное меню рукава.
            var contextMenuHose = new ContextMenuStrip();
            contextMenuHose.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Редактировать",  MenuEdit_MouseClick),
                GetStripMenuItem("Удалить",  MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана",  MenuRemoveIcon_MouseClick)});

            //Контекстное меню пожарного крана.
            var contextMenuHydrant = new ContextMenuStrip();
            contextMenuHydrant.Items.AddRange(new ToolStripItem[] {
                GetStripMenuItem("Редактировать",  MenuEdit_MouseClick),
                GetStripMenuItem("Удалить",  MenuRemove_MouseClick),
                GetStripMenuItem("Удалить с плана",  MenuRemoveIcon_MouseClick)});

            dictContextMenu = new Dictionary<Type, ContextMenuStrip>
            {
                [typeof(Int32)] = contextMenuProject,
                [typeof(Location)] = contextMenuLocation,
                [typeof(FireCabinet)] = contextMenuFireCabinet,
                [typeof(Extinguisher)] = contextMenuExtinguisher,
                [typeof(Hose)] = contextMenuHose,
                [typeof(Hydrant)] = contextMenuHydrant
            };

        }

        /// <summary>
        /// Обновляемый TreeView
        /// </summary>
        public static MyTreeView TreeView { get; set; }

        /// <summary>
        /// Обновляемый план.
        /// </summary>
        public static PictureContainer PictureContainer { get; set; }

        /// <summary>
        /// Возвращает ячейку меню.
        /// </summary>
        /// <param name="text">Текст ячейки.</param>
        /// <param name="handler">Событие по клику мышкой.</param>
        /// <param name="tag">Тип записываемый в тэг.</param>
        /// <returns></returns>
        private static ToolStripMenuItem GetStripMenuItem(string text, EventHandler handler = null, Type tag = null)
        {
            var result = new ToolStripMenuItem(text, null, handler);
            result.Tag = tag;
            return result;
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
                var wrd = new WordPassportExtinguisher();
                wrd.CreatePassportExtinguisher(ex);
            }
        }

        /// <summary>
        /// Возвращает родительское контекстное меню.
        /// </summary>
        /// <param name="finded">Ячейка меню.</param>
        /// <returns></returns>
        private static ContextMenuStrip FindContextMenuStrip(ToolStripItem finded)
        {
            if (finded.Owner.GetType() == typeof(ContextMenuStrip))
                return (ContextMenuStrip)finded.Owner;
            else
                return FindContextMenuStrip(((ToolStripDropDownMenu)finded.Owner).OwnerItem);
        }

        /// <summary>
        /// Возвращает меню по типу.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <returns></returns>
        public static ContextMenuStrip GetMenu(Type type) => dictContextMenu[type];

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
    }
}
