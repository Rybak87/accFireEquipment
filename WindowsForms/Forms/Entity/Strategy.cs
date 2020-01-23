using BL;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    public abstract class Strategy
    {
        protected FormEntity formEntity;

        public abstract string GetFormName(EntityBase entityBase);
        public abstract void btnOK(object sender, EventArgs e);
        public abstract int CreateControls(FormEntity formEntity);
    }

    public class AddStrategy : Strategy
    {
        private int countCopy { get => (int)numCountCopy.Value; }
        NumericUpDown numCountCopy;

        public AddStrategy(FormEntity formEntity)
        {
            this.formEntity = formEntity;
        }
        public override string GetFormName(EntityBase entityBase)
        {
            return "Добавить " + entityBase.ToString();
        }

        public override void btnOK(object sender, EventArgs e)
        {
            formEntity.CheckNeedControls();
            formEntity.ec.EntityAdd += formEntity.EntityChangedInvoke;
            formEntity.ec.AddRangeEntity(formEntity.currEntity as Hierarchy, countCopy);
            formEntity.ec.SaveChanges();
        }

        public override int CreateControls(FormEntity formEntity)
        {
            /// <summary>
            /// Надпись "количество" сущностей.
            /// </summary>
            var lblCountCopy = new Label
            {
                Text = "Количество",
                Location = new Point(25, 25),
                Size = new Size(175, 25)
            };

            numCountCopy = new NumericUpDown
            {
                Location = new Point(200, 25),
                Size = new Size(150, 25),
                Minimum = 1,
                Maximum = 100

            };

            formEntity.Controls.Add(lblCountCopy);
            formEntity.Controls.Add(numCountCopy);
            int lastYPosition = formEntity.CreateControls(50);
            return lastYPosition;
        }
    }

    public class EditStrategy : Strategy
    {
        public EditStrategy(FormEntity formEntity)
        {
            this.formEntity = formEntity;
        }

        public override string GetFormName(EntityBase entityBase)
        {
            return "Изменить " + entityBase.ToString();
        }
        public override void btnOK(object sender, EventArgs e)
        {
            formEntity.CheckNeedControls();
            formEntity.ec.Entry(formEntity.currEntity).State = EntityState.Modified;
            formEntity.ec.EntityAdd += formEntity.EntityChangedInvoke;
            formEntity.ec.SaveChanges();
        }

        public override int CreateControls(FormEntity formEntity)
        {
            return formEntity.CreateControls(25);
        }
    }
}
