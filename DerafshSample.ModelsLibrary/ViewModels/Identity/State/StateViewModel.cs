using System.ComponentModel.DataAnnotations.Schema;
using Derafsh.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using DerafshSample.ModelsLibrary.Interfaces;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Country;

namespace DerafshSample.ModelsLibrary.ViewModels.Identity.State
{
    [Table("State")]
    public class StateViewModel:Models.Identity.State,ISelectable
    {
        [Join("CountryId")]
        public CountryViewModel Country { get; set; }

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
