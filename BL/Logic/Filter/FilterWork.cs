using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Класс для работы с фильтрами.
    /// </summary>
    public static class FilterWork
    {
        /// <summary>
        /// Возвращает массив строк в соответствии с множеством множеств фильтров.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filterSet"></param>
        /// <returns></returns>
        public static string[] CreateResultStrings(Equipment entity, FilterSet[] filterSet)
        {
            var countFilters = filterSet.Length;
            string[] result = new string[countFilters];
            for (int i = 0; i < countFilters; i++)
            {
                if (filterSet[i].Required)
                {
                    result[i] = filterSet[i].CreateResultString(entity);
                    if (result[i] == string.Empty)
                        return null;
                }
            }
            for (int i = 0; i < countFilters; i++)
            {
                if (!filterSet[i].Required)
                    result[i] = filterSet[i].CreateResultString(entity);
            }
            return result;
        }
    }
}
