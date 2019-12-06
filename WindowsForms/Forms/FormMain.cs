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
            ReportMenu.Click += ReportMenu_Click;
            TypesEquipmentMenu.Click += (s, e) => new FormEditTypes().ShowDialog(this);
            StickersMenu.Click += StickersMenu_Click;
            SettingsMenu.Click += SettingsMenu_Click;
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
            var typeNewEntity = (Type)menuItem.Tag;
            var parentSign = (EntitySign)myTreeView.SelectedNode.Tag;
            AddDialog2(typeNewEntity, parentSign);
        }
        private void MenuAdd_MouseClick2(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var typeNewEntity = (Type)menuItem.Tag;
            var parentSign = (EntitySign)myTreeView.SelectedNode.Tag;
            AddRangeDialog(typeNewEntity, parentSign);
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
        private void AddDialog2(Type typeEntity, EntitySign parentSign)
        {
            //var ec = new EntityController();
            //ec.entityAdd += myTreeView.NodeAdd;
            //var entity = ec.CreateEntity(typeNewEntity);

            //if (parentSign == null)
            //{
            //    ((INumber)entity).Number = ec.GetNumber(entity);
            //}
            //else
            //{
            //    entity.Parent = ec.GetEntity(parentSign);
            //    ((INumber)entity).Number = ec.GetNumberChild(entity.Parent, entity.GetType());
            //}

            var AddEssForm = new FormEditEntity2(typeEntity, parentSign);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            using(var ec = new EntityController())
            {
                EntityBase entity = (EntityBase)ec.GetTable(typeEntity).Attach(AddEssForm.currEntity);
                ec.AddNewEntity(entity);
                ec.SaveChanges();
            }
            //var currImage = AddEssForm.currImage;

            //ec.AddNewEntity(entity);
        }
        private void AddRangeDialog(Type typeNewEntity, EntitySign parentSign)
        {
            var ec = new EntityController();
            ec.entityAdd += myTreeView.NodeAdd;

            var AddEssForm = new FormEditEntities(typeNewEntity, ec);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            int c = (int)AddEssForm.num.Value;
            var typeEq = AddEssForm.cbx.SelectedItem;
            var pi = AddEssForm.pi;
            for (int i = 1; i <= c; i++)
            {
                var entity = ec.CreateEntity(typeNewEntity);
                
                if (parentSign == null)
                {
                    ((INumber)entity).Number = ec.GetNumber(entity);
                    ((Location)entity).Name = "Помещение №" + ((INumber)entity).Number;
                }
                else
                {
                    entity.Parent = ec.GetEntity(parentSign);
                    ((INumber)entity).Number = ec.GetNumberChild(entity.Parent, entity.GetType());
                    pi.SetValue(entity, typeEq);
                }
                ec.AddNewEntity(entity);
            }

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
