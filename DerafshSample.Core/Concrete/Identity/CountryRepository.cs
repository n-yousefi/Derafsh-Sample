using System.Data.SqlClient;
using Derafsh.Business;
using DerafshSample.Core.Abstract;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Country;
using DerafshSample.Services.Abstract;


namespace DerafshSample.Core.Concrete.Identity
{
    public class CountryRepository : Repository<CountryViewModel>, ICountryRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDatabaseActions _databaseActions;
        private readonly IDatabaseConnectionService _connectionService;
        public CountryRepository(IDatabaseConnectionService databaseConnection,
            IDatabaseActions databaseActions,
            IDatabaseConnectionService connectionService) : base(databaseActions,connectionService)
        {
            _sqlConnection = databaseConnection.Create();
            _databaseActions = databaseActions;
        }

        public CountryViewModel GetDefaultInstance(string relatedTableName = "", int foreignKeyValue = 0)
        {
            return new CountryViewModel();
        }
    }
}
