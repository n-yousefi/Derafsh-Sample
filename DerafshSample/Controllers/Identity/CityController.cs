using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.City;
using DerafshSample.Models;

namespace DerafshSample.Controllers.Identity
{
    public class CityController : BaseController<CityViewModel>
    {
        private readonly ICityRepository _cityRepository;

        private static readonly BaseModel BaseModel = new BaseModel()
        {
            Name = "City",
            PluralName = "Cities",
        };

        public CityController(ICityRepository cityRepository)
            : base(cityRepository, BaseModel)
        {
            _cityRepository = cityRepository;
        }        
    }
}