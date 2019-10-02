using System.Windows.Forms;
using System;
using System.Data.Entity;
using BL;

namespace WindowsForms
{
    public partial class DbTables : Form
    {
        EssenseController essControl;

        public DbTables()
        {
            InitializeComponent();
            essControl = new EssenseController();
            #region События Контролов

            LocationsToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.Locations));
            FireCabinetsToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.FireCabinets));
            ExtinguihersToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.Extinguishers));
            HosesToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.Hoses));
            HydrantesToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.Hydrants));
            TypeExtinguisherToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.TypeExtinguishers));
            TypeHoseToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.TypeHoses));
            TypeFireCabinetToolStripMenuItem.Click += new EventHandler((s, e) => EssencesToolStripMenuItem_Click(s, e, essControl.db.TypeFireCabinets));
            #endregion
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!essControl.IsLoad)
                return;

            essControl.CreateNewEssense();

            //Создаем форму
            WorkEssense AddEssForm = new WorkEssense(essControl);

            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            essControl.AddNewEssense(AddEssForm.ControlsData);
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!essControl.IsLoad)
                return;

            int index = dgvTables.SelectedRows[0].Index;
            int id;

            if (!Int32.TryParse(dgvTables[0, index].Value.ToString(), out id))
                return;

            essControl.GetEssense(id);

            //Создаем форму
            WorkEssense AddEssForm = new WorkEssense(essControl);

            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            essControl.EditEssense(AddEssForm.ControlsData);
            dgvTables.Refresh();

        }
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (!essControl.IsLoad)
                return;

            int index = dgvTables.SelectedRows[0].Index;
            int id;

            if (!Int32.TryParse(dgvTables[0, index].Value.ToString(), out id))
                return;

            essControl.RemoveEssense(id);
        }
        private void EssencesToolStripMenuItem_Click<T>(object sender, EventArgs e, DbSet<T> collEssenses) where T : class
        {
            essControl.SetCollEssenses(collEssenses);
            dgvTables.DataSource = collEssenses.Local.ToBindingList();
        }
    }
}
