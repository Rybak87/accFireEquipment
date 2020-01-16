using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Фильтр.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Конструктор из одной инструкции.
        /// </summary>
        /// <param name="condition">Условие.</param>
        /// <param name="execution">Исполнение.</param>
        /// <param name="required">Обязательность множества фильтров.</param>
        public Filter(Func<Equipment, bool> condition, Func<Equipment, string> execution, bool required = false)
        {
            Required = required;
            Instructions = new Instruction[] { new Instruction(condition, execution) };
        }

        /// <summary>
        /// Конструктор из одной инструкции.
        /// </summary>
        /// <param name="execution">Исполнение.</param>
        /// <param name="required">Обязательность множества фильтров.</param>
        public Filter(Func<Equipment, string> execution, bool required = false)
        {
            Required = required;
            Instructions = new Instruction[] { new Instruction(execution) };
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="arr">Массив фильтров.</param>
        /// <param name="required">Обязательность множества фильтров.</param>
        public Filter(Instruction[] arr, bool required = false)
        {
            Required = required;
            Instructions = arr;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="required">Обязательность множества фильтров.</param>
        /// <param name="arr">Массив фильтров.</param>
        public Filter(bool required = false, params Instruction[] arr)
        {
            Required = required;
            Instructions = arr;
        }

        /// <summary>
        /// Конструктор из одного фильтра, с обязательностью "Истинно".
        /// </summary>
        /// <param name="instruction">Фильтр.</param>
        public Filter(Instruction instruction)
        {
            Required = true;
            Instructions = new Instruction[] { instruction };
        }

        /// <summary>
        /// Коллекция фильтров.
        /// </summary>
        private Instruction[] Instructions { get; }

        /// <summary>
        /// Обязательность этого множества.
        /// </summary>
        public bool Required { get; }

        /// <summary>
        /// Возвращает строку в соответствии с множеством инструкций.
        /// </summary>
        /// <param name="entity">Пожарный инвентарь.</param>
        /// <returns></returns>
        public string CreateResultString(Equipment entity)
        {
            string result = string.Empty;
            foreach (Instruction instr in Instructions)
                if (instr.Condition(entity))
                    result += instr.Execution(entity);
            return result;
        }
    }
}
