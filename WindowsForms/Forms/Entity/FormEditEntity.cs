using BL;
using System;
using System.Data.Entity;

namespace WindowsForms
{
    /// <summary>
    /// Форма редактирования сущности.
    /// </summary>
    public partial class FormEditEntity : FormEntity
    {
        /// <summary>
        /// Множество изменений пожарного инвентаря.
        /// </summary>
        protected HistorySet historySet;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sign"></param>
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

        //public event Action<out EntityBase> EntityEdit;

        /// <summary>
        /// Событие по изменению сущности в БД.
        /// </summary>
        public event Action<Hierarchy> EntityEdit;

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработчик загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEditEntity_Load(object sender, EventArgs e) => CreateControls(25);
    }
}
