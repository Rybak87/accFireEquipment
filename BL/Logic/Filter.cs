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
        public Func<EntityBase, bool> Condition { get; }
        public Func<EntityBase, string> Execution { get; }


        public Filter(Func<EntityBase, bool> condition, Func<EntityBase, string> execution/*, bool required = false*/)
        {
            Condition = condition;
            Execution = execution;
            //Required = required;
        }
    }
    public class FilterSet
    {
        Filter[] Filters { get; }
        public bool Required { get; }
        public FilterSet(Func<EntityBase, bool> condition, Func<EntityBase, string> execution, bool required = false)
        {
            Required = required;
            Filters = new Filter[] { new Filter(condition, execution/*, required*/) };
        }
        public FilterSet(Filter[] funcArray)
        {
            Required = true;
            Filters = funcArray;
        }
        public FilterSet(Filter filter)
        {
            Required = true;
            Filters = new Filter[] { filter };
        }
        public Filter this[int index]
        {
            get
            {
                return Filters[index];
            }
            set
            {
                Filters[index] = value;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return Filters.GetEnumerator();
        }

        private string CreateResultString(EntityBase entity, FilterSet filters)
        {
            string result = string.Empty;
            foreach (Filter filter in filters)
                if (filter.Condition(entity))
                    result += filter.Execution(entity);
            return result;
        }
        private string[] CreateResultStrings(EntityBase entity, FilterSet[] filterSet)
        {
            var countFilters = filterSet.Length;
            string[] result = new string[countFilters];
            for (int i = 0; i < countFilters; i++)
            {
                if (filterSet[i].Required)
                    result[i] = CreateResultString(entity, filterSet[i]);
                if (result[i] != string.Empty)
                    return null;
            }
            for (int i = 0; i < countFilters; i++)
            {
                if (!filterSet[i].Required)
                    result[i] = CreateResultString(entity, filterSet[i]);
            }
            return result;
        }
        private List<string[]> CreateResultStringsTable(Type type, params FilterSet[] filterSet)
        {
            if (filterSet.Length == 0)
                return null;
            List<string[]> result = new List<string[]>();
            using (var ec = new EntityController())
            {
                var full = ec.GetTableList(type);
                foreach (var ent in full)
                {
                    var str = CreateResultStrings(ent, filterSet);
                    if (str != null)
                        result.Add(str);
                }
            }
            return result;
        }
    }
    public static class FilterWork
    {
        private static string CreateResultString(EntityBase entity, FilterSet filters)
        {
            string result = string.Empty;
            foreach (Filter filter in filters)
                if (filter.Condition(entity))
                    result += filter.Execution(entity);
            return result;
        }
        public static string[] CreateResultStrings(EntityBase entity, FilterSet[] filterSet)
        {
            var countFilters = filterSet.Length;
            string[] result = new string[countFilters];
            for (int i = 0; i < countFilters; i++)
            {
                if (filterSet[i].Required)
                    result[i] = CreateResultString(entity, filterSet[i]);
                if (result[i] != string.Empty)
                    return null;
            }
            for (int i = 0; i < countFilters; i++)
            {
                if (!filterSet[i].Required)
                    result[i] = CreateResultString(entity, filterSet[i]);
            }
            return result;
        }
        public static List<string[]> CreateResultStringsTable(Type type, params FilterSet[] filterSet)
        {
            if (filterSet.Length == 0)
                return null;
            List<string[]> result = new List<string[]>();
            using (var ec = new EntityController())
            {
                var full = ec.GetTableList(type);
                foreach (var ent in full)
                {
                    var str = CreateResultStrings(ent, filterSet);
                    if (str != null)
                        result.Add(str);
                }
            }
            return result;
        }
    }
}
