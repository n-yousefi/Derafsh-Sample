using System.Collections.Generic;
using System.Data.SqlClient;
using Derafsh.Business;
using DerafshSample.Core.Abstract;
using DerafshSample.Core.Base.Identity;
using DerafshSample.ModelsLibrary.Enumeration;
using DerafshSample.ModelsLibrary.ViewModels.Identity;
using DerafshSample.Services.Abstract;

namespace DerafshSample.Core.Concrete.Identity
{
    public class IdentityRepository:Repository<IdentityViewModel>,IIdentityRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDatabaseConnectionService _connectionService;
        public IdentityRepository(IDatabaseConnectionService databaseConnection,
            IDatabaseActions databaseActions,
            IDatabaseConnectionService connectionService):base(databaseActions,connectionService)
        {
            _sqlConnection = databaseConnection.Create();
            _connectionService = connectionService;
        }

        public IdentityViewModel GetDefaultInstance(
            string relatedTableName = "", int foreignKeyValue = 0)
        {
            return new IdentityViewModel()
            {
                IdentityEnumId = (int)IdentityEnum.Person
            };
        }
    }
}
