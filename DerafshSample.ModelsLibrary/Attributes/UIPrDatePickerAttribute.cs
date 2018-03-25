using System;
using System.ComponentModel.DataAnnotations;

namespace DerafshSample.ModelsLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class UIPrDatePickerAttribute : UIHintAttribute
    {
        public UIPrDatePickerAttribute()
            : base("UIPrDatePicker", "MVC")
        {
        }
    }
}
