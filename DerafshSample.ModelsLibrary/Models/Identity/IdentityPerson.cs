using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Derafsh.Attributes;
using Microsoft.AspNetCore.Mvc;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.Enumeration;

namespace DerafshSample.ModelsLibrary.Models.Identity
{
    public class IdentityPerson 
    {
        [PrimaryKey]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "First Name", Order = 1)]
        [Abstract]
        public string FirstName { get; set; }

        [Display(Name = "Last Name", Order = 2)]
        [Abstract]
        public string LastName { get; set; }

        [ScaffoldColumn(false)]
        public string Name => FirstName + " " + LastName;

        [Display(Name = "Gender", Order = 3)]
        [UIEnum(typeof(GenderEnum), EnumDisplay.Radio)]        
        public int GenderEnumId { get; set; }


        [Display(Name = "Birth Date", Order = 4)]
        [ScaffoldColumn(false)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Father Name", Order = 5)]
        public string FatherName { get; set; }

        [Display(Name = "National Code", Order = 6)]
        public string NationalCode { get; set; }


        [Display(Name = "Birth City", Order = 7)]
        [UIRemoteSelect("City")]
        public int? BirthCityId { get; set; }

    }
}
