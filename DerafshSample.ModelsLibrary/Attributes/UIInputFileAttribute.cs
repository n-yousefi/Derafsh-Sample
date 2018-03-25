using System;
using System.ComponentModel.DataAnnotations;
using DerafshSample.ModelsLibrary.Enumeration;

namespace DerafshSample.ModelsLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class UIInputFileAttribute : UIHintAttribute
    {
        public UIInputFileAttribute() 
            :base("UIInputFile", "MVC")
        {

        }
    }
}
