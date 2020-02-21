using BL;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Стратегия добавления сущности.
    /// </summary>
    public class AddStrategy : Strategy
    {
        private bool needCountCopy;
        NumericUpDown numCountCopy;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public AddStrategy(bool needCountCopy = true)
        {
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
        /// Возвращает имя формы.
        /// </summary>
        /// <param name="entityBase">Сущность.</param>
        /// <returns></returns>
        public override string GetFormName(EntityBase entityBase) => "Добавить " + entityBase.ToString();

        /// <summary>
        /// Работа с сущностью при нажатии ОК.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ec"></param>
        public override void ApplyChanged(EntityBase entity, EntityController ec)
        {
            ec.EntityAdd += EntityChanged;
            ec.HierarchyAddRange += HierarchyChangedRange;
            if (entity as Hierarchy != null)
                ec.AddRangeHierarchy(entity as Hierarchy, countCopy);
            else
                ec.AddEntity(entity);
            ec.SaveChanges();
        }

        /// <summary>
        /// Добавление контролов количества сущностей.
        /// </summary>
        public override Control[] GetBeforeControls()
        {
            if (!needCountCopy)
                return null;

            var labelCountCopy = new Label
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

            return new Control[] { labelCountCopy, numCountCopy };
        }
    }
}
