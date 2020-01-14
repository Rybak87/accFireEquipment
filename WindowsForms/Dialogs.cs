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
        public static PictureContainer PictureContainer { get; set; }

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
            var AddEssForm = new FormAddHierarchy(typeEntity, parentSign);
            AddEssForm.EntityAdd += ent => TreeView.NodeAdd(ent as Hierarchy);
            AddEssForm.EntityAdd2 += PictureContainer.LoadImage;
            DialogResult result = AddEssForm.ShowDialog(Owner);
            if (result == DialogResult.Cancel)
                return;
        }

        /// <summary>
        /// Диалог изменения сущности в БД.
        /// </summary>
        /// <param name="sign">Идентификатор сущности.</param>
        public static void EditDialog(EntitySign sign)
        {
            var AddEssForm = new FormEditEntity(sign);
            AddEssForm.EntityEdit += TreeView.NodeMove;
            AddEssForm.EntityEdit2 += PictureContainer.LoadImage;
            DialogResult result = AddEssForm.ShowDialog(Owner);
            if (result == DialogResult.Cancel)
                return;
        }
    }
}
