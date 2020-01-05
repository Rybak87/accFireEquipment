using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormAddEntity : FormEntity
    {
        NumericUpDown numCountCopy = new NumericUpDown
        {
            Location = new Point(200, 25),
            Size = new Size(150, 25),
            Minimum = 1,
            Maximum = 100
            
        };
        Label lblCountCopy = new Label
        {
            Text = "Количество",
            Location = new Point(25, 25),
            Size = new Size(175, 25)
        };

        public int CountCopy { get => (int)numCountCopy.Value; }
        public event Action<Hierarchy> EntityAdd;

        public FormAddEntity(Type entityType, EntitySign parentSign)
        {
            InitializeComponent();

            Controls.Add(numCountCopy);
            Controls.Add(lblCountCopy);
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;

            if (entityType == typeof(Location))
            {
                ((INumber)currEntity).Number = ec.GetNumber(currEntity);
                currPlan = ((Location)currEntity).Plan;
            }
            else
            {
                ((Equipment)currEntity).Parent = (Hierarchy)ec.GetEntity(parentSign);
                ((INumber)currEntity).Number = ec.GetNumberChild(((Equipment)currEntity).Parent, entityType);
            }
            Text = "Добавить";
        }
        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            base.BtnOK_Click(sender, e);
            ec.entityAdd += EntityAdd;
            ec.AddRangeEntity(currEntity, CountCopy);
            ec.SaveChanges();
        }

        private void FormAddEntity_Load(object sender, EventArgs e)
        {
            CreateControls(50);
        }
    }
}
