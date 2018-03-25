using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Phone;
using DerafshSample.Models;

namespace DerafshSample.Controllers.Identity
{
    public class PhoneController : BaseController<PhoneViewModel>
    {
        private readonly IPhoneRepository _phoneRepository;

        private static readonly BaseModel BaseModel = new BaseModel()
        {
            Name = "Phone",            
            PluralName = "Phones",
        };

        public PhoneController(IPhoneRepository phoneRepository)
            :base(phoneRepository, BaseModel)
        {
            _phoneRepository = phoneRepository;
        }
    }
}