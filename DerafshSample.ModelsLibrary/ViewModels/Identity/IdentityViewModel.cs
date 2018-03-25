using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Derafsh.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using DerafshSample.ModelsLibrary.Enumeration;
using DerafshSample.ModelsLibrary.Interfaces;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Address;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Email;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Organization;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Person;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Phone;

namespace DerafshSample.ModelsLibrary.ViewModels.Identity
{
    [Table("Identity")]
    public class IdentityViewModel:Models.Identity.Identity,ISelectable
    {
        private PersonViewModel _person;
        private OrganizationViewModel _organization;

        [Join("PersonId")]
        [Abstract]
        [Display(Order = 2)]
        public PersonViewModel Person
        {
            get => IdentityEnumId != (int) IdentityEnum.Person ? null : _person;
            set => _person = value;
        }

        [Join("OrganizationId")]
        [Abstract]
        [Display(Order = 2)]
        public OrganizationViewModel Organization
        {
            get => IdentityEnumId != (int) IdentityEnum.Organization ? null : _organization;
            set => _organization = value;
        }

        [InverseJoin("IdentityId")]
        [Display(Name= "Phones", Order = 3)]        
        public List<PhoneViewModel> Phone { get; set; }

        [InverseJoin("IdentityId")]
        [Display(Name= "Addresses", Order = 4)]        
        public List<AddressViewModel> Address { get; set; }

        [InverseJoin("IdentityId")]
        [Display(Name = "Emails", Order = 5)]        
        public List<EmailViewModel> Email { get; set; }

        public SelectListItem ConvertToSelectListItem()
        {
            return new SelectListItem()
            {
                Text = (Person?.Name)??(Organization?.Name),
                Value = Id.ToString()
            };
        }
    }
}
