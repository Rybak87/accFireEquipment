using System;

namespace BL
{
    public class PropAttribute : Attribute
    {
        public string Control { get; set; }
        public bool IsRequired { get; set; }
        public string NameClass { get; set; }
        public PropAttribute(string control, bool isRequired)
        {
            Control = control;
            IsRequired = isRequired;
        }
        public PropAttribute(string control, bool isRequired, string nameClass)
        {
            Control = control;
            IsRequired = isRequired;
            NameClass = nameClass;
        }
    }
}
