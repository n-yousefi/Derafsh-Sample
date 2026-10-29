# Derafsh Commerce Stress Sample

A deliberately relationship-heavy .NET 10 sample showing why a graph-persistence helper can be useful alongside Dapper. It uses the published Derafsh package and SQLite so the sample and integration tests can run without a separate database server.

## Why use Derafsh when Dapper is already simple?

Dapper is excellent at executing SQL and mapping query results. It intentionally does not try to manage a whole object graph for you. When an aggregate contains a customer, addresses, order lines, products, categories, tags, payments, shipments, shipment items, and discounts, application code otherwise has to coordinate insert order, generated keys, foreign keys, repeated SQL, update behavior, and deletes across many commands.

Derafsh complements rather than replaces Dapper. It provides graph-oriented insert, load, and synchronization operations for mapped relationships, while Dapper remains available for custom joins, reports, bulk-oriented SQL, and queries whose shape is better expressed directly in SQL.

Use it when:
- Your model contains related objects and child collections that are routinely saved or loaded together.
- You want generated parent keys propagated to related rows without manually wiring every foreign key.
- You want a consistent graph insert/load/synchronize API and can describe relationships through the library's mapping conventions/configuration.
- You want to keep SQL explicit for reporting instead of forcing every query through an ORM abstraction.

Keep plain Dapper (or explicit SQL) when:
- A report is easier to express as a hand-written join, aggregate, window function, or projection.
- You need specialized bulk operations, stored procedures, vendor-specific SQL, or precise query-plan control.
- You need behavior that the library does not support or cannot express clearly; use a focused SQL/Dapper implementation for that part rather than forcing the entire application into one approach.

Derafsh is not a replacement for schema migrations, database constraints, domain validation, authorization, or transaction design. Review its supported mappings and the behavior of the exact package version before adopting it for a production aggregate.

## Scenario and relationship graph

```text
Order
├── Customer [reference]
│   └── Addresses[] [one-to-many]
├── Lines[] [one-to-many]
│   └── Product [reference]
│       ├── Category [reference]
│       └── Tags[] -> ProductTag [association entity]
│           └── Tag [reference]
├── Payments[] [one-to-many]
├── Shipments[] [one-to-many]
│   └── Items[] [one-to-many]
└── Discounts[] [one-to-many]
```

The model covers customers and multiple addresses, orders and lines, products and categories, payments with different states, shipments and shipment items, discounts, and product tags. Product-to-tag is represented explicitly with a `ProductTag` association entity. This sample does **not** claim automatic many-to-many handling: it demonstrates the common relational pattern of mapping the join table as an entity.

## Small persistence API

See `CommerceOrderStore.cs` for the intended division:
- `InsertAsync` calls `InsertGraphAsync` inside a transaction.
- `LoadAsync` calls `LoadGraphAsync<Order>`.
- `SynchronizeAsync` calls `SynchronizeGraphAsync` inside a transaction.
- `GetCapturedRevenueAsync` uses ordinary Dapper SQL for a report.

Transactions are deliberately explicit in the store. The sample does not imply that every graph write should be performed without transaction boundaries.

## Integration tests

Requires the .NET 10 SDK. From the repository root:

```bash
dotnet restore modern/DerafshSample.Commerce.Tests/DerafshSample.Commerce.Tests.csproj
dotnet test modern/DerafshSample.Commerce.Tests/DerafshSample.Commerce.Tests.csproj --configuration Release
```

Tests use a fresh SQLite in-memory database and the real Derafsh package. They exercise:
1. Inserting a deep graph and checking generated keys / foreign-key propagation.
2. Loading references and nested collections.
3. Synchronizing changed, added, and removed owned children.
4. Synchronizing a fully loaded graph without accidentally treating the operation as a sparse PATCH.
5. Rolling back a graph write using a caller-owned transaction.
6. Running a Dapper reporting query alongside graph persistence.
7. Persisting the many-to-many relationship through an explicit association entity.

The tests are integration tests for this sample and its package version, not a claim that every possible graph shape or database provider is covered.

## Important synchronization note

Treat `SynchronizeGraphAsync` as a graph synchronization operation, not a generic HTTP PATCH operation. Load the graph (or construct a complete graph according to the documented contract) before synchronizing it. Do not send a sparse root object with unrelated navigations set to null and assume every omitted relationship is automatically preserved. Add application-level partial-update semantics explicitly where needed.

## Run the modern UI sample

The separate Razor Pages UI is documented in [modern/README.md](../README.md):

```bash
dotnet run --project modern/DerafshSample.Modern/DerafshSample.Modern.csproj
```
