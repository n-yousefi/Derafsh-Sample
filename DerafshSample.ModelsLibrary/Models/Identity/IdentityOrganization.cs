using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Derafsh.Attributes;
using Microsoft.AspNetCore.Mvc;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.Enumeration;


namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class IdentityOrganization
    {
        [PrimaryKey]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Name", Order = 1)]
        [Abstract]
        public string Name { get; set; }

        [Display(Name = "Registration Number", Order = 2)]
        [Abstract]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Economical Number", Order = 3)]
        public string EconomicalNumber { get; set; }

        [Display(Name = "National Number", Order = 4)]
        public string NationalNumber { get; set; }

        [Display(Name = "Organization Status", Order = 5)]
        [UIEnum(typeof(OrganizationStatusEnum))]
        public int OrganizationStatusEnumId { get; set; }

        
        [Display(Name = "Organization Type", Order = 6)]
        [UIEnum(typeof(OrganizationEnum))]
        public int OrganizationEnumId { get; set; }

        
        [Display(Name = "Registration City", Order = 8)]
        [UIRemoteSelect("City")]
        public int? RegistrationCityId { get; set; }
        
   
        [Display(Name = "Register Date", Order = 4)]
        [ScaffoldColumn(false)]
        public DateTime? RegisterDate { get; set; }
    }
}
