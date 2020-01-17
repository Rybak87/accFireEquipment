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
        private static bool CorrectSample(this TextBox textBox)
        {
            var sourse = textBox.Text;
            var chars = GetterOfType.GetSampleChars(textBox.Tag as Type);
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

        /// <summary>
        /// Проверяет корректность шаблонов именования.
        /// </summary>
        public static int CorrectSample(this IEnumerable<TextBox> textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                var type = textBox.Tag as Type;
                if (!CorrectSample(textBox))
                {
                    MessageBox.Show($"Неккоректный шаблон: " + GetterOfType.GetTraslateMany(type));
                    return 0;
                }
            }
            return 1;
        }
    }
}
