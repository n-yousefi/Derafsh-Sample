using System.Data.SqlClient;
using Derafsh.Business;
using DerafshSample.Core.Abstract;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Phone;
using DerafshSample.Services.Abstract;

namespace DerafshSample.Core.Concrete.Identity
{
    public class PhoneRepository : Repository<PhoneViewModel>,IPhoneRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDatabaseActions _databaseActions;
        private readonly IDatabaseConnectionService _connectionService;
        public PhoneRepository(IDatabaseConnectionService databaseConnection,
            IDatabaseActions databaseActions,
            IDatabaseConnectionService connectionService):base(databaseActions,connectionService)
        {
            _sqlConnection = databaseConnection.Create();
            _databaseActions = databaseActions;
            _connectionService = connectionService;
        }

        public PhoneViewModel GetDefaultInstance(string relatedTableName = "", int foreignKeyValue = 0)
        {
            return relatedTableName.ToLower() == "identity"
                ? new PhoneViewModel()
                {
                    IdentityId = foreignKeyValue
                }
                : new PhoneViewModel();
        }
    }
}
