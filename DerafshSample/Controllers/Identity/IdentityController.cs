using System.Collections.Generic;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity;
using DerafshSample.Models;

namespace DerafshSample.Controllers.Identity
{
    public class IdentityController : BaseController<IdentityViewModel>
    {
        private readonly IIdentityRepository _identityRepository;

        private static readonly BaseModel BaseModel = new BaseModel()
        {
            Name = "Identity",
            PluralName = "Identities",
            HierarchyLoading = true,
            HasScript = true,
            DisableProperties = new List<string>() { "IdentityId" }
        };

        public IdentityController(IIdentityRepository identityRepository)
            : base(identityRepository, BaseModel)
        {
            _identityRepository = identityRepository;
        }
    }
}