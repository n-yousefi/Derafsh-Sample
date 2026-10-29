using Dapper;
using Derafsh;
using Microsoft.Data.Sqlite;
namespace DerafshSample.Commerce;
/// <summary>Derafsh persists graphs; Dapper remains available for specialized reports.</summary>
public sealed class CommerceOrderStore(string connectionString) {
 public async Task<Order?> LoadAsync(int id, CancellationToken ct = default) {
  await using var connection = new SqliteConnection(connectionString); await connection.OpenAsync(ct);
  return await connection.LoadGraphAsync<Order>(id, ct);
 }
 public async Task<GraphWriteResult> InsertAsync(Order order, CancellationToken ct = default) {
  await using var connection = new SqliteConnection(connectionString); await connection.OpenAsync(ct);
  await using var tx = await connection.BeginTransactionAsync(ct);
  try { var result = await connection.InsertGraphAsync(order, tx, ct); await tx.CommitAsync(ct); return result; }
  catch { await tx.RollbackAsync(ct); throw; }
 }
 public async Task<GraphWriteResult> SynchronizeAsync(Order order, CancellationToken ct = default) {
  await using var connection = new SqliteConnection(connectionString); await connection.OpenAsync(ct);
  await using var tx = await connection.BeginTransactionAsync(ct);
  try { var result = await connection.SynchronizeGraphAsync(order, tx, ct); await tx.CommitAsync(ct); return result; }
  catch { await tx.RollbackAsync(ct); throw; }
 }
 public async Task<decimal> GetCapturedRevenueAsync(CancellationToken ct = default) {
  await using var connection = new SqliteConnection(connectionString);
  return await connection.ExecuteScalarAsync<decimal>(new CommandDefinition(
   "SELECT COALESCE(SUM(Amount), 0) FROM CommercePayment WHERE Status = 'Captured';", cancellationToken: ct));
 }
}