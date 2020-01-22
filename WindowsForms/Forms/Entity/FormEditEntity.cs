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
            entityType = sign.Type;

            Text = "Редактирование: " + currEntity.ToString();
            if (entityType == typeof(Location))
                currPlan = ((Location)currEntity).Plan;
        }

        ///// <summary>
        ///// Событие по изменению сущности в БД.
        ///// </summary>
        //public event Action<Hierarchy> EntityChanged;

        ///// <summary>
        ///// Событие по изменению сущности в БД.
        ///// </summary>
        //public event Action<byte[]> EntityChanged2;

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            stratOKEdit(sender, e);
        }

        /// <summary>
        /// Обработчик загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEditEntity_Load(object sender, EventArgs e) => CreateControls(25);
    }
}
