using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Derafsh;
namespace DerafshSample.Commerce;
[Table("CommerceOrder")]
public sealed class Order {
 [Key] public int Id {get;set;}
 [Required, StringLength(40)] public string OrderNumber {get;set;} = "";
 public int CustomerId {get;set;}
 [Reference(nameof(CustomerId))] public Customer? Customer {get;set;}
 public string Status {get;set;} = "Draft";
 public decimal GrandTotal {get;set;}
 [ChildCollection(nameof(OrderLine.OrderId))] public List<OrderLine>? Lines {get;set;} = [];
 [ChildCollection(nameof(Payment.OrderId))] public List<Payment>? Payments {get;set;} = [];
 [ChildCollection(nameof(Shipment.OrderId))] public List<Shipment>? Shipments {get;set;} = [];
 [ChildCollection(nameof(OrderDiscount.OrderId))] public List<OrderDiscount>? Discounts {get;set;} = [];
}
[Table("CommerceCustomer")]
public sealed class Customer {
 [Key] public int Id {get;set;}
 public string Name {get;set;} = "";
 public string Email {get;set;} = "";
 [ChildCollection(nameof(CustomerAddress.CustomerId))] public List<CustomerAddress>? Addresses {get;set;} = [];
}
[Table("CommerceCustomerAddress")]
public sealed class CustomerAddress {
 [Key] public int Id {get;set;} public int CustomerId {get;set;}
 public string Label {get;set;} = ""; public string City {get;set;} = "";
 public string CountryCode {get;set;} = ""; public string Line1 {get;set;} = "";
}
[Table("CommerceOrderLine")]
public sealed class OrderLine {
 [Key] public int Id {get;set;} public int OrderId {get;set;}
 public int ProductId {get;set;}
 [Reference(nameof(ProductId))] public Product? Product {get;set;}
 public decimal Quantity {get;set;} public decimal UnitPrice {get;set;}
}
[Table("CommerceProduct")]
public sealed class Product {
 [Key] public int Id {get;set;} public string Sku {get;set;} = ""; public string Name {get;set;} = "";
 public int CategoryId {get;set;}
 [Reference(nameof(CategoryId))] public ProductCategory? Category {get;set;}
 [ChildCollection(nameof(ProductTag.ProductId))] public List<ProductTag>? Tags {get;set;} = [];
}
[Table("CommerceProductCategory")]
public sealed class ProductCategory { [Key] public int Id {get;set;} public string Name {get;set;} = ""; }
[Table("CommerceTag")]
public sealed class Tag { [Key] public int Id {get;set;} public string Name {get;set;} = ""; }
// Many-to-many is modeled explicitly by the association entity.
[Table("CommerceProductTag")]
public sealed class ProductTag {
 [Key] public int Id {get;set;} public int ProductId {get;set;} public int TagId {get;set;}
 [Reference(nameof(TagId))] public Tag? Tag {get;set;}
}
[Table("CommercePayment")]
public sealed class Payment {
 [Key] public int Id {get;set;} public int OrderId {get;set;} public decimal Amount {get;set;}
 public string Method {get;set;} = ""; public string Status {get;set;} = "Pending";
 public string TransactionReference {get;set;} = "";
}
[Table("CommerceShipment")]
public sealed class Shipment {
 [Key] public int Id {get;set;} public int OrderId {get;set;} public string Carrier {get;set;} = "";
 public string TrackingNumber {get;set;} = ""; public string Status {get;set;} = "Created";
 [ChildCollection(nameof(ShipmentItem.ShipmentId))] public List<ShipmentItem>? Items {get;set;} = [];
}
[Table("CommerceShipmentItem")]
public sealed class ShipmentItem {
 [Key] public int Id {get;set;} public int ShipmentId {get;set;} public int OrderLineId {get;set;} public decimal Quantity {get;set;}
}
[Table("CommerceOrderDiscount")]
public sealed class OrderDiscount {
 [Key] public int Id {get;set;} public int OrderId {get;set;} public string Code {get;set;} = ""; public decimal Amount {get;set;}
}