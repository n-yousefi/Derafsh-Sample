using System.ComponentModel.DataAnnotations;
using Derafsh.Attributes;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class Phone : BaseViewModel
    {
        [Display(Name = "Identity", Order = 1)]
        [UIRemoteSelect("Identity")]
        public int IdentityId { get; set; }

        [Abstract]
        [Searchable]
        [Display(Name = "Number", Order = 2)]
        public string Number { get; set; }
        
    }
}
