using BL;
using System;
using System.Data.Entity;

namespace WindowsForms
{
    public partial class FormEditEntity : FormEntity
    {
        //public event Action<out EntityBase> EntityEdit;
        public event Action<Hierarchy> EntityEdit;
        protected HistorySet historySet;

        public FormEditEntity(EntitySign sign)
        {
            InitializeComponent();

            currEntity = ec.GetEntity(sign);
            entityType = currEntity.GetType();

            Text = "Редактирование: " + currEntity.ToString();
            if (currEntity.GetType() == typeof(Location))
                currPlan = ((Location)currEntity).Plan;
            if (currEntity is Equipment)
            {
                historySet = new HistorySet(currEntity as Equipment);
            }
        }
        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            base.BtnOK_Click(sender, e);
            ec.Entry(currEntity).State = EntityState.Modified;
            EntityEdit?.Invoke((Hierarchy)currEntity);
            ec.SaveChanges();
            if (currEntity is Equipment)
            {
                historySet.SetNewValues();
                historySet.Save(ec);
            }
        }

        private void FormEditEntity_Load(object sender, EventArgs e)
        {
            CreateControls(25);
        }
    }
}
