//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using BL;

//namespace WindowsForms
//{
//    public delegate void TreeNodeEventHandler(object sender, TreeNodeEventArgs e);

//    public class MyTreeView : TreeView
//    {
//        public event TreeNodeEventHandler DoubleLeftButtonMouseClick;
//        public event Action<EntityBase> ButtonMouseClick;
//        public event Action<EntityBase> ButtonMouseDoubleClick;
//        private Dictionary<Type, (int indImage, ContextMenuStrip menu)> settings;
//        public MyTreeView(ImageList imageList, Dictionary<Type, (int indImage, ContextMenuStrip menu)> settings)
//        {
//            ImageList = imageList;
//            this.settings = settings;
//            ItemDrag += treeView_ItemDrag;
//            DragEnter += treeView_DragEnter;
//            DragOver += treeView_DragOver;
//            DragDrop += treeView_DragDrop;
//            NodeMouseClick += TreeViewDB_MouseClick;
//            DoubleLeftButtonMouseClick += (s, e) => ButtonMouseDoubleClick((EntityBase)e.Node.Tag);
//        }

//        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
//        {
//            ((TreeView)sender).SelectedNode = (System.Windows.Forms.TreeNode)e.Item;
//            var entity = (EntityBase)((System.Windows.Forms.TreeNode)e.Item).Tag;
//            if (entity == null)
//                return;
//            if (e.Button == MouseButtons.Left &&
//                entity.GetType() != typeof(Location))
//                DoDragDrop(e.Item, DragDropEffects.Move);
//        }
//        private void treeView_DragEnter(object sender, DragEventArgs e)
//        {
//            e.Effect = DragDropEffects.Move;
//        }
//        private void treeView_DragOver(object sender, DragEventArgs e)
//        {
//            Point targetPoint = PointToClient(new Point(e.X, e.Y));
//            SelectedNode = GetNodeAt(targetPoint);
//        }
//        private void treeView_DragDrop(object sender, DragEventArgs e)
//        {
//            Point targetPoint = PointToClient(new Point(e.X, e.Y));
//            System.Windows.Forms.TreeNode targetNode = GetNodeAt(targetPoint);
//            System.Windows.Forms.TreeNode draggedNode = (System.Windows.Forms.TreeNode)e.Data.GetData(typeof(System.Windows.Forms.TreeNode));

//            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
//            {
//                if (e.Effect == DragDropEffects.Move)
//                {
//                    draggedNode.Remove();
//                    targetNode.Nodes.Add(draggedNode);
//                }

//                targetNode.Expand();
//            }
//        }
//        private void TreeViewDB_MouseClick(object sender, TreeNodeMouseClickEventArgs e)
//        {
//            if (e.Button == MouseButtons.Right)
//            {
//                ((TreeView)sender).SelectedNode = e.Node;
//                return;
//            }

//            if (e.Button != MouseButtons.Left ||
//                e.Node.Tag == null)
//                return;
//            var entity = (EntityBase)e.Node.Tag;
//            ButtonMouseClick?.Invoke(entity);
//            //picContainer.LoadImage(entity);
//        }
//        private bool ContainsNode(System.Windows.Forms.TreeNode node1, System.Windows.Forms.TreeNode node2)
//        {
//            if (node2 == null)
//                return false;
//            if (node2.Parent == null) return false;
//            if (node2.Parent.Equals(node1)) return true;
//            return ContainsNode(node1, node2.Parent);
//        }
//        protected override void WndProc(ref Message m)
//        {
//            if (m.Msg == 0x203) // определение двойного клика
//            {
//                DoubleLeftButtonMouseClick?.Invoke(this, new TreeNodeEventArgs(this.SelectedNode));
//                m.Result = IntPtr.Zero;
//            }
//            else base.WndProc(ref m);
//        }
//        public void LoadTreeViewDb()
//        {
//            using (var db = new BLContext())
//            {
//                db.Locations.Load();
//            }
            
//            var projectNode = new System.Windows.Forms.TreeNode("Текущий проект");
//            Nodes.Add(projectNode);
//            projectNode.ContextMenuStrip = settings[0.GetType()].menu;

//            this.SuspendDrawing();
//            using (var ec = new EntityController(typeof(Location)))
//            {
//                ec.Locations.Include(l => l.Image).Load();

//                foreach (var location in ec.Locations)
//                {
//                    var nodeLocation = CreateNode(projectNode, location, 0, settings[typeof(Location)].menu);

//                    foreach (var fireCabinet in location.FireCabinets)
//                    {
//                        var nodeFireCabinet = CreateNode(nodeLocation, fireCabinet, 1, settings[typeof(FireCabinet)].menu);

