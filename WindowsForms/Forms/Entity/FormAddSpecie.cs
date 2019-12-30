using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormAddSpecie : FormEntity
    {
        public FormAddSpecie(Type entityType)
        {
            InitializeComponent();
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;
            Text = "Добавить новый тип";
        }
        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            base.BtnOK_Click(sender, e);
            ec.AddEntity(currEntity);
            ec.SaveChanges();
        }

        private void FormAddSpecie_Load(object sender, EventArgs e)
        {
            CreateControls(25);
        }
    }
}
