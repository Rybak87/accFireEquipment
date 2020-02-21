using System;
using System.Linq;
using System.Windows.Forms;

namespace BL
{
    /// <summary>
    /// Методы расширения для ListView.
    /// </summary>
    public static class HelperListView
    {
        /// <summary>
        /// Заполняет TreeView данными из БД.
        /// </summary>
        public static void FillListView(this ListView listView, Type type, params Filter[] filterSet)
        {
            if (filterSet.Length == 0)
                return;
            using (var ec = new EntityController())
            {
                var groups = ec.Set<Location>().ToArray().Select(l => l.Name).OrderBy(n => n).Select(s => new ListViewGroup(s, s)).ToArray();
                listView.Groups.AddRange(groups);
                var full = ec.GetIQueryable(type).Cast<Equipment>().ToList().OrderBy(ent => ent.Parent.ToString()).ThenBy(ent => ent.ToString());
                foreach (var ent in full)
                {
                    var row = CreateFiltersStrings(ent, filterSet);
                    if (row == null)
                        continue;

                    var groupName = ent.GetLocation.Name;
                    var group = listView.Groups.Cast<ListViewGroup>().First(g => g.Name == groupName);
                    var item = new ListViewItem(row[0], group);

                    var subItems = row.Skip(1).Select(s => new ListViewItem.ListViewSubItem(item, s)).ToArray();
                    item.SubItems.AddRange(subItems);
                    item.Tag = ent.GetSign();
                    listView.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Возвращает массив строк в соответствии с множеством множеств фильтров.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filterSet"></param>
        /// <returns></returns>
        private static string[] CreateFiltersStrings(Equipment entity, Filter[] filterSet)
        {
            var calcStrings = filterSet.Select(f => (f.Required, calcString: f.CreateResultString(entity)));
            bool requiredFilterCalcEmpty = calcStrings.Any(h => h.Required && h.calcString == string.Empty);
            if (requiredFilterCalcEmpty)
                return null;
            return calcStrings.Select(i => i.calcString).ToArray();
        }

        /// <summary>
        /// Инициализация колонок в ListView.
        /// </summary>
        /// <param name="columnsNames"></param>
        /// /// <param name="listView"></param>
        public static void InitColumns(this ListView listView, params string[] columnsNames)
        {
            listView.Clear();
            listView.Groups.Clear();
            var countColumns = columnsNames.Count();
            var columnWidth = listView.Width / countColumns;
            var columnHeaders = new ColumnHeader[countColumns];
            int count = 0;
            foreach (var name in columnsNames)
            {
                var columnHeader = new ColumnHeader()
                {
                    Text = name,
                    Width = columnWidth
                };
                columnHeaders[count] = columnHeader;
                count++;
            }
            listView.Columns.AddRange(columnHeaders);
        }
    }
}
