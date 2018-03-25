using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.State;
using DerafshSample.Models;

namespace DerafshSample.Controllers.Identity
{
    public class StateController : BaseController<StateViewModel>
    {
        private readonly IStateRepository _stateRepository;

        private static readonly BaseModel BaseModel = new BaseModel()
        {
            Name = "State",            
            PluralName = "States",
        };

        public StateController(IStateRepository stateRepository)
            : base(stateRepository, BaseModel)
        {
            _stateRepository = stateRepository;
        }        
    }
}