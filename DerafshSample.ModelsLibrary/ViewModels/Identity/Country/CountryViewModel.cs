using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using DerafshSample.ModelsLibrary.Interfaces;
using DerafshSample.ModelsLibrary.Models;
using DerafshSample.ModelsLibrary.ViewModels.Identity.State;

namespace DerafshSample.ModelsLibrary.ViewModels.Identity.Country
{
    [Table("Country")]
    public class CountryViewModel:Models.Identity.Country,ISelectable
    {
        public SelectListItem ConvertToSelectListItem()
        {
            return new SelectListItem()
            {
                Text = Name,
                Value = Id.ToString()
            };
        }
    }
}
