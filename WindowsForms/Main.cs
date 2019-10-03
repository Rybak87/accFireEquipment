using BL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Main : Form
    {
        EssenseControl essControl;
        Dictionary<TreeNode, object> dictNodeEssense = new Dictionary<TreeNode, object>();
        
        public Main()
        {
            InitializeComponent();
            LoadTreeViewDb();
            AddMenuStrip();
        }

        private void AddMenuStrip()
        {
            
        }

        private void РедактироватьТаблицыToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var AddEssForm = new DbTables();
            AddEssForm.ShowDialog();
            LoadTreeViewDb();
        }
        private void TreeViewDB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode node = treeViewDB.GetNodeAt(e.Location);
                if (node == null)
                    return;

                var essense = dictNodeEssense.FirstOrDefault(i => i.Key == node).Value;

                DrawImage(essense);
            }
            if (e.Button == MouseButtons.Right)
            {
                
            }
        }

        private void DrawImage(object essense)
        {
            var prop = essense?.GetType().GetProperty("Image");
            byte[] data = (byte[])prop?.GetValue(essense);

            if (data == null)
                return;

            picBox.Image = Image.FromStream(new MemoryStream(data));
        }
        private void LoadTreeViewDb()
        {
            essControl = new EssenseControl();
            treeViewDB.Nodes.Clear();

            var contextMenuLocation = new ContextMenuStrip();
            contextMenuLocation.Items.Add("Добавить локацию");
            var contextMenuFireCabinets = new ContextMenuStrip();
            contextMenuFireCabinets.Items.Add("Добавить шкаф");
            var contextMenuEquipment = new ContextMenuStrip();
            contextMenuEquipment.Items.Add("Добавить огнетушитель");
            contextMenuEquipment.Items.Add("Добавить рукав");
            contextMenuEquipment.Items.Add("Добавить пожарный кран");

            var projectNode = new TreeNode("Текущий проект");
            projectNode.ContextMenuStrip = contextMenuLocation;
            //contextMenuLocation.Click += new EventHandler(contextMenu_MouseClick());
            treeViewDB.Nodes.Add(projectNode);
            foreach (var location in essControl.db.Locations)
            {
                var nodeLocation = new TreeNode(location.ToString());
                nodeLocation.ContextMenuStrip = contextMenuFireCabinets;
                projectNode.Nodes.Add(nodeLocation);
                dictNodeEssense.Add(nodeLocation, location);

                foreach (var fireCabinet in location)
                {
                    var nodeFireCabinet = new TreeNode(fireCabinet.ToString());
                    nodeFireCabinet.ContextMenuStrip = contextMenuEquipment;
                    nodeLocation.Nodes.Add(nodeFireCabinet);
                    dictNodeEssense.Add(nodeFireCabinet, fireCabinet);

                    foreach (var extinguisher in ((FireCabinet)fireCabinet).Extinguishers)
                    {
                        var nodeExtinguisher = new TreeNode(extinguisher.ToString());
                        nodeFireCabinet.Nodes.Add(nodeExtinguisher);
                        dictNodeEssense.Add(nodeExtinguisher, extinguisher);
                    }
                    foreach (var hose in ((FireCabinet)fireCabinet).Hoses)
                    {
                        var nodeHose = new TreeNode(hose.ToString());
                        nodeFireCabinet.Nodes.Add(nodeHose);
                        dictNodeEssense.Add(nodeHose, hose);
                    }
                    if (((FireCabinet)fireCabinet).Hydrant != null)
                    {
                        var nodeHydrant = new TreeNode(((FireCabinet)fireCabinet).Hydrant.ToString());
                        nodeFireCabinet.Nodes.Add(nodeHydrant);
                        dictNodeEssense.Add(nodeHydrant, ((FireCabinet)fireCabinet).Hydrant);
                    }
                }
            }
        }
        private void contextMenu_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void TreeViewDB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode editNode = treeViewDB.GetNodeAt(e.Location);
                if (editNode == null)
                    return;
                var essense = dictNodeEssense.FirstOrDefault(i => i.Key == editNode).Value;
                essControl.GetEssense(essense);
                if (essense.GetType() == typeof(Location))
                {
                    essControl.GetEssense(essControl.db.Locations);
                }
                else if (essense.GetType() == typeof(FireCabinet))
                {
                    essControl.GetEssense(essControl.db.FireCabinets);
                }
                else if (essense.GetType() == typeof(Extinguisher))
                {
                    essControl.GetEssense(essControl.db.Extinguishers);
                }
                else if (essense.GetType() == typeof(Hose))
                {
                    essControl.GetEssense(essControl.db.Hoses);
                }
                else if (essense.GetType() == typeof(Hydrant))
                {
                    essControl.GetEssense(essControl.db.Hydrants);
                }
                WorkEssense AddEssForm = new WorkEssense(essControl);

                DialogResult result = AddEssForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                essControl.EditEssense(AddEssForm.ControlsData);
                LoadTreeViewDb();
                DrawImage(essense);
            }
        }

    }
}
