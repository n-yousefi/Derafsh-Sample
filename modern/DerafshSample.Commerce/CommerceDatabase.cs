using Microsoft.Data.Sqlite;
namespace DerafshSample.Commerce;
public static class CommerceDatabase {
 public static async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default) {
  await connection.OpenAsync(cancellationToken);
  const string sql = """
  PRAGMA foreign_keys = ON;
  CREATE TABLE IF NOT EXISTS CommerceProductCategory (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceTag (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceProduct (Id INTEGER PRIMARY KEY AUTOINCREMENT, Sku TEXT NOT NULL UNIQUE, Name TEXT NOT NULL, CategoryId INTEGER NOT NULL REFERENCES CommerceProductCategory(Id));
  CREATE TABLE IF NOT EXISTS CommerceProductTag (Id INTEGER PRIMARY KEY AUTOINCREMENT, ProductId INTEGER NOT NULL REFERENCES CommerceProduct(Id) ON DELETE CASCADE, TagId INTEGER NOT NULL REFERENCES CommerceTag(Id));
  CREATE UNIQUE INDEX IF NOT EXISTS UX_CommerceProductTag_Product_Tag ON CommerceProductTag(ProductId, TagId);
  CREATE TABLE IF NOT EXISTS CommerceCustomer (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Email TEXT NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceCustomerAddress (Id INTEGER PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER NOT NULL REFERENCES CommerceCustomer(Id) ON DELETE CASCADE, Label TEXT NOT NULL, City TEXT NOT NULL, CountryCode TEXT NOT NULL, Line1 TEXT NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceOrder (Id INTEGER PRIMARY KEY AUTOINCREMENT, OrderNumber TEXT NOT NULL UNIQUE, CustomerId INTEGER NOT NULL REFERENCES CommerceCustomer(Id), Status TEXT NOT NULL, GrandTotal NUMERIC NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceOrderLine (Id INTEGER PRIMARY KEY AUTOINCREMENT, OrderId INTEGER NOT NULL REFERENCES CommerceOrder(Id) ON DELETE CASCADE, ProductId INTEGER NOT NULL REFERENCES CommerceProduct(Id), Quantity NUMERIC NOT NULL, UnitPrice NUMERIC NOT NULL);
  CREATE TABLE IF NOT EXISTS CommercePayment (Id INTEGER PRIMARY KEY AUTOINCREMENT, OrderId INTEGER NOT NULL REFERENCES CommerceOrder(Id) ON DELETE CASCADE, Amount NUMERIC NOT NULL, Method TEXT NOT NULL, Status TEXT NOT NULL, TransactionReference TEXT NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceShipment (Id INTEGER PRIMARY KEY AUTOINCREMENT, OrderId INTEGER NOT NULL REFERENCES CommerceOrder(Id) ON DELETE CASCADE, Carrier TEXT NOT NULL, TrackingNumber TEXT NOT NULL, Status TEXT NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceShipmentItem (Id INTEGER PRIMARY KEY AUTOINCREMENT, ShipmentId INTEGER NOT NULL REFERENCES CommerceShipment(Id) ON DELETE CASCADE, OrderLineId INTEGER NOT NULL REFERENCES CommerceOrderLine(Id), Quantity NUMERIC NOT NULL);
  CREATE TABLE IF NOT EXISTS CommerceOrderDiscount (Id INTEGER PRIMARY KEY AUTOINCREMENT, OrderId INTEGER NOT NULL REFERENCES CommerceOrder(Id) ON DELETE CASCADE, Code TEXT NOT NULL, Amount NUMERIC NOT NULL);
  CREATE INDEX IF NOT EXISTS IX_CommerceOrder_CustomerId ON CommerceOrder(CustomerId);
  CREATE INDEX IF NOT EXISTS IX_CommerceOrderLine_OrderId ON CommerceOrderLine(OrderId);
  CREATE INDEX IF NOT EXISTS IX_CommercePayment_OrderId ON CommercePayment(OrderId);
  CREATE INDEX IF NOT EXISTS IX_CommerceShipment_OrderId ON CommerceShipment(OrderId);
  CREATE INDEX IF NOT EXISTS IX_CommerceCustomerAddress_CustomerId ON CommerceCustomerAddress(CustomerId);
  """;
  await using var command = connection.CreateCommand(); command.CommandText = sql;
  await command.ExecuteNonQueryAsync(cancellationToken);
 }
}