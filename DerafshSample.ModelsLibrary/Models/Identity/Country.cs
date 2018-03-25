using System.ComponentModel.DataAnnotations;
using Derafsh.Attributes;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class Country:BaseViewModel
    {
        [Abstract]
        [Display(Name= "Country Code", Order = 1)]        
        public string CountryCode { get; set; }

        [Abstract]
        [Display(Name = "Name", Order = 2)]
        public string Name { get; set; }
    }
}
