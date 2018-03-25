using System.Data.SqlClient;
using Derafsh.Business;
using DerafshSample.Core.Abstract;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Address;
using DerafshSample.Services.Abstract;

namespace DerafshSample.Core.Concrete.Identity
{
    public class AddressRepository : Repository<AddressViewModel>,IAddressRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDatabaseActions _databaseActions;
        private readonly IDatabaseConnectionService _connectionService;
        public AddressRepository(IDatabaseConnectionService databaseConnection,
            IDatabaseActions databaseActions,
            IDatabaseConnectionService connectionService):base(databaseActions,
            connectionService)
        {
            _sqlConnection = databaseConnection.Create();
            _databaseActions = databaseActions;
            _connectionService = connectionService;
        }

        public AddressViewModel GetDefaultInstance(string relatedTableName = "", int foreignKeyValue = 0)
        {
            return relatedTableName.ToLower() == "identity"
                ? new AddressViewModel()
                {
                    IdentityId = foreignKeyValue
                }
                : new AddressViewModel();
        }
    }
}
