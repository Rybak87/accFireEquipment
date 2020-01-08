using System;

namespace BL
{
    /// <summary>
    /// Аттрибут для создание элементов управления на форме.
    /// </summary>
    public class ControlAttribute : Attribute
    {
        /// <summary>
        /// Тип элемента.
        /// </summary>
        public readonly string control;

        /// <summary>
        /// Необходимость заполнения.
        /// </summary>
        public readonly bool isRequired;

        /// <summary>
        /// Номер элемента на форме по порядку.
        /// </summary>
        public readonly int orderNumber;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ControlAttribute(string control, bool isRequired, int orderNumber)
        {
            this.control = control;
            this.isRequired = isRequired;
            this.orderNumber = orderNumber;
        }
    }
}
