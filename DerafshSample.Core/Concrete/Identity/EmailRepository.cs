using System.Data.SqlClient;
using Derafsh.Business;
using DerafshSample.Core.Abstract;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.ViewModels.Identity.Email;
using DerafshSample.Services.Abstract;


namespace DerafshSample.Core.Concrete.Identity
{
    public class EmailRepository:Repository<EmailViewModel>,IEmailRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDatabaseActions _databaseActions;
        private readonly IDatabaseConnectionService _connectionService;
        public EmailRepository(IDatabaseConnectionService databaseConnection,
            IDatabaseActions databaseActions,
            IDatabaseConnectionService connectionService):base(databaseActions,connectionService)
        {
            _sqlConnection = databaseConnection.Create();
            _databaseActions = databaseActions;
            _connectionService = connectionService;
        }

        public EmailViewModel GetDefaultInstance(string relatedTableName = "", int foreignKeyValue = 0)
        {
            return relatedTableName.ToLower() == "identity"
                ? new EmailViewModel()
                {
                    IdentityId = foreignKeyValue
                }
                : new EmailViewModel();
        }
    }
}
