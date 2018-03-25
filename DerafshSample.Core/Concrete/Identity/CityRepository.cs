using System.Data.SqlClient;
using Derafsh.Business;
using DerafshSample.Core.Abstract;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.City;
using DerafshSample.Services.Abstract;


namespace DerafshSample.Core.Concrete.Identity
{
    public class CityRepository:Repository<CityViewModel>,ICityRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDatabaseActions _databaseActions;
        private readonly IDatabaseConnectionService _connectionService;
        public CityRepository(IDatabaseConnectionService databaseConnection,
            IDatabaseActions databaseActions,
            IDatabaseConnectionService connectionService):base(databaseActions,connectionService)
        {
            _sqlConnection = databaseConnection.Create();
            _databaseActions = databaseActions;
            _connectionService = connectionService;
        }

        public CityViewModel GetDefaultInstance(string relatedTableName = "", int foreignKeyValue = 0)
        {
            return relatedTableName.ToLower() == "state"
                ? new CityViewModel()
                {
                    StateId = foreignKeyValue
                }
                : new CityViewModel();
        }
    }
}
