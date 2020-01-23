using System;

namespace WindowsForms
{
    /// <summary>
    /// Форма добавления вида пожарного инвентаря.
    /// </summary>
    public partial class FormAddKind : FormEntity
    {
        Strategy Strategy;
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityType">Тип.</param>
        public FormAddKind(Type entityType)
        {
            InitializeComponent();
            Strategy = new AddStrategy(this);
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;
            Text = "Добавить новый тип";
            Strategy.CreateControls(this);
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void BtnOK_Click(object sender, EventArgs e)
        {
            Strategy.btnOK(sender, e);
            //base.BtnOK_Click(sender, e);
            //ec.AddEntity(currEntity);
        }
    }
}
