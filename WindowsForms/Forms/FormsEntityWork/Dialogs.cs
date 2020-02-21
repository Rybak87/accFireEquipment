using BL;
using System;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Диалоги.
    /// </summary>
    public static class Dialogs
    {
        static Dialogs()
        { }

        /// <summary>
        /// Обновляемый TreeView
        /// </summary>
        public static MyTreeView TreeView { get; set; }

        /// <summary>
        /// Обновляемый Plan
        /// </summary>
        public static Plan Plan { get; set; }

        /// <summary>
        ///  Родительская форма для диалоговых окон.
        /// </summary>
        public static Form Owner { get; set; }

        /// <summary>
        /// Диалог добавления сущности в БД.
        /// </summary>
        /// <param name="typeEntity">Тип сущности.</param>
        /// <param name="parentSign">Идентификатор родителя новой сущности.</param>
        public static void AddDialog(Type typeEntity, EntitySign parentSign = null)
        {
            var strategy = new AddStrategy();
            strategy.HierarchyChangedRange += TreeView.NodeAddRange;
            FormEntity frmAdd = null;

            if (typeEntity.IsSubclassOf(typeof(KindBase)))
                frmAdd = new FormWorkKind(typeEntity, strategy);
            else if (typeEntity == typeof(Location))
            {
                frmAdd = new FormWorkLocation(strategy);
                (frmAdd as FormWorkLocation).LocationEntityChanged += Plan.LoadImage;
            }
            else if (typeEntity.IsSubclassOf(typeof(Equipment)))
            {
                if (typeEntity == typeof(Extinguisher))
                    frmAdd = new FormWorkExtinguisher(typeEntity, parentSign, strategy);
                else
                    frmAdd = new FormWorkEquipment(typeEntity, parentSign, strategy);
            }

            frmAdd.ShowDialog(Owner);
            frmAdd.Dispose();
        }

        /// <summary>
        /// Диалог изменения сущности в БД.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        public static void EditDialog(EntitySign sign)
        {
            if (sign == null)
                return;
            var strategy = new EditStrategy();
            FormEntity frmEdit = null;

            if (sign.Type.IsSubclassOf(typeof(KindBase)))
                frmEdit = new FormWorkKind(sign, strategy);
            if (sign.Type == typeof(Location))
            {
                frmEdit = new FormWorkLocation(sign, strategy);
                (frmEdit as FormWorkLocation).LocationEntityChanged += Plan.LoadImage;
            }
            else if (sign.Type.IsSubclassOf(typeof(Equipment)))
            {
                if (sign.Type == typeof(Extinguisher))
                    frmEdit = new FormWorkExtinguisher(sign, strategy);
                else
                    frmEdit = new FormWorkEquipment(sign, strategy);
            }

            frmEdit.ShowDialog(Owner);
            frmEdit.Dispose();
        }
    }
}
