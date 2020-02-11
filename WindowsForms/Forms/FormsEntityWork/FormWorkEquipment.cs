using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormWorkEquipment : FormEntity
    {
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
