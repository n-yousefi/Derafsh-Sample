using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Email;
using DerafshSample.Models;

namespace DerafshSample.Controllers.Identity
{
    public class EmailController : BaseController<EmailViewModel>
    {
        private readonly IEmailRepository _emailRepository;

        private static readonly BaseModel BaseModel = new BaseModel()
        {
            Name = "Email",            
            PluralName = "Emails",
        };

        public EmailController(IEmailRepository addressRepository)
            : base(addressRepository, BaseModel)
        {
            _emailRepository = addressRepository;
        }
    }
}