using BL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Стратегия по работе с сущностью.
    /// </summary>
    public abstract class Strategy
    {
        /// <summary>
        /// Возвращает имя формы.
        /// </summary>
        /// <param name="entityBase">Сущность.</param>
        /// <returns></returns>
        public abstract string GetFormName(EntityBase entityBase);

        /// <summary>
        /// Работа с сущностью при нажатии ОК.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ec"></param>
        public abstract void ApplyChanged(EntityBase entity, EntityController ec);

        /// <summary>
        /// Событие по добавлению/изменению сущности в БД.
        /// </summary>
        public Action<EntityBase> EntityChanged;

        /// <summary>
        /// Событие по добавлению/изменению группы сущностей в БД.
        /// </summary>
        public Action<Hierarchy[]> HierarchyChangedRange;

        /// <summary>
        /// Возможность добавить контролы на форму перед основными.
        /// </summary>
        /// <returns></returns>
        public abstract Control[] GetBeforeControls();
    }
}
