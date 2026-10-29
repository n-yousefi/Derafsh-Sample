using Dapper;
using Derafsh;
using DerafshSample.Commerce;
using Microsoft.Data.Sqlite;
using Xunit;
namespace DerafshSample.Commerce.Tests;
public sealed class CommerceGraphTests {
 private static async Task<SqliteConnection> DbAsync() {
  var connection = new SqliteConnection("Data Source=:memory:"); await CommerceDatabase.InitializeAsync(connection); return connection;
 }
 private static Order Sample() => new() {
  OrderNumber="WEB-2026-001", Status="Confirmed", GrandTotal=149.50m,
  Customer=new Customer { Name="Ada Lovelace", Email="ada@example.test", Addresses=[
   new CustomerAddress { Label="Home", City="London", CountryCode="GB", Line1="10 Analytical Engine Way" },
   new CustomerAddress { Label="Office", City="London", CountryCode="GB", Line1="2 Difference Street" }] },
  Lines=[
   new OrderLine { Quantity=2, UnitPrice=50m, Product=new Product { Sku="KB-001", Name="Mechanical keyboard", Category=new ProductCategory { Name="Accessories" }, Tags=[
    new ProductTag { Tag=new Tag { Name="Popular" } }, new ProductTag { Tag=new Tag { Name="Office" } } ] } },
   new OrderLine { Quantity=1, UnitPrice=49.50m, Product=new Product { Sku="MS-001", Name="Wireless mouse", Category=new ProductCategory { Name="Accessories" }, Tags=[new ProductTag { Tag=new Tag { Name="Wireless" } }] } }],
  Payments=[new Payment { Amount=100m, Method="Card", Status="Captured", TransactionReference="PAY-001" }, new Payment { Amount=49.50m, Method="GiftCard", Status="Pending", TransactionReference="PAY-002" }],
  Shipments=[new Shipment { Carrier="ParcelPost", TrackingNumber="TRACK-001", Status="Packed", Items=[new ShipmentItem { OrderLineId=1, Quantity=1 }] }],
  Discounts=[new OrderDiscount { Code="WELCOME10", Amount=10m }]
 };
 [Fact] public async Task Insert_persists_deep_graph_and_propagates_generated_keys() {
  await using var db=await DbAsync(); var order=Sample(); var result=await db.InsertGraphAsync(order);
  Assert.True(order.Id>0); Assert.True(order.Customer!.Id>0); Assert.Equal(order.Customer.Id,order.CustomerId);
  Assert.Equal(2,order.Lines!.Count); Assert.All(order.Lines,x=>Assert.True(x.Id>0 && x.OrderId==order.Id));
  Assert.All(order.Lines,x=>Assert.True(x.Product!.Id>0 && x.Product.Category!.Id>0));
  Assert.All(order.Lines,x=>Assert.All(x.Product!.Tags!,link=>Assert.True(link.Tag!.Id>0)));
  Assert.Equal(2,order.Customer.Addresses!.Count); Assert.Equal(2,order.Payments!.Count); Assert.Single(order.Shipments!);
  Assert.Single(order.Discounts!); Assert.True(result.InsertedCount>=15);
 }
 [Fact] public async Task Load_hydrates_references_and_nested_child_collections() {
  await using var db=await DbAsync(); var order=Sample(); await db.InsertGraphAsync(order);
  var loaded=await db.LoadGraphAsync<Order>(order.Id);
  Assert.NotNull(loaded); Assert.Equal("Ada Lovelace",loaded.Customer!.Name); Assert.Equal(2,loaded.Customer.Addresses!.Count);
  Assert.Equal(2,loaded.Lines!.Count); Assert.Equal("Mechanical keyboard",loaded.Lines[0].Product!.Name);
  Assert.Equal("Accessories",loaded.Lines[0].Product.Category!.Name); Assert.Equal(2,loaded.Lines[0].Product.Tags!.Count);
  Assert.Equal(2,loaded.Payments!.Count); Assert.Single(loaded.Shipments!); Assert.Single(loaded.Shipments[0].Items!);
  Assert.Equal("WELCOME10",Assert.Single(loaded.Discounts!).Code);
 }
 [Fact] public async Task Synchronize_updates_inserts_and_removes_owned_children() {
  await using var db=await DbAsync(); var order=Sample(); await db.InsertGraphAsync(order);
  var loaded=(await db.LoadGraphAsync<Order>(order.Id))!; loaded.Status="PartiallyShipped";
  loaded.Lines!.RemoveAt(1); loaded.Lines[0].Quantity=3; loaded.Payments=[loaded.Payments![0]]; loaded.Discounts=[];
  loaded.Lines.Add(new OrderLine { Quantity=1, UnitPrice=12m, Product=new Product { Sku="CB-001", Name="USB-C cable", Category=loaded.Lines[0].Product!.Category } });
  var result=await db.SynchronizeGraphAsync(loaded); var again=(await db.LoadGraphAsync<Order>(order.Id))!;
  Assert.Equal("PartiallyShipped",again.Status); Assert.Equal(2,again.Lines!.Count);
  Assert.Equal(3m,again.Lines.Single(x=>x.Product!.Sku=="KB-001").Quantity); Assert.Contains(again.Lines,x=>x.Product!.Sku=="CB-001");
  Assert.Single(again.Payments!); Assert.Empty(again.Discounts!); Assert.True(result.DeletedCount>=2); Assert.True(result.InsertedCount>=1);
 }
 [Fact] public async Task Null_collection_is_not_used_as_a_partial_update_contract() {
  await using var db=await DbAsync(); var order=Sample(); await db.InsertGraphAsync(order);
  var loaded=(await db.LoadGraphAsync<Order>(order.Id))!;
  Assert.Equal(2,loaded.Lines!.Count);
  // Synchronization is intended for a complete, loaded graph. Do not pass a sparse root
  // with unrelated references omitted and assume it is a safe PATCH operation.
  loaded.Status="Reviewed";
  await db.SynchronizeGraphAsync(loaded);
  var again=(await db.LoadGraphAsync<Order>(order.Id))!;
  Assert.Equal("Reviewed",again.Status);
  Assert.Equal(2,again.Lines!.Count);
 }
 [Fact] public async Task Caller_transaction_rolls_back_all_graph_rows() {
  await using var db=await DbAsync(); await using var tx=await db.BeginTransactionAsync();
  await db.InsertGraphAsync(Sample(),tx); await tx.RollbackAsync();
  Assert.Equal(0,await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM CommerceOrder"));
  Assert.Equal(0,await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM CommerceCustomer"));
  Assert.Equal(0,await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM CommerceProduct"));
 }
 [Fact] public async Task Dapper_reporting_query_works_alongside_graph_persistence() {
  await using var db=await DbAsync(); await db.InsertGraphAsync(Sample());
  var revenue=await db.ExecuteScalarAsync<decimal>("SELECT COALESCE(SUM(Amount),0) FROM CommercePayment WHERE Status='Captured'");
  Assert.Equal(100m,revenue);
 }
 [Fact] public async Task Many_to_many_is_explicit_through_association_entity() {
  await using var db=await DbAsync(); var order=Sample(); await db.InsertGraphAsync(order);
  var count=await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM CommerceProductTag pt JOIN CommerceProduct p ON p.Id=pt.ProductId WHERE p.Sku='KB-001'");
  Assert.Equal(2,count);
 }
}