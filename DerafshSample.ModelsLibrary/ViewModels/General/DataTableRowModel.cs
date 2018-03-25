using System;
using System.Collections.Generic;
using System.Text;
using DerafshSample.ModelsLibrary.Enumeration;

namespace DerafshSample.ModelsLibrary.ViewModels.General
{
    public class DataTableRowModel
    {

    }

    public class LabelCol : DataTableRowModel
    {
         public string Key { get; set; }
    }

    public class LinkCol : DataTableRowModel
    {
        public string Url { get; set; }
        public string UrlTitle { get; set; }
        public string ColTitle { get; set; }
    }
}
