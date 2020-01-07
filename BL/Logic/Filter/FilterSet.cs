using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Множество фильтров.
    /// </summary>
    public class FilterSet
    {
        /// <summary>
        /// Конструктор из одного фильтра.
        /// </summary>
        /// <param name="condition">Условие.</param>
        /// <param name="execution">Исполнение.</param>
        /// <param name="required">Обязательность.</param>
        public FilterSet(Func<Equipment, bool> condition, Func<Equipment, string> execution, bool required = false)
        {
            Required = required;
            Filters = new Filter[] { new Filter(condition, execution) };
        }

        /// <summary>
        /// Конструктор из одного фильтра.
        /// </summary>
        /// <param name="execution">Исполнение.</param>
        /// <param name="required">Обязательность.</param>
        public FilterSet(Func<Equipment, string> execution, bool required = false)
        {
            Required = required;
            Filters = new Filter[] { new Filter(execution) };
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="funcArray">Массив фильтров.</param>
        /// <param name="required">Обязательность.</param>
        public FilterSet(Filter[] funcArray, bool required = false)
        {
            Required = required;
            Filters = funcArray;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="required">Обязательность.</param>
        /// <param name="funcArray">Массив фильтров.</param>
        public FilterSet(bool required = false, params Filter[] funcArray)
        {
            Required = required;
            Filters = funcArray;
        }

        /// <summary>
        /// Конструктор из одного фильтра, с обязательностью "Истинно".
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        public FilterSet(Filter filter)
        {
            Required = true;
            Filters = new Filter[] { filter };
        }

        /// <summary>
        /// Коллекция фильтров.
        /// </summary>
        private Filter[] Filters { get; }

        /// <summary>
        /// Обязательность этого множества.
        /// </summary>
        public bool Required { get; }

        //public Filter this[int index]
        //{
        //    get
        //    {
        //        return Filters[index];
        //    }
        //    set
        //    {
        //        Filters[index] = value;
        //    }
        //}
        //public IEnumerator GetEnumerator()
        //{
        //    return Filters.GetEnumerator();
        //}

        /// <summary>
        /// Возвращает строку в соответствии с множеством фильтров.
        /// </summary>
        /// <param name="entity">Пожарный инвентарь.</param>
        /// <returns></returns>
        public string CreateResultString(Equipment entity)
        {
            string result = string.Empty;
            foreach (Filter filter in Filters)
                if (filter.Condition(entity))
                    result += filter.Execution(entity);
            return result;
        }
    }
}
