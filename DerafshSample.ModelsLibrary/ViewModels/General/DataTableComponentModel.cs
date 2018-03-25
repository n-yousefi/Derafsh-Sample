using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.ViewModels.General
{
    public class DataTableComponentModel
    {
        public string Title { get; set; }
        public string TableId { get; set; }
        public List<LinkCol> LinkRows { get; set; }        
        public string Name { get; set; }
        public DataTable DataTable { get; set; }
        public int PageCount { get; set; }

        public string Entity { get; set; }
        public int EntityId { get; set; }

        public string FetchUrl { get; set; }
        public string CreateUrl { get; set; }
    }
}
