using BL;
using System.Data.Entity;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Стратегия редактирования сущности.
    /// </summary>
    public class EditStrategy : Strategy
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public EditStrategy()
        { }

        /// <summary>
        /// Возвращает имя формы.
        /// </summary>
        /// <param name="entityBase">Сущность.</param>
        /// <returns></returns>
        public override string GetFormName(EntityBase entityBase) => "Изменить " + entityBase.ToString();

        /// <summary>
        /// Работа с сущностью при нажатии ОК.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ec"></param>
        public override void ApplyChanged(EntityBase entity, EntityController ec)
        {
            ec.Entry(entity).State = EntityState.Modified;

            var eq = entity as Equipment;
            if (eq != null)
            {
                var histories = eq.GetNewHistories();
                ec.Set<History>().AddRange(histories);
            }
            ec.SaveChanges();
        }

        /// <summary>
        /// Не добавляет контролов.
        /// </summary>
        /// <returns></returns>
        public override Control[] GetBeforeControls() => null;
    }
}
