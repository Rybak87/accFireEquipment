using BL;
using System;

namespace WindowsForms
{
    /// <summary>
    /// Форма добавления вида пожарного инвентаря.
    /// </summary>
    public partial class FormWorkKind : FormEntity
    {
        Strategy Strategy;
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityType">Тип.</param>
        public FormWorkKind(Type entityType)
        {
            InitializeComponent();
            Strategy = new AddStrategy(this, false);
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;
            Text = "Добавить новый тип";
            Strategy.CreateControls();
        }

        public FormWorkKind(EntitySign sign)
        {
            InitializeComponent();
            Strategy = new EditStrategy(this);
            currEntity = ec.GetEntity(sign);
            entityType = sign.Type;
            Text = "Изменить тип";
            Strategy.CreateControls();
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
