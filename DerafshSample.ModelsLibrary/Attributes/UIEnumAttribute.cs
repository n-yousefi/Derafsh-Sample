using System;
using System.ComponentModel.DataAnnotations;
using DerafshSample.ModelsLibrary.Enumeration;

namespace DerafshSample.ModelsLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class UIEnumAttribute : UIHintAttribute
    {
        public UIEnumAttribute(Type enumType, 
            EnumDisplay displayType=EnumDisplay.DropDown,
            bool disabled = false) 
            :base("UIEnum", "MVC")
        {
            Type = enumType;
            DisplayType = displayType;
            Disabled = disabled;
        }

        public virtual Type Type { get; }
        public virtual EnumDisplay DisplayType { get; }
        public bool Disabled { get; set; }
    }
}
