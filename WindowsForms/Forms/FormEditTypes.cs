using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormEditTypes : Form
    {
        Type saveType;
        public FormEditTypes()
        {
            InitializeComponent();
            FireCabinetsMenu.Image = ImageSettings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));
            HosesMenu.Image = ImageSettings.IconsImage(typeof(Hose));
        }

        private void FireCabinetsMenu_Click(object sender, EventArgs e) => LoadTypes(typeof(TypeFireCabinet));
        private void ExtinguishersMenu_Click(object sender, EventArgs e) => LoadTypes(typeof(TypeExtinguisher));
        private void HosesMenu_Click(object sender, EventArgs e) => LoadTypes(typeof(TypeHose));
        private void LoadTypes(Type type)
        {
            listView.Items.Clear();
            using (var ec = new EntityController())
            {
                foreach (ITypes t in ec.GetCollection(type))
                {
                    var item = new ListViewItem(t.Name);
                    var subItem = new ListViewItem.ListViewSubItem(item, t.Manufacturer);
                    item.SubItems.Add(subItem);
                    item.Tag = ((EntityBase)t).GetSign();
                    listView.Items.Add(item);
                }
            }
            saveType = type;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (saveType == null)
                return;
            var ec = new EntityController();
            var entity = ec.CreateEntity(saveType);

            var AddEssForm = new FormEditEntity(entity, ec);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            ec.AddNewEntity(entity);
            LoadTypes(saveType);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var sign = (EntitySign)listView.SelectedItems[0].Tag;

            var ec = new EntityController();
            var AddEssForm = new FormEditEntity(ec.GetEntity(sign), ec);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            ec.EditEntity(sign);
            LoadTypes(saveType);
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var sign = (EntitySign)listView.SelectedItems[0].Tag;

            var ec = new EntityController();
            ec.RemoveEntity(sign);
            LoadTypes(saveType);
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var item = listView.SelectedItems[0];
            var sign = (EntitySign)item.Tag;
            BtnEdit_Click(null, EventArgs.Empty);
        }
    }
}
