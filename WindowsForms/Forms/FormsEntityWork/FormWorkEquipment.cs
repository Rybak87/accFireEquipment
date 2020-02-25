using BL;
using System;

namespace WindowsForms
{
    /// <summary>
    /// Форма для работы с пожарным инвентарем.
    /// </summary>
    public partial class FormWorkEquipment : FormEntity
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="childType"></param>
        /// <param name="parentSign"></param>
        /// <param name="strategy"></param>
        public FormWorkEquipment(Type childType, EntitySign parentSign, Strategy strategy) : base(strategy)
        {
            InitializeComponent();
            currEntity = ec.CreateEntity(childType);
            entityType = childType;
            var parent = ec.GetEntity(parentSign) as Hierarchy;
            (currEntity as Equipment).Number = ec.GetNumberChild(parent, entityType);
            (currEntity as Equipment).Parent = parent;
            PostConstruct();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="strategy"></param>
        public FormWorkEquipment(EntitySign sign, Strategy strategy) : base(strategy)
        {
            InitializeComponent();
            currEntity = ec.GetEntity(sign);
            entityType = sign.Type;
            PostConstruct();
        }

        private void PostConstruct()
        {
            Text = strategy.GetFormName(currEntity);
            CreateControls();
        }
    }
}
