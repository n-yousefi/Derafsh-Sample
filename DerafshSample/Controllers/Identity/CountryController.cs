using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Country;
using DerafshSample.Models;

namespace DerafshSample.Controllers.Identity
{
    public class CountryController : BaseController<CountryViewModel>
    {
        private readonly ICountryRepository _countryRepository;

        private static readonly BaseModel BaseModel = new BaseModel()
        {
            Name = "Country",
            PluralName = "Countries",
        };

        public CountryController(ICountryRepository countryRepository)
            : base(countryRepository, BaseModel)
        {
            _countryRepository = countryRepository;
        }        
    }
}