using System.ComponentModel.DataAnnotations;
using Derafsh.Attributes;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class City:BaseViewModel
    {
        [Display(Name = "State", Order = 1)]
        [UIRemoteSelect("State")]
        public int StateId { get; set; }

        [Display(Name = "City Code", Order = 2)]
        [Abstract]
        public string CityCode { get; set; }

        [Display(Name = "Name", Order = 3)]
        [Abstract]
        public string Name { get; set; }
    }
}
