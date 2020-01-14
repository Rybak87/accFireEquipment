using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public static class Dialogs
    {
        static Dialogs()
        {

        }

        /// <summary>
        /// Обновляемый TreeView
        /// </summary>
        public static MyTreeView TreeView { get; set; }

        /// <summary>
        ///  Родительская форма для диалоговых окон.
        /// </summary>
        public static Form Owner { get; set; }

        /// <summary>
        /// Обновляемый план.
        /// </summary>
        public static PictureContainer PictureContainer { get; set; }

        /// <summary>
        /// Диалог добавления сущности в БД.
        /// </summary>
        /// <param name="typeEntity">Тип сущности.</param>
        /// <param name="parentSign">Идентификатор родителя новой сущности.</param>
        public static void AddDialog(Type typeEntity, EntitySign parentSign)
        {
            var AddEssForm = new FormAddHierarchy(typeEntity, parentSign);
            AddEssForm.EntityAdd += ent => TreeView.NodeAdd(ent as Hierarchy);
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
            DialogResult result = AddEssForm.ShowDialog(Owner);
            if (result == DialogResult.Cancel)
                return;
        }
    }
}
