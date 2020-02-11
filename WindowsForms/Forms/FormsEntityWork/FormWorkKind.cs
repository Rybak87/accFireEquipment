using BL;
using System;

namespace WindowsForms
{
    /// <summary>
    /// Форма добавления вида пожарного инвентаря.
    /// </summary>
    public partial class FormWorkKind : FormEntity
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityType">Тип.</param>
        public FormWorkKind(Type entityType, Strategy strategy):base(strategy)
        {
            InitializeComponent();
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;
            Text = "Добавить новый тип";
            CreateControls();
        }

        public FormWorkKind(EntitySign sign, Strategy strategy) : base(strategy)
        {
            InitializeComponent();
            currEntity = ec.GetEntity(sign);
            entityType = sign.Type;
            Text = "Изменить тип";
            CreateControls();
        }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public override void BtnOK_Click(object sender, EventArgs e)
        //{
        //    strategy.ApplyChanged(sender, e);
        //    //base.BtnOK_Click(sender, e);
        //    //ec.AddEntity(currEntity);
        //}
    }
}
