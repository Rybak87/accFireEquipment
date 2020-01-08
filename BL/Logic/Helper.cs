using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BL
{
    /// <summary>
    /// Методы расширения.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Посылка сообщений в приложение.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        /// <summary>
        /// Отключает перерисовку для Control.
        /// </summary>
        public static void SuspendDrawing(this Control Target)
        {
            SendMessage(Target.Handle, WM_SETREDRAW, false, 0);
        }

        /// <summary>
        /// Включает перерисовку для Control.
        /// </summary>
        public static void ResumeDrawing(this Control Target)
        {
            SendMessage(Target.Handle, WM_SETREDRAW, true, 0);
            Target.Invalidate(true);
            Target.Parent.Invalidate(true);
            Target.Update();
            Target.Parent.Update();
        }

        /// <summary>
        /// Возвращает разницу в месяцах.
        /// </summary>
        public static int SubtractMonths(this DateTime dt1, DateTime dt2) => dt1.Year * 12 + dt1.Month - dt2.Year * 12 - dt2.Month;

        /// <summary>
        /// Проверяет корректность шаблона именования.
        /// </summary>
        public static bool CorrectSample(this string sourse, params char[] chars)
        {
            for (int i = 0; i < sourse.Length; i++)
            {
                var ch = sourse[i];
                if (ch == '#')
                {
                    if (i + 1 == sourse.Length)
                        return false;
                    if (!chars.Contains(sourse[i + 1]))
                        return false;
                }
            }
            return true;
        }
    }

    /// <summary>
    /// Методы расширения для ListView.
    /// </summary>
    public static class HelperListView
    {
        public static Filter filterName = new Filter(ent => ent.ToString());
        public static Filter filterParent = new Filter(ent => ent.Parent.ToString());
        public static Filter filterLocation = new Filter(ent => ent.GetLocation.ToString());
        public static Filter filterFireCabinetFault = new Filter(true,
                                    new Instruction(ent => ((FireCabinet)ent).IsDented, ent => "Поврежден; "),
                                    new Instruction(ent => !((FireCabinet)ent).IsSticker, ent => "Без наклейки; ")
                                    );
        public static Filter filterExtinguisherFault = new Filter(true,
                                    new Instruction(ent => ((Extinguisher)ent).IsDented, ent => "Поврежден; "),
                                    new Instruction(ent => !((Extinguisher)ent).IsSticker, ent => "Без наклейки; "),
                                    new Instruction(ent => !((Extinguisher)ent).IsHose, ent => "Нет шланга; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsLabelDamage, ent => "Повреждена этикетка; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsHandleDamage, ent => "Повреждено ЗПУ; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsPaintDamage, ent => "Повреждена краска; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsPressureGaugeFault, ent => "Поврежден манометр; "),
                                    new Instruction(ent => ((Extinguisher)ent).Pressure < ((Extinguisher)ent).KindExtinguisher.MinPressure, ent => "Давление менее допустимого; "),
                                    new Instruction(ent => ((Extinguisher)ent).Weight < ((Extinguisher)ent).KindExtinguisher.MinWeight, ent => "Вес менее допустимого; ")
                                    );
        public static Filter filterHoseFault = new Filter(true,
                                    new Instruction(ent => ((Hose)ent).IsRagged, ent => "Порван; "),
                                    new Instruction(ent => ((Hose)ent).DateRolling.Subtract(DateTime.Now).Days < 30, ent => "Необходима перекатка; ")
                                    );
        public static Filter filterHydrantFault = new Filter(ent => ((Hydrant)ent).IsDamage, ent => "Поврежден; ", true);
        public static Filter filterExtinguisherRecharge = new Filter(true,
            new Instruction(ent => ((Extinguisher)ent).DateRecharge.Subtract(DateTime.Now).Days < 365, ent => ((Extinguisher)ent).DateRecharge.SubtractMonths(DateTime.Now).ToString())
        );


        /// <summary>
        /// Заполняет TreeView данными из БД.
        /// </summary>
        public static void EntityReport(this ListView listView, Type type, Filter nameGroups, params Filter[] filterSet)
        {
            if (filterSet.Length == 0)
                return;
            List<ListViewGroup> groups = new List<ListViewGroup>();
            using (var ec = new EntityController())
            {
                var full = ec.GetTableList(type).Cast<Equipment>().OrderBy(ent => ent.Parent.ToString()).ThenBy(ent => ent.ToString());
                foreach (var ent in full)
                {
                    var row = CreateFiltersStrings(ent, filterSet);
                    if (row == null)
                        continue;
                    ListViewItem item;
                    if (nameGroups != null)
                    {
                        var groupName = nameGroups.CreateResultString(ent);
                        var group = groups.SingleOrDefault(g => g.Name == groupName);
                        if (group == null)
                        {
                            group = new ListViewGroup(groupName, groupName);
                            groups.Add(group);
                        }
                        item = new ListViewItem(row[0], group);
                    }
                    else
                    {
                        item = new ListViewItem(row[0]);
                    }
                    var countSbItems = filterSet.Length - 1;
                    var subItems = new ListViewItem.ListViewSubItem[countSbItems];
                    for (int i = 0; i < countSbItems; i++)
                    {
                        var newSubitem = new ListViewItem.ListViewSubItem(item, row[i + 1]);
                        subItems[i] = newSubitem;
                    }
                    item.SubItems.AddRange(subItems);
                    item.Tag = ent.GetSign();
                    listView.Items.Add(item);
                }
            }
            List<ListViewGroup> currGroups = new List<ListViewGroup>();
            currGroups.AddRange(listView.Groups.Cast<ListViewGroup>());
            foreach (var gr in groups)
            {
                if (!currGroups.Select(g => g.Name).Contains(gr.Name))
                    currGroups.Add(gr);
                else
                {
                    var addGroup = currGroups.Single(g => g.Name == gr.Name);
                    addGroup.Items.AddRange(gr.Items);
                }
            }
            var groupsSortArray = currGroups.OrderBy(g => g.Header).ToArray();
            listView.Groups.Clear();
            listView.Groups.AddRange(groupsSortArray);

        }

        /// <summary>
        /// Возвращает массив строк в соответствии с множеством множеств фильтров.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filterSet"></param>
        /// <returns></returns>
        public static string[] CreateFiltersStrings(Equipment entity, Filter[] filterSet)
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
