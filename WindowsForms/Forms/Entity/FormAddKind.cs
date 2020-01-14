using System;

namespace WindowsForms
{
    /// <summary>
    /// Форма добавления вида пожарного инвентаря.
    /// </summary>
    public partial class FormAddKind : FormEntity
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityType">Тип.</param>
        public FormAddKind(Type entityType)
        {
            InitializeComponent();
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;
            Text = "Добавить новый тип";
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            base.BtnOK_Click(sender, e);
            ec.AddEntity(currEntity);
        }

        /// <summary>
        /// Обработчик загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAddKind_Load(object sender, EventArgs e) => CreateControls(25);
    }
}
