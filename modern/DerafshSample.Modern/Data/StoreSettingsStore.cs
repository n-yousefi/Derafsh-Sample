using Derafsh;
using Microsoft.Data.Sqlite;

namespace DerafshSample.Modern.Data;

public sealed class StoreSettingsStore(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetConnectionString("DerafshSample")
        ?? throw new InvalidOperationException("Connection string 'DerafshSample' is not configured.");

    public async Task<StoreSettingsDto> LoadAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = new SqliteConnection(_connectionString);
        return await connection.LoadGraphAsync<StoreSettingsDto>(1, cancellationToken)
            ?? throw new InvalidOperationException("Demo settings row was not found.");
    }

    public async Task<GraphWriteResult> SaveAsync(
        StoreSettingsDto settings,
        CancellationToken cancellationToken = default)
    {
        await using var connection = new SqliteConnection(_connectionString);
        return await connection.SynchronizeGraphAsync(settings, cancellationToken);
    }
}
