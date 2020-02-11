﻿using BL;
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
        protected Strategy Strategy;
        Equipment currEquipment;
        public FormWorkEquipment(Type childType, EntitySign parentSign)
        {
            InitializeComponent();
            currEntity = ec.CreateEntity(childType);
            Strategy = new AddStrategy(this);
            entityType = childType;
            var parent = ec.GetEntity(parentSign) as Hierarchy;
            (currEntity as Equipment).Number = ec.GetNumberChild(parent, entityType);
            (currEntity as Equipment).Parent = parent;
            PostConstruct();
        }
        public FormWorkEquipment(EntitySign sign)
        {
            InitializeComponent();
            currEntity = ec.GetEntity(sign);
            Strategy = new EditStrategy(this);
            entityType = sign.Type;
            PostConstruct();
        }

        private void PostConstruct()
        {
            currEquipment = currEntity as Equipment;
            Text = Strategy.GetFormName(currEntity);
            Strategy.CreateControls();
        }

        public override void BtnOK_Click(object sender, EventArgs e)
        {
            Strategy.btnOK_Click(sender, e);
        }
    }
}