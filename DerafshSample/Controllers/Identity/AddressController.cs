using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Address;
using DerafshSample.Models;

namespace DerafshSample.Controllers.Identity
{
    public class AddressController : BaseController<AddressViewModel>
    {
        private readonly IAddressRepository _addressRepository;

        private static readonly BaseModel BaseModel = new BaseModel()
        {
            Name = "Address",
            PluralName = "Addresses",
        };

        public AddressController(IAddressRepository addressRepository)
            : base(addressRepository, BaseModel)
        {
            _addressRepository = addressRepository;
        }
    }
}