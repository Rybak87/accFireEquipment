using System.Windows.Forms;
using System;
using System.Data.Entity;
using BL;

namespace WindowsForms
{
    public partial class DbTables : Form
    {
        //EssenseControl essControl;
        BLContext db;
        public DbTables()
        {
            InitializeComponent();


            #region События Контролов
            db = new BLContext();
            //using (var db = new BLContext())
            //{
            db.Locations.Load();
            var xxxx = db.Locations;
                LocationsToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.Locations));
                FireCabinetsToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.FireCabinets));
                ExtinguihersToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.Extinguishers));
                HosesToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.Hoses));
                HydrantesToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.Hydrants));
                TypeExtinguisherToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.TypeExtinguishers));
                TypeHoseToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.TypeHoses));
                TypeFireCabinetToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, db.TypeFireCabinets));
            //}
            #endregion

        }
        private void BtnAdd_Click<T>(object sender, EventArgs e) where T : class, new()
        {
            //if (!essControl.IsLoad)
            //    return;

            //essControl.CreateNewEssense();

            //Создаем форму
            var ec = new newEssenseControl<T>();
            ec.CreateEssense();
            var AddEssForm = new WorkEssense2<T>(ec);


            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            ec.AddNewEssense(AddEssForm.ControlsData);
        }
        private void BtnEdit_Click<T>(object sender, EventArgs e) where T : class, new()
        {
            //if (!essControl.IsLoad)
            //    return;

            int index = dgvTables.SelectedRows[0].Index;
            int id;

            if (!Int32.TryParse(dgvTables[0, index].Value.ToString(), out id))
                return;
            var ec = new newEssenseControl<T>();
            ec.GetEssense(id);

            //Создаем форму
            
            ec.CreateEssense();
            var AddEssForm = new WorkEssense2<T>(ec);

            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            ec.EditEssense(AddEssForm.ControlsData);
            dgvTables.Refresh();

        }
        private void BtnRemove_Click<T>(object sender, EventArgs e) where T : class, new()
        {
            //if (!essControl.IsLoad)
            //    return;

            int index = dgvTables.SelectedRows[0].Index;
            int id;

            if (!Int32.TryParse(dgvTables[0, index].Value.ToString(), out id))
                return;
            var ec = new newEssenseControl<T>();
            ec.RemoveEssense(id);
        }
        private void EssencesToolStripMenuItem_Click<T>(object sender, EventArgs e, DbSet<T> collEssenses) where T : class, new()
        {
            //essControl.SetCollEssenses(collEssenses);
            var xxx = collEssenses.Local.ToBindingList();
            dgvTables.DataSource = collEssenses.Local.ToBindingList();
            btnAdd.Click += new EventHandler(BtnAdd_Click<T>);
            btnEdit.Click += new EventHandler(BtnEdit_Click<T>);
            btnRemove.Click += new EventHandler(this.BtnRemove_Click<T>);
        }
    }
}


