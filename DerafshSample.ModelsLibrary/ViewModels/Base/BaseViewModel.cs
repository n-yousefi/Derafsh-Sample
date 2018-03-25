using System;
using System.ComponentModel.DataAnnotations;
using Derafsh.Attributes;
using Microsoft.AspNetCore.Mvc;
using DerafshSample.ModelsLibrary.Attributes;
using DerafshSample.ModelsLibrary.Interfaces;

namespace DerafshSample.ModelsLibrary.ViewModels.Base
{
    public class BaseViewModel
    {
        [PrimaryKey]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Abstract]  
        [Display(Name= "Date", Order = 1000)]         
        [ScaffoldColumn(false)]
        public DateTime Date { get; set; } = DateTime.Now;


        [Display(Name = "Is Active", Order = 1002)]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Is Deleted", Order = 1003)]        
        public bool IsDeleted { get; set; } = false;
    }
}
