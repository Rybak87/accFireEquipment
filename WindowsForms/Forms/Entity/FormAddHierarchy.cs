using BL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Форма добаления иерархической сущности.
    /// </summary>
    public partial class FormAddHierarchy : FormEntity
    {
        /// <summary>
        /// Количество сущностей.
        /// </summary>
        private NumericUpDown numCountCopy = new NumericUpDown
        {
            Location = new Point(200, 25),
            Size = new Size(150, 25),
            Minimum = 1,
            Maximum = 100

        };

        /// <summary>
        /// Надпись "количество" сущностей.
        /// </summary>
        private Label lblCountCopy = new Label
        {
            Text = "Количество",
            Location = new Point(25, 25),
            Size = new Size(175, 25)
        };

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="parentSign"></param>
        public FormAddHierarchy(Type entityType, EntitySign parentSign)
        {
            InitializeComponent();

            Controls.Add(numCountCopy);
            Controls.Add(lblCountCopy);
            currEntity = ec.CreateEntity(entityType);
            this.entityType = entityType;

            if (entityType == typeof(Location))
            {
                ((Hierarchy)currEntity).Number = ec.GetNumber(currEntity as Hierarchy);
                currPlan = ((Location)currEntity).Plan;
            }
            else
            {
                ((Equipment)currEntity).Parent = (Hierarchy)ec.GetEntity(parentSign);
                ((Hierarchy)currEntity).Number = ec.GetNumberChild(((Equipment)currEntity).Parent, entityType);
            }
            Text = "Добавить";
        }

        /// <summary>
        /// Событие по добавлению сущности в БД.
        /// </summary>
        public event Action<Hierarchy> EntityAdd;

        /// <summary>
        /// Количество сущностей.
        /// </summary>
        public int CountCopy { get => (int)numCountCopy.Value; }

        /// <summary>
        /// Обработчик события кнопки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnOK_Click(object sender, EventArgs e)
        {
            base.BtnOK_Click(sender, e);
            ec.EntityAdd += EntityAdd;
            ec.AddRangeEntity((Hierarchy)currEntity, CountCopy);
            ec.SaveChanges();
        }

        /// <summary>
        /// Обработчик события загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAddEntity_Load(object sender, EventArgs e)
        {
            CreateControls(50);
        }
    }
}
