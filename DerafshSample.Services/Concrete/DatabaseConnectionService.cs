using DerafshSample.Services.Abstract;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DerafshSample.Services.Concrete
{
    public class DatabaseConnectionService : IDatabaseConnectionService
    {
        private SqlConnection _sqlConnection;
        private readonly string _connectionString;

        public DatabaseConnectionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Create()
        {
            _sqlConnection = new SqlConnection(_connectionString);            
            _sqlConnection.Open();

            return _sqlConnection;
        }

        public void Dispose()
        {
            if (_sqlConnection == null)
            {
                return;
            }

            _sqlConnection.Dispose();
            _sqlConnection = null;
        }
    }
}