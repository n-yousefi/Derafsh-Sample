using System.ComponentModel.DataAnnotations;
using Derafsh.Attributes;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class Address : BaseViewModel
    {
        [Display(Name = "Identity", Order = 1)]
        [UIRemoteSelect("Identity")]
        public int IdentityId { get; set; }

        [Display(Name = "City", Order = 2)]
        [UIRemoteSelect("City")]
        public int CityId { get; set; }

        [Display(Name = "Full Address", Order = 3)]
        [Abstract]
        public string FullAddress { get; set; }

        [Display(Name = "Description", Order = 4)]
        [Abstract]
        public string Description { get; set; }

        [Display(Name = "Postal Code", Order = 5)]
        [Abstract]
        public string PostalCode { get; set; }
    }
}
