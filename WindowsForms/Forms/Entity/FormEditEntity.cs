using BL;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Форма редактирования сущности.
    /// </summary>
    public partial class FormEditEntity : FormEntity
    {
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
        }

        /// <summary>
        /// Событие по изменению сущности в БД.
        /// </summary>
        public event Action<Hierarchy> EntityEdit;

        /// <summary>
        /// Событие по изменению сущности в БД.
        /// </summary>
        public event Action<byte[]> EntityEdit2;

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
            if (entityType == typeof(Location))
            {
                ((Location)currEntity).Plan = currPlan;
                EntityEdit2?.Invoke(currPlan);
            }

            var eq = currEntity as Equipment;
            if (eq != null)
            {
                var histories = eq.GetNewHistories();
                ec.Set<History>().AddRange(histories);
            }
            ec.SaveChanges();
        }

        /// <summary>
        /// Обработчик загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEditEntity_Load(object sender, EventArgs e) => CreateControls(25);
    }
}
