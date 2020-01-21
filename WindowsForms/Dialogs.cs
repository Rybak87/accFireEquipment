using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static void AddDialog(Type typeEntity, EntitySign parentSign)
        {
            var frmAdd = new FormAddHierarchy(typeEntity, parentSign);
            frmAdd.EntityAdd += ent => TreeView.NodeAdd(ent as Hierarchy);
            frmAdd.EntityAdd2 += PictureContainer.LoadImage;
            DialogResult result = frmAdd.ShowDialog(Owner);
            frmAdd.Dispose();
        }

        /// <summary>
        /// Диалог изменения сущности в БД.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        public static void EditDialog(EntitySign sign)
        {
            var frmEdit = new FormEditEntity(sign);
            frmEdit.EntityEdit += TreeView.NodeMove;
            frmEdit.EntityEdit2 += PictureContainer.LoadImage;
            DialogResult result = frmEdit.ShowDialog(Owner);
            frmEdit.Dispose();
        }
    }
}
