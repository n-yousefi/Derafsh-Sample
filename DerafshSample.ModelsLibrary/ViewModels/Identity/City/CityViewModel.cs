using System.ComponentModel.DataAnnotations.Schema;
using Derafsh.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using DerafshSample.ModelsLibrary.Interfaces;
using DerafshSample.ModelsLibrary.ViewModels.Identity.State;


namespace DerafshSample.ModelsLibrary.ViewModels.Identity.City
{
    [Table("City")]
    public class CityViewModel:Models.Identity.City, ISelectable
    {
        [Join("StateId")]
        public StateViewModel State { get; set; }

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
