using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Filter
    {
        public Func<EquipmentBase, bool> Condition { get; }
        public Func<EquipmentBase, string> Execution { get; }


        public Filter(Func<EquipmentBase, bool> condition, Func<EquipmentBase, string> execution)
        {
            Condition = condition;
            Execution = execution;
        }
        public Filter(Func<EquipmentBase, string> execution)
        {
            Condition = ent => true;
            Execution = execution;
        }
    }
    public class FilterSet
    {
        Filter[] Filters { get; }
        public bool Required { get; }
        public FilterSet(Func<EquipmentBase, bool> condition, Func<EquipmentBase, string> execution, bool required = false)
        {
            Required = required;
            Filters = new Filter[] { new Filter(condition, execution) };
        }
        public FilterSet(Func<EquipmentBase, string> execution, bool required = false)
        {
            Required = required;
            Filters = new Filter[] { new Filter(execution) };
        }
        public FilterSet(Filter[] funcArray, bool required = false)
        {
            Required = required;
            Filters = funcArray;
        }
        public FilterSet(bool required = false, params Filter[] funcArray)
        {
            Required = required;
            Filters = funcArray;
        }
        public FilterSet(Filter filter)
        {
            Required = true;
            Filters = new Filter[] { filter };
        }
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

        public string CreateResultString(EquipmentBase entity)
        {
            string result = string.Empty;
            foreach (Filter filter in Filters)
                if (filter.Condition(entity))
                    result += filter.Execution(entity);
            return result;
        }
    }
    public static class FilterWork
    {
        public static string[] CreateResultStrings(EquipmentBase entity, FilterSet[] filterSet)
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
