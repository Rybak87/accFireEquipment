//using BL;
//using System;
//using System.Drawing;
//using System.Windows.Forms;

//namespace WindowsForms
//{
//    /// <summary>
//    /// Форма добаления иерархической сущности.
//    /// </summary>
//    public partial class FormHierarchy : FormEntity
//    {
//        /// <summary>
//        /// Количество сущностей.
//        /// </summary>
//        //private NumericUpDown numCountCopy;

//        /// <summary>
//        /// Надпись "количество" сущностей.
//        /// </summary>

//        /// <summary>
//        /// Конструктор.
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="parentSign"></param>
//        public FormHierarchy(Type entityType, EntitySign parentSign)
//        {
//            InitializeComponent();

//            //var countControls = CountControls();
//            //numCountCopy = countControls.Item1;
//            //Controls.Add(numCountCopy);
//            //Controls.Add(countControls.Item2);
//            currEntity = ec.CreateEntity(entityType);
//            this.entityType = entityType;

//            if (entityType == typeof(Location))
//            {
//                var loc = currEntity as Location;
//                loc.Number = ec.GetNumber(loc);
//                currPlan = loc.Plan;
//            }
//            else
//            {
//                var equip = currEntity as Equipment;
//                var newParent = (Hierarchy)ec.GetEntity(parentSign);
//                equip.Parent = newParent;
//                equip.Number = ec.GetNumberChild(newParent, entityType);
//            }
//            //Text = "Добавить";
//        }

//        ///// <summary>
//        ///// Событие по добавлению сущности в БД.
//        ///// </summary>
//        //public event Action<EntityBase> EntityChanged;

//        ///// <summary>
//        ///// Событие по добавлению сущности в БД.
//        ///// </summary>
//        //public event Action<byte[]> EntityChanged2;

//        /// <summary>
//        /// Количество сущностей.
//        /// </summary>
//        //public int CountCopy { get => (int)numCountCopy.Value; }

//        /// <summary>
//        /// Обработчик события кнопки.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        protected override void BtnOK_Click(object sender, EventArgs e)
//        {
//            //stratOKAdd(sender, e, CountCopy);
//        }

//        protected override void CreateControls(int yPosControl)
//        {
//            base.CreateControls(yPosControl);
//            //if (entityType == typeof(Extinguisher))
//            //    GetWeightPressure(cbxKindEquipment);
//        }

//        /// <summary>
//        /// Обработчик события загрузки формы.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        //private void FormAddEntity_Load(object sender, EventArgs e) => CreateControls(50);
//    }
//}
