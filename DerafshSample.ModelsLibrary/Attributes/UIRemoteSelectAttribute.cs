using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DerafshSample.ModelsLibrary.Enumeration;

namespace DerafshSample.ModelsLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class UIRemoteSelectAttribute : UIHintAttribute
    {
        public UIRemoteSelectAttribute(string controller)
            : base("UIRemoteSelect", "MVC")
        {
            Controller = controller;
        }
        public virtual string Controller { get; }
    }
}
