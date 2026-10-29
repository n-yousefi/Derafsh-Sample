# Derafsh Commerce Stress Sample

A relationship-heavy .NET 10 sample demonstrating where Derafsh simplifies relational graph persistence and where Dapper remains useful for reporting.

## Scenario

The model includes customers and addresses, orders and lines, products and categories, product tags, payments, shipments and shipment items, and order discounts.

    Order
    ├── Customer [Reference]
    │   └── Addresses[]
    ├── Lines[]
    │   └── Product [Reference]
    │       ├── Category [Reference]
    │       └── Tags[] (ProductTag association entity)
    │           └── Tag [Reference]
    ├── Payments[]
    ├── Shipments[]
    │   └── Items[]
    └── Discounts[]

Product-to-tag many-to-many is represented by an explicit association entity, not claimed as a built-in automatic many-to-many feature.

## Run integration tests

Requires the .NET 10 SDK.

    dotnet test modern/DerafshSample.Commerce.Tests/DerafshSample.Commerce.Tests.csproj

The tests use SQLite in-memory databases and the real Derafsh package. They cover deep graph insert/load, generated-key propagation, synchronization, null-versus-empty collection semantics, transaction rollback, a Dapper report, and the association-entity pattern.
