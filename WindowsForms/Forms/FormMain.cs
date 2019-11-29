using BL;
using System;
using System.Collections.Generic;
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
            }

            myTreeView.LoadTreeViewDb();
            myTreeView.ButtonMouseClick += picContainer.LoadImage;
            myTreeView.ButtonMouseDoubleClick += EditDialog;
            picContainer.EditEntity += EditDialog;
            EditDatabaseMenu.Click += (s, e) => new DbTables().ShowDialog(this);
            ReportMenu.Click += (s, e) => new FormReport().Show();
            TypesEquipmentMenu.Click += (s, e) => new FormEditTypes().ShowDialog(this);
            StickersMenu.Click += (s, e) => new FormStickers().Show();
            SettingsMenu.Click += SettingsMenu_Click;
        }

        private void SettingsMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormSettings();
            frm.ChangeSample += myTreeView.RenameNodesOfType;
            frm.Show();
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
            var typeNewEntity = (Type)menuItem.Tag;
            var parentSign = (EntitySign)myTreeView.SelectedNode.Tag;
            AddDialog(typeNewEntity, parentSign);
        }
        private void MenuEdit_MouseClick(object sender, EventArgs e)
        {
            var editSign = (EntitySign)myTreeView.SelectedNode.Tag;
            EditDialog(editSign);
        }
        private void MenuRemove_MouseClick(object sender, EventArgs e)
        {
            var removeSign = (EntitySign)myTreeView.SelectedNode.Tag;
            var ec = new EntityController();
            ec.entityRemove += myTreeView.NodeRemove;
            ec.RemoveEntity(removeSign);
        }
        private void AddDialog(Type typeNewEntity, EntitySign parentSign)
        {
            var ec = new EntityController();
            ec.entityAdd += myTreeView.NodeAdd;
            var entity = ec.CreateEntity(typeNewEntity);

            if (parentSign == null)
            {
                ((INumber)entity).Number = ec.GetNumber(entity);
            }
            else
            {
                entity.Parent = ec.GetEntity(parentSign);
                ((INumber)entity).Number = ec.GetNumberChild(entity.Parent, entity.GetType());
            }

            var AddEssForm = new FormEditEntity(entity, ec, true);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            var currImage = AddEssForm.currImage;

            ec.AddNewEntity(entity);
        }
        public void EditDialog(EntitySign sign)
        {
            if (sign == null)
                return;
            var ec = new EntityController();
            ec.entityEdit += myTreeView.NodeMove;

            var AddEssForm = new FormEditEntity(ec.GetEntity(sign), ec);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            var currImage = AddEssForm.currImage;
            ec.EditEntity(sign);
        }
    }
}