//                        foreach (var extinguisher in fireCabinet.Extinguishers)
//                            CreateNode(nodeFireCabinet, extinguisher, 2, settings[typeof(Extinguisher)].menu);
//                        foreach (var hose in fireCabinet.Hoses)
//                            CreateNode(nodeFireCabinet, hose, 3, settings[typeof(Hose)].menu);
//                        foreach (var hydrant in fireCabinet.Hydrants)
//                            CreateNode(nodeFireCabinet, hydrant, 4, settings[typeof(Hydrant)].menu);
//                    }
//                }
//            }
//            Sort();
//            this.ResumeDrawing();

//            System.Windows.Forms.TreeNode CreateNode(System.Windows.Forms.TreeNode parent, EntityBase entity, int indImage, ContextMenuStrip menu = null)
//            {
//                var child = new System.Windows.Forms.TreeNode(entity.ToString(), indImage, indImage);
//                child.ContextMenuStrip = menu;
//                parent.Nodes.Add(child);
//                child.Tag = entity;
//                return child;
//            }
//        }
//        public void NodeAdd(EntityBase entity)
//        {
//            var saveNode = SelectedNode;
//            var nodeParent = NodeFind(Nodes, entity.Parent) ?? Nodes[0];
//            var entityType = entity.GetType();
//            if (!settings.Keys.Contains(entityType))
//                return;
//            var indImage = settings[entityType].indImage;
//            var newNode = new System.Windows.Forms.TreeNode(entity.ToString(), indImage, indImage);
//            newNode.ContextMenuStrip = settings[entityType].menu;
//            newNode.Tag = entity;
//            nodeParent.Nodes.Add(newNode);
//            SelectedNode = saveNode;
//        }
//        public void NodeMove(EntityBase entity)
//        {
//            var saveNode = SelectedNode;
//            var currNode = NodeFind(Nodes, entity);
//            var nodeParent = NodeFind(Nodes, entity.Parent) ?? Nodes[0];
//            currNode.Remove();
//            nodeParent.Nodes.Add(currNode);
//            currNode.Text = entity.ToString();
//            currNode.Tag = entity;
//            SelectedNode = saveNode;
//        }
//        public void NodeRemove(EntityBase entity)
//        {
//            Nodes.Remove(NodeFind(Nodes, entity));
//        }
//        public TreeNode NodeFind(TreeNodeCollection nodes, EntityBase entity)
//        {
//            if (nodes == null)
//                return null;
//            foreach (System.Windows.Forms.TreeNode n in nodes)
//            {
//                if (nodes[0].Tag != null)
//                    if ((EntityBase)n.Tag == entity)
//                        return n;

//                var node = NodeFind(n.Nodes, entity);
//                if (node != null)
//                    return node;
//            }
//            return null;
//        }
//        public TreeNode NodeFind(EntityBase entity)
//        {
//            if (Nodes == null)
//                return null;
//            foreach (System.Windows.Forms.TreeNode n in Nodes)
//            {
//                if (Nodes[0].Tag != null)
//                    if ((EntityBase)n.Tag == entity)
//                        return n;

//                var node = NodeFind(n.Nodes, entity);
//                if (node != null)
//                    return node;
//            }
//            return null;
//        }
//        public void RefreshNodeTag(System.Windows.Forms.TreeNode currNode)
//        {
//            RefreshNodeTagParent(currNode);
//            RefreshNodeTagChild(currNode);

//            void RefreshNodeTagParent(System.Windows.Forms.TreeNode currNode2)
//            {

//                var node = currNode2.Parent;
//                if (node.Tag != null)
//                {
//                    node.Tag = ((EntityBase)currNode2.Tag).Parent;
//                    RefreshNodeTagParent(node);
//                }

//            }
//            void RefreshNodeTagChild(System.Windows.Forms.TreeNode currNode2)
//            {
//                foreach (System.Windows.Forms.TreeNode node in currNode2.Nodes)
//                {
//                    var entity = (EntityBase)node.Tag;
//                    entity.Parent = (EntityBase)currNode2.Tag;
//                    RefreshNodeTag(node);
//                }
//            }
//        }
//        public void RefreshNodeTag(EntityBase entity) => RefreshNodeTag(NodeFind(entity));
//        public void nodeUpdate(EntityBase entity)
//        {
//            var node = NodeFind(Nodes, entity);
//            var ec = new EntityController(entity);
//            node.Tag = ec.AttachEntity(entity);
//            RefreshNodeTag(node);
//        }
//    }
//    public class TreeNodeEventArgs : EventArgs
//    {
//        public TreeNode Node { get; }

//        public TreeNodeEventArgs(System.Windows.Forms.TreeNode node)
//        {
//            Node = node;
//        }
//    }
//}
