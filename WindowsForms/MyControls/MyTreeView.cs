using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using BL;

namespace WindowsForms
{
    public delegate void TreeNodeEventHandler(object sender, TreeNodeEventArgs e);

    public class MyTreeView : TreeView
    {
        public event TreeNodeEventHandler DoubleLeftButtonMouseClick;
        public event Action<EntitySign> ButtonMouseClick;
        public event Action<EntitySign> ButtonMouseDoubleClick;
        private Dictionary<Type, ContextMenuStrip> dictMenu;
        private Dictionary<EntitySign, TreeNode> dictNodes;
        public MyTreeView(Dictionary<Type, ContextMenuStrip> dictMenu)
        {
            dictNodes = new Dictionary<EntitySign, TreeNode>();
            ImageList = ImageSettings.IconsImageList;
            this.dictMenu = dictMenu;
            ItemDrag += treeView_ItemDrag;
            DragEnter += treeView_DragEnter;
            DragOver += treeView_DragOver;
            DragDrop += treeView_DragDrop;
            NodeMouseClick += TreeViewDB_MouseClick;
            DoubleLeftButtonMouseClick += (s, e) => ButtonMouseDoubleClick((EntitySign)e.Node.Tag);
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var entitySign = (EntitySign)((TreeNode)e.Item).Tag;

            if (entitySign?.typeEntity == null || entitySign.typeEntity == typeof(Location))
                return;
            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = PointToClient(new Point(e.X, e.Y));
            SelectedNode = GetNodeAt(targetPoint);
        }
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = GetNodeAt(targetPoint);
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            var targetType = ((EntitySign)targetNode.Tag).typeEntity;
            var draggedType = ((EntitySign)draggedNode.Tag).typeEntity;
            Type draggedTypeParent;
            if (draggedType == typeof(FireCabinet))
                draggedTypeParent = typeof(Location);
            else
                draggedTypeParent = typeof(FireCabinet);
            if (targetType == draggedTypeParent)
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    var sign = (EntitySign)draggedNode.Tag;
                    var signNewParent = (EntitySign)targetNode.Tag;
                    using (var ec = new EntityController())
                    {
                        var entity = ec.GetEntity(sign);
                        entity.Parent = ec.GetEntity(signNewParent);
                        ec.SaveChanges();
                    }
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                }

                targetNode.Expand();
            }
        }
        private void TreeViewDB_MouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ((TreeView)sender).SelectedNode = e.Node;
                return;
            }

            if (e.Button != MouseButtons.Left || e.Node.Tag == null)
                return;
            var entitySign = (EntitySign)e.Node.Tag;

            ButtonMouseClick?.Invoke(entitySign);
        }
        public void RenameNodesOfType(Type type)
        {
            var nodes = dictNodes.Where(i => i.Key.typeEntity == type);
            using (var ec = new EntityController())
            {
                foreach (var node in nodes)
                    node.Value.Text = ec.GetEntity(node.Key).ToString();
            }
        }
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2 == null)
                return false;
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;
            return ContainsNode(node1, node2.Parent);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x203) // определение двойного клика
            {
                DoubleLeftButtonMouseClick?.Invoke(this, new TreeNodeEventArgs(SelectedNode));
                m.Result = IntPtr.Zero;
            }
            else base.WndProc(ref m);
        }
        public void LoadTreeViewDb()
        {
            this.SuspendDrawing();
            using (var ec = new EntityController())
            {
                var projectNode = new TreeNode("Текущий проект");
                projectNode.ContextMenuStrip = dictMenu[0.GetType()];
                //projectNode.Tag = new EntitySign(null, 0);
                Nodes.Add(projectNode);

                foreach (var location in ec.Locations)
                {
                    var locationSign = location.GetSign();
                    var nodeLocation = CreateNode(projectNode, locationSign, location.ToString(), dictMenu[typeof(Location)]);

                    foreach (var fireCabinet in location.FireCabinets)
                    {
                        var fireCabinetSign = fireCabinet.GetSign();
                        var nodeFireCabinet = CreateNode(nodeLocation, fireCabinetSign, fireCabinet.ToString(), dictMenu[typeof(FireCabinet)]);

                        foreach (var extinguisher in fireCabinet.Extinguishers)
                        {
                            var extinguisherSign = extinguisher.GetSign();
                            CreateNode(nodeFireCabinet, extinguisherSign, extinguisher.ToString(), dictMenu[typeof(Extinguisher)]);
                        }
                        foreach (var hose in fireCabinet.Hoses)
                        {
                            var hoseSign = hose.GetSign();
                            CreateNode(nodeFireCabinet, hoseSign, hose.ToString(), dictMenu[typeof(Hose)]);
                        }
                        foreach (var hydrant in fireCabinet.Hydrants)
                        {
                            var hydrantSign = hydrant.GetSign();
                            CreateNode(nodeFireCabinet, hydrantSign, hydrant.ToString(), dictMenu[typeof(Hydrant)]);
                        }
                    }
                }
            }
            Sort();
            this.ResumeDrawing();

            TreeNode CreateNode(TreeNode parent, EntitySign entitySign, string text, ContextMenuStrip menu = null)
            {
                var indImage = ImageSettings.IconsImageIndex[entitySign.typeEntity];
                var child = new TreeNode(text, indImage, indImage);
                child.ContextMenuStrip = menu;
                parent.Nodes.Add(child);
                child.Tag = entitySign;
                dictNodes.Add(entitySign, child);
                return child;
            }
        }
        public void NodeAdd(EntityBase entity)
        {
            var nodeParent = SelectedNode;

            var indImage = ImageSettings.IconsImageIndex[entity.GetType()];
            var newNode = new TreeNode(entity.ToString(), indImage, indImage);
            newNode.ContextMenuStrip = dictMenu[entity.GetType()];
            newNode.Tag = entity.GetSign();
            nodeParent.Nodes.Add(newNode);
            dictNodes.Add(entity.GetSign(), newNode);
            SelectedNode = newNode;
        }
        public void NodeMove(EntityBase entity)
        {
            var saveSelectedNode = SelectedNode;
            var currNode = dictNodes[entity.GetSign()];
            var entityParent = entity.Parent;
            TreeNode newNodeParent;
            if (entityParent == null)
                newNodeParent = Nodes[0];
            else
                newNodeParent = dictNodes[entityParent.GetSign()];
            if (currNode.Parent != newNodeParent)
            {
                currNode.Remove();
                newNodeParent.Nodes.Add(currNode);
                currNode.Tag = entity.GetSign();
            }
            currNode.Text = entity.ToString();
            SelectedNode = saveSelectedNode;
        }
        public void NodeRemove(EntityBase entity)
        {
            Nodes.Remove(dictNodes[entity.GetSign()]);
            dictNodes.Remove(entity.GetSign());
        }
    }

    public class TreeNodeEventArgs : EventArgs
    {
        public TreeNode Node { get; }

        public TreeNodeEventArgs(TreeNode node)
        {
            Node = node;
        }
    }
}
