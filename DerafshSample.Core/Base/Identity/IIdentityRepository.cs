using DerafshSample.Core.Abstract;
using DerafshSample.ModelsLibrary.ViewModels.Identity;

namespace DerafshSample.Core.Base.Identity
{
    public interface IIdentityRepository:
        IRepository<IdentityViewModel>
    {
    }
}
