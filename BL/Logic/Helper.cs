using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BL
{
    public static class Helper
    {
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
        public static int SubtractMonths(this DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.Year * 12 + dateTime1.Month - dateTime2.Year * 12 - dateTime2.Month;
        }

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

    public static class HelperListView
    {
        /// <summary>
        /// Заполняет TreeView данными из БД.
        /// </summary>
        public static void EntityReport(this ListView listView, Type type, FilterSet nameGroups, params FilterSet[] filterSet)
        {
            if (filterSet.Length == 0)
                return;
            List<ListViewGroup> groups = new List<ListViewGroup>();
            using (var ec = new EntityController())
            {
                var full = ec.GetTableList(type).Cast<Equipment>().OrderBy(ent => ent.Parent.ToString()).ThenBy(ent => ent.ToString());
                foreach (var ent in full)
                {
                    var row = FilterWork.CreateResultStrings(ent, filterSet);
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
    }
}
