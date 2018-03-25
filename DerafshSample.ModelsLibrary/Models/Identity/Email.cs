using System.ComponentModel.DataAnnotations;
using Derafsh.Attributes;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.ViewModels.Base;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class Email :BaseViewModel
    {
        [Display(Name = "Identity", Order = 1)]
        [UIRemoteSelect("Identity")]
        public int IdentityId { get; set; }

        [Abstract]
        [Display(Name = "Mail Address", Order = 2)]
        public string MailAddress { get; set; }

        [Abstract]
        [Display(Name = "Description", Order = 3)]
        public string Description { get; set; }
    }
}
