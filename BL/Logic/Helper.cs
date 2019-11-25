﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BL
{
    public static class Helper
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(this Control Target)
        {
            SendMessage(Target.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(this Control Target)
        {
            SendMessage(Target.Handle, WM_SETREDRAW, true, 0);
            Target.Invalidate(true);
            Target.Parent.Invalidate(true);
            Target.Update();
            Target.Parent.Update();
        }
        public static int SubtractMonths(this DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.Year * 12 + dateTime1.Month - dateTime2.Year * 12 - dateTime2.Month;
        }

    }
}
