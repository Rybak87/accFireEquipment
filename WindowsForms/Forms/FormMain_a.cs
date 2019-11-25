//using BL;
//using System;
//using System.Collections.Generic;
//using System.Windows.Forms;

//namespace WindowsForms
//{
//    public partial class FormMain : Form
//    {
//        private readonly Dictionary<Type, (int indImage, ContextMenuStrip menu)> Settings;

//        public FormMain()
//        {
//            InitializeComponent();
//            Settings = new Dictionary<Type, (int indImage, ContextMenuStrip menu)>
//            {
//                [0.GetType()] = (0, contextMenuProject),
//                [typeof(Location)] = (0, contextMenuLocation),
//                [typeof(FireCabinet)] = (1, contextMenuFireCabinet),
//                [typeof(Extinguisher)] = (2, contextMenuEquipment),
//                [typeof(Hose)] = (3, contextMenuEquipment),
//                [typeof(Hydrant)] = (4, contextMenuEquipment)
//            };
//            MyInitializeComponent();

            

//            myTreeView.ImageList = imageList;
//            myTreeView.LoadTreeViewDb();
//            myTreeView.ButtonMouseClick += picContainer.LoadImage;
//            myTreeView.ButtonMouseDoubleClick += EditDialog;
//            picContainer.PicDrop += myTreeView.nodeUpdate;
//            picContainer.EditEntity += EditDialog;
//            ToolStripMenuItem.Click += (s, e) => new DbTables().ShowDialog(this);
//        }

//        private void Main_Resize(object sender, EventArgs e)
//        {
//            if (picContainer?.Image == null)
//                return;
//            picContainer.DrawImage();
//        }
//        private void MenuAdd_MouseClick(object sender, EventArgs e)
//        {
//            var menuItem = (ToolStripMenuItem)sender;
//            var typeNewEntity = (Type)menuItem.Tag;
//            var parentEntity = (EntityBase)myTreeView.SelectedNode.Tag;
//            AddDialog(typeNewEntity, parentEntity);
//        }
//        private void MenuEdit_MouseClick(object sender, EventArgs e)
//        {
//            var editEntity = (EntityBase)myTreeView.SelectedNode.Tag;
//            EditDialog(editEntity);
//        }
//        private void MenuRemove_MouseClick(object sender, EventArgs e)
//        {
//            var removeEntity = (EntityBase)myTreeView.SelectedNode.Tag;
//            var ec = new EntityController(ref removeEntity);
//            ec.entityRemove += myTreeView.NodeRemove;
//            ec.RemoveEntity(removeEntity);
//        }
//        private void AddDialog(Type typeNewEntity, EntityBase parentEntity)
//        {

//            var ec = new EntityController(typeNewEntity);
//            ec.entityAdd += myTreeView.NodeAdd;
//            var entity = ec.CreateEntity();

//            if (parentEntity == null)
//            {
//                ((INumber)entity).Number = ec.GetNumber(entity);
//            }
//            else
//            {
//                entity.Parent = ec.AttachEntity(parentEntity);
//                ((INumber)entity).Number = ec.GetNumber(entity.Parent, entity.GetType());
//            }

//            var AddEssForm = new FormEditEntity(entity, ec, true);
//            DialogResult result = AddEssForm.ShowDialog(this);
//            if (result == DialogResult.Cancel)
//                return;

//            if (parentEntity == null)
//            {
//                var currImage = AddEssForm.currImage;
//                ec.AddNewEntity(entity, currImage);
//                picContainer.LoadImage(entity);
//            }
//            else
//            {
//                ec.AddNewEntity(entity);
//                var node = myTreeView.NodeFind(entity);
//                myTreeView.RefreshNodeTag(node);
//            }
//        }
//        public void EditDialog(EntityBase entity)
//        {
//            var ec = new EntityController(ref entity);
//            ec.entityEdit += myTreeView.NodeMove;

//            var AddEssForm = new FormEditEntity(entity, ec);
//            DialogResult result = AddEssForm.ShowDialog(this);
//            if (result == DialogResult.Cancel)
//                return;

//            var currImage = AddEssForm.currImage;
//            ec.EditEntity(ref entity, currImage);
//            if (entity is Location)
//                picContainer.LoadImage(entity);
//            myTreeView.RefreshNodeTag(entity);
//        }
//        //private void AddDialog(Type typeNewEntity, EntityBase parentEntity)
//        //{
//        //    var ec = new EntityController(typeNewEntity);
//        //    ec.entityAdd += myTreeView.NodeAdd;
//        //    var child = ec.CreateEntity();
//        //    child.Parent = ec.AttachEntity(parentEntity);

//        //    ((INumber)child).Number = ec.GetNumber(child.Parent, child.GetType());

//        //    var AddEssForm = new FormEditEntity(child, ec, true);
//        //    DialogResult result = AddEssForm.ShowDialog(this);
//        //    if (result == DialogResult.Cancel)
//        //        return;

//        //    ec.AddNewEntity(child);
//        //    var node = myTreeView.NodeFind(child);
//        //    myTreeView.RefreshNodeTag(node);
//        //}
//        //private void AddRootDialog(Type typeNewEntity)
//        //{
//        //    var ec = new EntityController(typeNewEntity);
//        //    ec.entityAdd += myTreeView.NodeAdd;
//        //    var newEntity = ec.CreateEntity();

//        //    ((INumber)newEntity).Number = ec.GetNumber(newEntity);

//        //    var AddEssForm = new FormEditEntity(newEntity, ec);
//        //    DialogResult result = AddEssForm.ShowDialog(this);
//        //    if (result == DialogResult.Cancel)
//        //        return;
//        //    var currImage = AddEssForm.currImage;

//        //    ec.AddNewEntity(newEntity, currImage);
//        //    picContainer.LoadImage(newEntity);
//        //}
//    }
//}
