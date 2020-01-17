using BL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Частный TreeView
    /// </summary>
    public class MyTreeView : TreeView
    {
        /// <summary>
        /// Коллекция узлов по идентификатору.
        /// </summary>
        private Dictionary<EntitySign, TreeNode> dictNodes;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MyTreeView()
        {
            dictNodes = new Dictionary<EntitySign, TreeNode>();
            ImageList = IconsGetter.IconsImageList;
            ItemDrag += treeView_ItemDrag;
            DragEnter += treeView_DragEnter;
            DragOver += treeView_DragOver;
            DragDrop += treeView_DragDrop;
            NodeMouseClick += TreeViewDB_MouseClick;
        }

        /// <summary>
        /// Событие по левому клику мышью.
        /// </summary>
        public event Action<EntitySign> LeftMouseClick;

        #region DragDrop
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var sign = (EntitySign)((TreeNode)e.Item).Tag;

            if (sign?.Type == null || sign.Type == typeof(Location))
                return;


            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
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

            var targetType = ((EntitySign)targetNode.Tag).Type;
            var draggedType = ((EntitySign)draggedNode.Tag).Type;
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
                        var entity = ec.GetEntity(sign) as Equipment;
                        entity.Parent = (Hierarchy)ec.GetEntity(signNewParent);
                        ec.SaveChanges();
                    }
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                }

                targetNode.Expand();
            }
        }
        #endregion

        /// <summary>
        /// Обработчик события клика мышью.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewDB_MouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Tag != null)
                    ContextMenuGetter.GetMenu(((EntitySign)e.Node.Tag).Type).Tag = e.Node.Tag;
                ((TreeView)sender).SelectedNode = e.Node;
                return;
            }

            if (e.Button != MouseButtons.Left || e.Node.Tag == null)
                return;
            var entitySign = (EntitySign)e.Node.Tag;
            SelectedNode = e.Node;
            LeftMouseClick?.Invoke(entitySign);
        }

        /// <summary>
        /// Переименование узлов определенного типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        public void RenameNodesOfType(Type type)
        {
            var nodes = dictNodes.Where(i => i.Key.Type == type);
            using (var ec = new EntityController())
            {
                foreach (var node in nodes)
                    node.Value.Text = ec.GetEntity(node.Key).ToString();
            }
        }

        /// <summary>
        /// Обрабатывает сообщения Windows.
        /// </summary>
        /// <param name="m">Сообщение.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x203) // определение двойного клика
            {
                var localPos = PointToClient(Cursor.Position);
                var localNode = GetNodeAt(localPos);
                if (localNode != null)
                {
                    SelectedNode = localNode;
                    Dialogs.EditDialog((EntitySign)localNode.Tag);
                }

                m.Result = IntPtr.Zero;
            }
            else base.WndProc(ref m);
        }

        /// <summary>
        /// Заполнить данными.
        /// </summary>
        public void LoadFromContext()
        {
            this.SuspendDrawing();
            using (var ec = new EntityController())
            {
                var projectNode = new TreeNode("Проект");
                projectNode.ContextMenuStrip = ContextMenuGetter.GetMenu(0.GetType());
                Nodes.Add(projectNode);

                foreach (var location in ec.Locations)
                {
                    var locationSign = location.GetSign();
                    var nodeLocation = CreateNode(projectNode, locationSign, location.ToString(), ContextMenuGetter.GetMenu(typeof(Location)));

                    foreach (var fireCabinet in location.FireCabinets)
                    {
                        var fireCabinetSign = fireCabinet.GetSign();
                        var nodeFireCabinet = CreateNode(nodeLocation, fireCabinetSign, fireCabinet.ToString(), ContextMenuGetter.GetMenu(typeof(FireCabinet)));

                        foreach (var extinguisher in fireCabinet.Extinguishers)
                        {
                            var extinguisherSign = extinguisher.GetSign();
                            CreateNode(nodeFireCabinet, extinguisherSign, extinguisher.ToString(), ContextMenuGetter.GetMenu(typeof(Extinguisher)));
                        }
                        foreach (var hose in fireCabinet.Hoses)
                        {
                            var hoseSign = hose.GetSign();
                            CreateNode(nodeFireCabinet, hoseSign, hose.ToString(), ContextMenuGetter.GetMenu(typeof(Hose)));
                        }
                        foreach (var hydrant in fireCabinet.Hydrants)
                        {
                            var hydrantSign = hydrant.GetSign();
                            CreateNode(nodeFireCabinet, hydrantSign, hydrant.ToString(), ContextMenuGetter.GetMenu(typeof(Hydrant)));
                        }
                    }
                }
            }
            Sort();
            Nodes[0].Expand();
            this.ResumeDrawing();

            TreeNode CreateNode(TreeNode parent, EntitySign entitySign, string text, ContextMenuStrip menu = null)
            {
                var indImage = IconsGetter.GetIndexIcon(entitySign.Type);
                var child = new TreeNode(text, indImage, indImage);
                //child.ContextMenuStrip = menu;
                child.ContextMenuStrip = ContextMenuGetter.GetMenu(entitySign.Type);
                parent.Nodes.Add(child);
                child.Tag = entitySign;
                dictNodes.Add(entitySign, child);
                return child;
            }
        }

        /// <summary>
        /// Добавить узел.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public void NodeAdd(Hierarchy entity)
        {
            TreeNode nodeParent;
            if (entity is Location)
                nodeParent = Nodes[0];
            else if (entity is Equipment)
                nodeParent = dictNodes[((Equipment)entity).Parent.GetSign()];
            else
                return;

            var indImage = IconsGetter.GetIndexIcon(entity.GetType());
            var newNode = new TreeNode(entity.ToString(), indImage, indImage);
            newNode.ContextMenuStrip = ContextMenuGetter.GetMenu(entity.GetType());
            newNode.Tag = entity.GetSign();
            nodeParent.Nodes.Add(newNode);
            dictNodes.Add(entity.GetSign(), newNode);
            SelectedNode = newNode;
        }

        /// <summary>
        /// Переместить узел.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public void NodeMove(Hierarchy entity)
        {
            var saveSelectedNode = SelectedNode;
            var currNode = dictNodes[entity.GetSign()];
            Hierarchy entityParent = null;
            if (entity is Equipment)
                entityParent = ((Equipment)entity).Parent;
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

        /// <summary>
        /// Удалить узел.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public void NodeRemove(EntityBase entity)
        {
            Nodes.Remove(dictNodes[entity.GetSign()]);
            dictNodes.Remove(entity.GetSign());
        }
    }
}
