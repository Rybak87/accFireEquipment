﻿using BL;
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
        public abstract void ApplyChanged(object sender, EventArgs e);
        public abstract int CreateControls();
    }

    public class AddStrategy : Strategy
    {
        protected bool needCountCopy;
        
        NumericUpDown numCountCopy;

        public AddStrategy(FormEntity formEntity, bool needCountCopy = true)
        {
            this.formEntity = formEntity;
            this.needCountCopy = needCountCopy;
        }

        private int countCopy
        {
            get
            {
                if (numCountCopy != null)
                    return (int)numCountCopy.Value;
                else
                    return 1;
            }
        }

        /// <summary>
        /// Событие по добавлению сущности в БД.
        /// </summary>
        public event Action<EntityBase> EntityAdd;

        public override string GetFormName(EntityBase entityBase) => "Добавить " + entityBase.ToString();

        public override void ApplyChanged(object sender, EventArgs e)
        {
            if (!formEntity.CheckNeedControls())
                return;
            formEntity.ec.EntityAdd += EntityAdd;//formEntity.EntityChangedInvoke;
            if (formEntity.currEntity as Hierarchy != null)
                formEntity.ec.AddRangeEntity(formEntity.currEntity as Hierarchy, countCopy);
            else
                formEntity.ec.AddEntity(formEntity.currEntity);
            formEntity.ec.SaveChanges();
        }

        public override int CreateControls()
        {
            int lastYPosition;
            if (needCountCopy)
            {

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
                lastYPosition = formEntity.CreateControls(50);
            }
            else
            {
                lastYPosition = formEntity.CreateControls(25);
            }
            return lastYPosition;
        }
    }

    public class EditStrategy : Strategy
    {
        public EditStrategy(FormEntity formEntity)
        {
            this.formEntity = formEntity;
        }

        public override string GetFormName(EntityBase entityBase) => "Изменить " + entityBase.ToString();

        public override void ApplyChanged(object sender, EventArgs e)
        {
            if (!formEntity.CheckNeedControls())
                return;
            formEntity.ec.Entry(formEntity.currEntity).State = EntityState.Modified;

            var eq = formEntity.currEntity as Equipment;
            if (eq != null)
            {
                var histories = eq.GetNewHistories();
                formEntity.ec.Set<History>().AddRange(histories);
            }
            formEntity.ec.SaveChanges();
        }

        public override int CreateControls()
        {
            return formEntity.CreateControls(25);
        }
    }
}
