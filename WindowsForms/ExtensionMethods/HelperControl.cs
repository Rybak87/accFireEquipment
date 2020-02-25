using BL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Методы расширения для элементов формы.
    /// </summary>
    public static class HelperControl
    {
        /// <summary>
        /// Посылка сообщений в приложение.
        /// </summary>
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
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
        /// Проверяет корректность шаблонов именования.
        /// </summary>
        public static bool CorrectSample(IEnumerable<TextBox> textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                var type = textBox.Tag as Type;
                if (!GetterOfType.CorrectSample(textBox.Text, type))
                {
                    MessageBox.Show($"Неккоректный шаблон: " + GetterOfType.GetTraslateMany(type));
                    return false;
                }
            }
            return true;
        }
    }
}
