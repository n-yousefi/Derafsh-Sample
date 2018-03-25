using System.ComponentModel.DataAnnotations.Schema;
using Derafsh.Attributes;
using DerafshSample.ModelsLibrary.ViewModels.Identity.City;

namespace DerafshSample.ModelsLibrary.ViewModels.Identity.Address
{
    [Table("Address")]
    public class AddressViewModel:Models.Identity.Address
    {
        [Join("CityId")]
        public CityViewModel City { get; set; }
    }
}
