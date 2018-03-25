using System.ComponentModel.DataAnnotations;
using Derafsh.Attributes;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class State:BaseViewModel
    {
        [Display(Name = "Country", Order = 1)]
        [UIRemoteSelect("Country")]
        public int CountryId { get; set; }

        [Abstract]
        [Searchable]
        [Display(Name = "Name", Order = 2)]
        public string Name { get; set; }
    }
}
