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
    public static class SettingsOfType
    {
        private static Dictionary<Type, IconStruct> dictIcons;
        static SettingsOfType()
        {
            //AddLocationContextMenu.Click += MenuAdd_MouseClick;
            var contextMenuProject = new ContextMenuStrip();
            contextMenuProject.Items.AddRange(new ToolStripItem[] {
            GetStripMenuItem("Добавить локации", "Добавить", typeof(Location))});
            //contextMenuProject.Name = "contextMenuLocations";

            var contextMenuLocation = new ContextMenuStrip();
            contextMenuLocation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            GetStripMenuItem("Добавить пожарные шкафы", "Добавить", typeof(FireCabinet)),
            GetStripMenuItem("Редактировать", "Редактировать"),
            GetStripMenuItem("Удалить", "Удалить")
            });

            dictIcons = new Dictionary<Type, IconStruct>
            {
                [typeof(Int32)] = new IconStruct(contextMenuProject, Icons.Location),
                [typeof(Location)] = new IconStruct(contextMenuLocation, Icons.Location),
                [typeof(FireCabinet)] = new IconStruct(new ContextMenuStrip(), Icons.FireCabinet),
                [typeof(Extinguisher)] = new IconStruct(new ContextMenuStrip(), Icons.Extinguisher),
                [typeof(Hose)] = new IconStruct(new ContextMenuStrip(), Icons.Hose),
                [typeof(Hydrant)] = new IconStruct(new ContextMenuStrip(), Icons.Hydrant)
            };

        }

        private static ToolStripMenuItem GetStripMenuItem(string text, string name, Type type = null)
        {
            var result = new ToolStripMenuItem(text, null, null, name);
            result.Tag = type;
            return result;
        }

        public static ContextMenuStrip GetMenu(Type type) => dictIcons[type].contextMenu;
        //private static void MenuAdd_MouseClick(object sender, EventArgs e)
        //{
        //    var menuItem = (ToolStripMenuItem)sender;
        //    var parentSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
        //    var typeNewEntity = (Type)menuItem.Tag;
        //    AddDialog(typeNewEntity, parentSign);
        //}
        //private static void AddDialog(Type typeEntity, EntitySign parentSign)
        //{
        //    var AddEssForm = new FormAddHierarchy(typeEntity, parentSign);
        //    AddEssForm.EntityAdd += ent => myTreeView.NodeAdd(ent as Hierarchy);
        //    DialogResult result = AddEssForm.ShowDialog(this);
        //    if (result == DialogResult.Cancel)
        //        return;
        //}
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
        //Type type;
        public ContextMenuStrip contextMenu;
        public Icon icon;

        public IconStruct(/*Type type, */ContextMenuStrip contextMenu, Icon icon)
        {
            //this.type = type;
            this.contextMenu = contextMenu;
            this.icon = icon;
        }
    }
}
