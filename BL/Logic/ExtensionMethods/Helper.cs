using System;

namespace BL
{
    /// <summary>
    /// Методы расширения.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Возвращает разницу в месяцах.
        /// </summary>
        public static int SubtractMonths(this DateTime dt1, DateTime dt2) => dt1.Year * 12 + dt1.Month - dt2.Year * 12 - dt2.Month;
    }
}
