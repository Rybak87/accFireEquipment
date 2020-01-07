using System;

namespace BL
{
    /// <summary>
    /// Фильтр.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="condition">Условие.</param>
        /// <param name="execution">Исполнение.</param>
        public Filter(Func<Equipment, bool> condition, Func<Equipment, string> execution)
        {
            Condition = condition;
            Execution = execution;
        }

        /// <summary>
        /// Конструктор. Условие "истинно"
        /// </summary>
        /// <param name="execution">Исполнение.</param>
        public Filter(Func<Equipment, string> execution)
        {
            Condition = ent => true;
            Execution = execution;
        }

        /// <summary>
        /// Условие.
        /// </summary>
        public Func<Equipment, bool> Condition { get; }

        /// <summary>
        /// Исполнение.
        /// </summary>
        public Func<Equipment, string> Execution { get; }
    }

    
}
