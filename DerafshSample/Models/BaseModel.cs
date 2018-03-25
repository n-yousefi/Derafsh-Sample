using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DerafshSample.ModelsLibrary.ViewModels.General;

namespace DerafshSample.Models
{
    public class BaseModel
    {
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool HierarchyLoading { get; set; }
        public bool HasScript { get; set; }
        public List<LinkCol> LinkRows { get; set; }
        public List<string> DisableProperties { get; set; }
    }
}
