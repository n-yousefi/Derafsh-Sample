using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.Enumeration;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class Identity:BaseViewModel
    {
        [UIEnum(typeof(IdentityEnum),EnumDisplay.Radio)]
        [Display(Name = "Identity Type", Order = 1)]
        public int IdentityEnumId { get; set; } 

        [HiddenInput(DisplayValue = false)]
        public int? PersonId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? OrganizationId { get; set; }
    }
}
