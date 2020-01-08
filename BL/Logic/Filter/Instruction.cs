using System;

namespace BL
{
    /// <summary>
    /// Инструкция.
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="condition">Условие.</param>
        /// <param name="execution">Исполнение.</param>
        public Instruction(Func<Equipment, bool> condition, Func<Equipment, string> execution)
        {
            Condition = condition;
            Execution = execution;
        }

        /// <summary>
        /// Конструктор. Условие "истинно"
        /// </summary>
        /// <param name="execution">Исполнение.</param>
        public Instruction(Func<Equipment, string> execution)
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
