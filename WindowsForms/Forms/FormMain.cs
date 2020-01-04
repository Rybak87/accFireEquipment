using BL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormMain : Form
    {
        private readonly Dictionary<Type, ContextMenuStrip> dictMenu;

        public FormMain()
        {
            InitializeComponent();
            dictMenu = new Dictionary<Type, ContextMenuStrip>
            {
                [0.GetType()] = (contextMenuProject),
                [typeof(Location)] = (contextMenuLocation),
                [typeof(FireCabinet)] = (contextMenuFireCabinet),
                [typeof(Extinguisher)] = (contextMenuEquipment),
                [typeof(Hose)] = (contextMenuEquipment),
                [typeof(Hydrant)] = (contextMenuEquipment)
            };
            MyInitializeComponent();

            using (var db = new BLContext())
            {
                db.Database.Initialize(false);
                //db.Histories.Load();
                //var x = db.Histories.Local;
                //var f = (FireCabinet)x[0].EquipmentBase;
                //var e = (Extinguisher)x[1].EquipmentBase;

            }

            myTreeView.LoadFromContext();
            myTreeView.ButtonMouseClick += picContainer.LoadImage;
            myTreeView.ButtonMouseDoubleClick += EditDialog;
            picContainer.EditEntity += EditDialog;
            picContainer.RightClick += ShowContextMenu;
            ReportMenu.Click += ReportMenu_Click;
            TypesEquipmentMenu.Click += (s, e) => new FormTypes().ShowDialog(this);
            StickersMenu.Click += StickersMenu_Click;
            SettingsMenu.Click += SettingsMenu_Click;

        }
        public void ShowContextMenu(EntitySign sign, Point e)
        {
            dictMenu[sign.Type].Tag = sign;
            dictMenu[sign.Type].Show(e);
        }
        private void StickersMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormStickers();
            frm.EditEntity += EditDialog;
            frm.Show(this);
        }

        private void ReportMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormReport();
            frm.EditEntity += EditDialog;
            frm.Show(this);
        }

        private void SettingsMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormSettings();
            frm.ChangeSample += myTreeView.RenameNodesOfType;
            frm.ChangeSample += picContainer.DoRenameIcons;
            frm.ChangeIconSize += picContainer.DoCoerciveResize;
            frm.Show(this);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (picContainer?.Image == null)
                return;
            picContainer.ResizeRelativePosition();
        }
        private void MenuAdd_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var parentSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            var typeNewEntity = (Type)menuItem.Tag;
            AddDialog(typeNewEntity, parentSign);


        }
        private void MenuEdit_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var editSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            //var editSign = (EntitySign)myTreeView.SelectedNode.Tag;
            EditDialog(editSign);
        }
        private void MenuRemove_MouseClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var removeSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            using (var ec = new EntityController())
            {
                ec.entityRemove += myTreeView.NodeRemove;
                ec.RemoveEntity(removeSign);
                ec.SaveChanges();
            }
        }
        private void MenuRemoveIcon_MouseClick2(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var removeSign = FindContextMenuStrip(menuItem).Tag as EntitySign;
            picContainer.RemoveOfPlan(removeSign);
        }
        private void AddDialog(Type typeEntity, EntitySign parentSign)
        {
            var AddEssForm = new FormAddEntity(typeEntity, parentSign);
            AddEssForm.EntityAdd += myTreeView.NodeAdd;
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
        }
        public void EditDialog(EntitySign sign)
        {
            //if (sign == null)
            //    return;

            var AddEssForm = new FormEditEntity(sign);
            AddEssForm.EntityEdit += myTreeView.NodeMove;
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
        }
        ContextMenuStrip FindContextMenuStrip(ToolStripItem finded)
        {
            if (finded.Owner.GetType() == typeof(ContextMenuStrip))
                return (ContextMenuStrip)finded.Owner;
            else
                return FindContextMenuStrip(((ToolStripDropDownMenu)finded.Owner).OwnerItem);
        }

        private void ПаспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ec = new EntityController())
            {
                var ex = ec.Extinguishers.First();
                var wrd = new WordApp();

                wrd.CreateNewDocument(ex);
            }
        }
    }
}
