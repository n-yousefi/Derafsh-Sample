using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DerafshSample.Services.Abstract
{
    public interface IDatabaseConnectionService : IDisposable
    {
        SqlConnection Create();
    }
}