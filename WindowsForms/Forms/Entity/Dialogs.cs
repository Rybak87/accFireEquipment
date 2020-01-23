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
        /// Обновляемый PictureContainer
        /// </summary>
        public static Plan PictureContainer { get; set; }

        /// <summary>
        ///  Родительская форма для диалоговых окон.
        /// </summary>
        public static Form Owner { get; set; }

        /// <summary>
        /// Диалог добавления сущности в БД.
        /// </summary>
        /// <param name="typeEntity">Тип сущности.</param>
        /// <param name="parentSign">Идентификатор родителя новой сущности.</param>
        public static void AddDialog(Type typeEntity, EntitySign parentSign=null)
        {
            if (typeEntity.IsSubclassOf(typeof(KindBase)))
            {
                var frmAdd = new FormAddKind(typeEntity);
                frmAdd.ShowDialog(Owner);
                frmAdd.Dispose();
            }

            else if (typeEntity == typeof(Location))
            {
                var frmAdd = new FormWorkLocation();
                frmAdd.EntityChanged += ent => TreeView.NodeAdd(ent as Hierarchy);
                frmAdd.EntityChanged2 += PictureContainer.LoadImage;
                frmAdd.ShowDialog(Owner);
                frmAdd.Dispose();
            }
            else if (typeEntity.IsSubclassOf(typeof(Equipment)))
            {
                if (typeEntity == typeof(Extinguisher))
                {
                    var frmAdd = new FormWorkExtinguisher(typeEntity, parentSign);
                    frmAdd.EntityChanged += ent => TreeView.NodeAdd(ent as Hierarchy);
                    frmAdd.ShowDialog(Owner);
                    frmAdd.Dispose();
                }
                else
                {
                    var frmAdd = new FormWorkEquipment(typeEntity, parentSign);
                    frmAdd.EntityChanged += ent => TreeView.NodeAdd(ent as Hierarchy);
                    frmAdd.ShowDialog(Owner);
                    frmAdd.Dispose();
                }
            }
        }

        /// <summary>
        /// Диалог изменения сущности в БД.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        public static void EditDialog(EntitySign sign)
        {
            if (sign == null)
                return;
            if (sign.Type == typeof(Location))
            {
                var frmEdit = new FormWorkLocation(sign);
                frmEdit.EntityChanged2 += PictureContainer.LoadImage;
                frmEdit.ShowDialog(Owner);
                frmEdit.Dispose();
            }
            else if (sign.Type.IsSubclassOf(typeof(Equipment)))
            {
                if (sign.Type == typeof(Extinguisher))
                {
                    var frmAdd = new FormWorkExtinguisher(sign);
                    frmAdd.EntityChanged += ent => TreeView.NodeAdd(ent as Hierarchy);
                    frmAdd.ShowDialog(Owner);
                    frmAdd.Dispose();
                }
                else
                {
                    var frmEdit = new FormWorkEquipment(sign);
                    frmEdit.ShowDialog(Owner);
                    frmEdit.Dispose();
                }
            }
        }
    }
}
