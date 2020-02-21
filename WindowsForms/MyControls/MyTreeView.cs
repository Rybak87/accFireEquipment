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
            NodeMouseClick += TreeView_NodeMouseClick;
        }

        /// <summary>
        /// Событие по левому клику мышью.
        /// </summary>
        public event Action<EntitySign> MouseClickSign;

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
            TreeNode draggedNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;

            var targetType = (targetNode.Tag as EntitySign).Type;
            var draggedType = (draggedNode.Tag as EntitySign).Type;
            Type draggedTypeParent;

            if (draggedType == typeof(FireCabinet))
                draggedTypeParent = typeof(Location);
            else
                draggedTypeParent = typeof(FireCabinet);

            if (targetType == draggedTypeParent)
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    var sign = draggedNode.Tag as EntitySign;
                    var signNewParent = targetNode.Tag as EntitySign;
                    using (var ec = new EntityController())
                    {
                        var entity = ec.GetEntity(sign) as Equipment;
                        entity.Parent = (Hierarchy)ec.GetEntity(signNewParent);
                        entity.Point.Displayed = false;
                        ec.EditEntity(entity);
                        ec.SaveChanges();
                        draggedNode.Text = entity.ToString();
                    }
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                    MouseClickSign?.Invoke(sign);
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
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SelectedNode = e.Node;
            var sign = e.Node.Tag as EntitySign;
            if (sign == null)
                return;
            MouseClickSign?.Invoke(sign);

            if (e.Button == MouseButtons.Right)
                ContextMenuGetter.GetMenu(sign.Type).Tag = e.Node.Tag;
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
            //TreeViewNodeSorter = new SortOfSign();
            Sort();
            Nodes[0].Expand();
            this.ResumeDrawing();

            TreeNode CreateNode(TreeNode parent, EntitySign entitySign, string text, ContextMenuStrip menu = null)
            {
                var indImage = IconsGetter.GetIndexIcon(entitySign.Type);
                var child = new TreeNode(text, indImage, indImage);
                child.ContextMenuStrip = ContextMenuGetter.GetMenu(entitySign.Type);
                parent.Nodes.Add(child);
                child.Tag = entitySign;
                dictNodes.Add(entitySign, child);
                return child;
            }
        }

        /// <summary>
        /// Добавить узлы.
        /// </summary>
        /// <param name="entities">Сущности.</param>
        public void NodeAddRange(Hierarchy[] entities)
        {
            TreeNode nodeParent;
            if (entities[0] is Location)
                nodeParent = Nodes[0];
            else if (entities[0] is Equipment)
                nodeParent = dictNodes[((Equipment)entities[0]).Parent.GetSign()];
            else
                return;

            var indImage = IconsGetter.GetIndexIcon(entities[0].GetType());
            var contextMenuStrip = ContextMenuGetter.GetMenu(entities[0].GetType());

            var newNodes = new List<TreeNode>(entities.Length);
            foreach (var entity in entities)
            {
                var newNode = new TreeNode(entity.ToString(), indImage, indImage);
                newNode.ContextMenuStrip = contextMenuStrip;
                newNode.Tag = entity.GetSign();
                dictNodes.Add(entity.GetSign(), newNode);
                newNodes.Add(newNode);
            }
            nodeParent.Nodes.AddRange(newNodes.ToArray());
            SelectedNode = newNodes.First();
            Sort();
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
        /// Переименовать узел.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public void NodeRename(Hierarchy entity)
        {
            if (entity == null)
                return;
            var saveSelectedNode = SelectedNode;
            var currNode = dictNodes[entity.GetSign()];
            currNode.Text = entity.ToString();
            SelectedNode = saveSelectedNode;
        }

        /// <summary>
        /// Удалить узел.
        /// </summary>
        /// <param name="sign"></param>
        public void NodeRemove(EntitySign sign)
        {
            Nodes.Remove(dictNodes[sign]);
            dictNodes.Remove(sign);
        }

        /// <summary>
        /// Возвращает идентификаторы: родителя и его подсущностей.
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public IEnumerable<EntitySign> GetChildSigns(EntitySign sign)
        {
            yield return sign;
            var parent = dictNodes.SingleOrDefault(d => d.Key == sign);
            var nodes = parent.Value.Nodes.Cast<TreeNode>();
            var signs = nodes.Select(n => n.Tag as EntitySign);
            var moreSigns = signs.SelectMany(s => GetChildSigns(s));
            foreach (var item in moreSigns)
            {
                yield return item;
            }
        }
    }
}
