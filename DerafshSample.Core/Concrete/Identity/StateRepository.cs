using System.Data.SqlClient;
using Derafsh.Business;
using DerafshSample.Core.Abstract;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.State;
using DerafshSample.Services.Abstract;


namespace DerafshSample.Core.Concrete.Identity
{
    public class  StateRepository : Repository<StateViewModel>, IStateRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDatabaseActions _databaseActions;
        private readonly IDatabaseConnectionService _connectionService;
        public StateRepository(IDatabaseConnectionService databaseConnection,
            IDatabaseActions databaseActions,
            IDatabaseConnectionService connectionService) : base(databaseActions,connectionService)
        {
            _sqlConnection = databaseConnection.Create();
            _databaseActions = databaseActions;
            _connectionService = connectionService;
        }

        public StateViewModel GetDefaultInstance(string relatedTableName = "", int foreignKeyValue = 0)
        {
            return relatedTableName.ToLower() == "country"
                ? new StateViewModel()
                {
                    CountryId =  foreignKeyValue
                }
                : new StateViewModel();
        }
    }
}
