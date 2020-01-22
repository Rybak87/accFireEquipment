using BL;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    public abstract class Strat
    {
        protected FormEntity formEntity;

        public abstract string GetFormName(EntityBase entityBase);
        public abstract void  btnOK(object sender, EventArgs e);
        public abstract int CreateControls(FormEntity formEntity);
    }

    public class StratAdd : Strat
    {
        private int countCopy;

        public StratAdd(FormEntity formEntity)
        {
            this.formEntity = formEntity;
        }
        public override string GetFormName(EntityBase entityBase)
        {
            return "Добавить";
        }

        public override void btnOK(object sender, EventArgs e)
        {
            formEntity.CheckNeedControls();
            //formEntity.ec.EntityAdd += EntityChanged;
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

            var numCountCopy = new NumericUpDown
            {
                Location = new Point(200, 25),
                Size = new Size(150, 25),
                Minimum = 1,
                Maximum = 100

            };

            formEntity.Controls.Add(lblCountCopy);
            formEntity.Controls.Add(numCountCopy);
            return formEntity.CreateControls(50);
        }
    }

    public class StratEdit : Strat
    {
        public StratEdit(FormEntity formEntity)
        {
            this.formEntity = formEntity;
        }

        public override string GetFormName(EntityBase entityBase)
        {
            return "Изменить";
        }
        public override void btnOK(object sender, EventArgs e)
        {
            formEntity.CheckNeedControls();
            formEntity.ec.Entry(formEntity.currEntity).State = EntityState.Modified;
            //EntityChanged?.Invoke((Hierarchy)formEntity.currEntity);

            //var eq = formEntity.currEntity as Equipment;
            //if (eq != null)
            //{
            //    var histories = eq.GetNewHistories();
            //    formEntity.ec.Set<History>().AddRange(histories);
            //}
            formEntity.ec.SaveChanges();
        }

        public override int CreateControls(FormEntity formEntity)
        {
            return formEntity.CreateControls(25);
        }
    }
}
