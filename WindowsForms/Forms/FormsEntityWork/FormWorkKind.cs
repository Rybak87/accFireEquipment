using BL;
using System;

namespace WindowsForms
{
    /// <summary>
    /// Форма для работы с видами пожарного инвентаря.
    /// </summary>
    public partial class FormWorkKind : FormEntity
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityType">Тип.</param>
        /// /// <param name="strategy">Стратегия.</param>
        public FormWorkKind(Type entityType, Strategy strategy) : base(strategy)
        {
            InitializeComponent();
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;
            Text = "Добавить новый тип";
            CreateControls();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        /// /// <param name="strategy">Стратегия.</param>
        public FormWorkKind(EntitySign sign, Strategy strategy) : base(strategy)
        {
            InitializeComponent();
            currEntity = ec.GetEntity(sign);
            entityType = sign.Type;
            Text = "Изменить тип";
            CreateControls();
        }
    }
}
