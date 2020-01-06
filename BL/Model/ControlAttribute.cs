using System;

namespace BL
{
    /// <summary>
    /// Аттрибут для создание элементов управления на форме.
    /// </summary>
    public class ControlAttribute : Attribute
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ControlAttribute(string control, bool isRequired)
        {
            Control = control;
            IsRequired = isRequired;
        }

        /// <summary>
        /// Тип элемента.
        /// </summary>
        public string Control { get; set; }

        /// <summary>
        /// Необходимость заполнения.
        /// </summary>
        public bool IsRequired { get; set; }

        //public string NameClass { get; set; }
        //public ControlAttribute(string control, bool isRequired, string nameClass)
        //{
        //    Control = control;
        //    IsRequired = isRequired;
        //    NameClass = nameClass;
        //}
    }
}
