using System;
using System.Windows.Forms;

namespace BL
{
    public class ControlAttribute : Attribute
    {
        public string Control { get; set; }
        public bool IsRequired { get; set; }
        public string NameClass { get; set; }
        public bool IsCanHide { get; set; }
        public ControlAttribute(string control, bool isRequired)
        {
            Control = control;
            IsRequired = isRequired;
            IsCanHide = false;
        }
        public ControlAttribute(string control, bool isRequired, string nameClass)
        {
            Control = control;
            IsRequired = isRequired;
            NameClass = nameClass;
        }
        public ControlAttribute(string control, bool isRequired, string nameClass, bool isCanHide)
        {
            Control = control;
            IsRequired = isRequired;
            NameClass = nameClass;
            IsCanHide = isCanHide;
        }
    }
}
