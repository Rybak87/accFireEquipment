using BL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Main : Form
    {
        EssenseController essControl;
        Dictionary<TreeNode, object> dictionary = new Dictionary<TreeNode, object>();
        
        public Main()
        {
            InitializeComponent();
            LoadTreeViewDb();
        }

        private void РедактироватьТаблицыToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var AddEssForm = new DbTables();
            AddEssForm.ShowDialog();
            LoadTreeViewDb();
        }
        private void TreeViewDB_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = treeViewDB.GetNodeAt(e.Location);
            if (node == null)
                return;

            var essense = dictionary.FirstOrDefault(i => i.Key == node).Value;
            var prop = essense?.GetType().GetProperty("Image");
            byte[] data = (byte[])prop?.GetValue(essense);

            if (data == null)
                return;

            picBox.Image = Image.FromStream(new MemoryStream(data));
        }
        private void LoadTreeViewDb()
        {
            essControl = new EssenseController();
            treeViewDB.Nodes.Clear();

            var projectNode = new TreeNode("Текущий проект");
            treeViewDB.Nodes.Add(projectNode);
            foreach (var location in essControl.db.Locations)
            {
                var nodeLocation = new TreeNode(location.ToString());
                projectNode.Nodes.Add(nodeLocation);
                dictionary.Add(nodeLocation, location);

                foreach (var fireCabinet in location)
                {
                    var nodeFireCabinet = new TreeNode(fireCabinet.ToString());
                    nodeLocation.Nodes.Add(nodeFireCabinet);
                    dictionary.Add(nodeFireCabinet, fireCabinet);

                    foreach (var extinguisher in ((FireCabinet)fireCabinet).Extinguishers)
                    {
                        var nodeExtinguisher = new TreeNode(extinguisher.ToString());
                        nodeFireCabinet.Nodes.Add(nodeExtinguisher);
                        dictionary.Add(nodeExtinguisher, extinguisher);
                    }
                    foreach (var hose in ((FireCabinet)fireCabinet).Hoses)
                    {
                        var nodeHose = new TreeNode(hose.ToString());
                        nodeFireCabinet.Nodes.Add(nodeHose);
                        dictionary.Add(nodeHose, hose);
                    }
                    if (((FireCabinet)fireCabinet).Hydrant != null)
                    {
                        var nodeHydrant = new TreeNode(((FireCabinet)fireCabinet).Hydrant.ToString());
                        nodeFireCabinet.Nodes.Add(nodeHydrant);
                        dictionary.Add(nodeHydrant, ((FireCabinet)fireCabinet).Hydrant);
                    }
                }
            }
        }
    }
}
