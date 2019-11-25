using System.Windows.Forms;
using System;
using System.Data.Entity;
using BL;
using System.Collections.Generic;

namespace WindowsForms
{
    public partial class DbTables : Form
    {
        List<EventHandler> lstClick;
        public DbTables()
        {
            InitializeComponent();
            lstClick = new List<EventHandler>();
            #region События Контролов
            LocationsToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<Location>(s, e));
            FireCabinetsToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<FireCabinet>(s, e));
            ExtinguihersToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<Extinguisher>(s, e));
            HosesToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<Hose>(s, e));
            HydrantesToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<Hydrant>(s, e));
            TypeExtinguisherToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<TypeExtinguisher>(s, e));
            TypeHoseToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<TypeHose>(s, e));
            TypeFireCabinetToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click<TypeFireCabinet>(s, e));
            //}
            #endregion

        }
        private void BtnAdd_Click<TEntity>(object sender, EventArgs e) where TEntity : EntityBase, new()
        {
            var ec = new EntityController();
            ec.entityAdd += ((FormMain)Owner).myTreeView.NodeAdd;

            var newEntity = ec.CreateEntity(typeof(TEntity));

            //Создаем форму
            var AddEssForm = new FormEditEntity(newEntity, ec);

            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            
            ec.AddNewEntity(newEntity);
            ec.Set<TEntity>().Load();
            dgvTables.DataSource = ec.Set<TEntity>().Local.ToBindingList();
            dgvTables.Refresh();
        }
        private void BtnEdit_Click<TEntity>(object sender, EventArgs e) where TEntity : EntityBase, new()
        {
            if (dgvTables.SelectedRows.Count == 0)
                return;
            int index = dgvTables.SelectedRows[0].Index;
            if (!Int32.TryParse(dgvTables[dgvTables.Columns["Id"].Index, index].Value.ToString(), out int id))
                return;
            var sign = new EntitySign(typeof(TEntity), id);

            var ec = new EntityController();
            ec.entityEdit += ((FormMain)Owner).myTreeView.NodeMove;
            var newEntity = ec.GetEntity(sign);

            //Создаем форму
            var AddEssForm = new FormEditEntity(newEntity, ec);

            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            
            ec.EditEntity(sign);
            ec.Set<TEntity>().Load();
            dgvTables.DataSource = ec.Set<TEntity>().Local.ToBindingList();
            dgvTables.Refresh();
        }
        private void BtnRemove_Click<TEntity>(object sender, EventArgs e) where TEntity : EntityBase, new()
        {
            if (dgvTables.SelectedRows.Count == 0)
                return;
            int index = dgvTables.SelectedRows[0].Index;
            if (!Int32.TryParse(dgvTables[dgvTables.Columns["Id"].Index, index].Value.ToString(), out int id))
                return;

            var sign = new EntitySign(typeof(TEntity), id);
            var ec = new EntityController();
            ec.entityRemove += ((FormMain)Owner).myTreeView.NodeRemove;

            ec.RemoveEntity(sign);
            
            ec.Set<TEntity>().Load();
            dgvTables.DataSource = ec.Set<TEntity>().Local.ToBindingList();
            dgvTables.Refresh();
        }
        private void EssencesToolStripMenuItem_Click<TEntity>(object sender, EventArgs e) where TEntity : EntityBase,  new()
        {
            if (lstClick.Count != 0)
            {
                btnAdd.Click -= lstClick[0];
                btnEdit.Click -= lstClick[1];
                btnRemove.Click -= lstClick[2];
                lstClick.Clear();
            }
            lstClick.Add(new EventHandler(BtnAdd_Click<TEntity>));
            lstClick.Add(new EventHandler(BtnEdit_Click<TEntity>));
            lstClick.Add(new EventHandler(BtnRemove_Click<TEntity>));
            btnAdd.Click += lstClick[0];
            btnEdit.Click += lstClick[1];
            btnRemove.Click += lstClick[2];

            var db = new BLContext();
            db.Set<TEntity>().Load();
            dgvTables.DataSource = db.Set<TEntity>().Local.ToBindingList();
        }
    }
}


