using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Рефлексия.
    /// </summary>
    public static class Reflection
    {
        /// <summary>
        /// Поиск аттрибута конкретного типа.
        /// </summary>
        private static TAttr GetAttribute<TAttr>(PropertyInfo pi) where TAttr : Attribute =>
            pi.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(TAttr)) as TAttr;

        /// <summary>
        /// Возвращает коллекцию кортежей (свойство, атрибут элементов управления, название свойства)
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns></returns>
        public static IEnumerable<(PropertyInfo prop, ControlAttribute cntrlAttr, string name)> GetEditProperties(EntityBase entity)
        {
            var result = new List<(PropertyInfo prop, ControlAttribute cntrlAttr, string name)>();
            foreach (PropertyInfo prop in entity?.GetType().GetProperties())
            {
                var controlAttr = GetAttribute<ControlAttribute>(prop);
                if (controlAttr == null)
                    continue;

                var columnAttr = GetAttribute<ColumnAttribute>(prop)?.Name;
                result.Add((prop, controlAttr, columnAttr));
            }
            result = result.OrderBy(i => i.cntrlAttr.orderNumber).ToList();
            return result;
        }

        /// <summary>
        /// Возвращает свойства пожарного инвентаря с атрибутом создания элементов.
        /// </summary>
        /// <param name="entity">Пожарный инвентарь.</param>
        public static List<PropertyInfo> GetPropertiesWithControlAttribute(Equipment entity) =>
            entity?.GetType().GetProperties().Where(p => GetAttribute<ControlAttribute>(p) != null).ToList();
    }
}
