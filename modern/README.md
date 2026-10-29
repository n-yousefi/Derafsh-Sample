# Derafsh Modern Sample

A modernized companion to the original MVC sample. It keeps the original project intact and adds a separate **.NET 10 + Razor Pages** sample demonstrating a current UI with Derafsh 2.x graph persistence.

## What it demonstrates

- A responsive dark admin interface with a 2026-inspired visual system.
- A single form for a root entity and three related child collections.
- Dynamic add/remove rows for contacts, shipping zones, and notification recipients.
- Labels and basic validation informed by .NET data annotations.
- A graph overview that updates as child rows are added or removed.
- SQLite initialization with foreign keys and indexes.
- Loading and synchronizing the complete graph using `LoadGraphAsync` and `SynchronizeGraphAsync`.

The form is metadata-informed, while nested collection rows are rendered against the graph model and support dynamic client-side editing. This is a sample application, not a general-purpose UI generator package.

## Run

Requires the .NET 10 SDK.

```bash
dotnet restore modern/DerafshSample.Modern/DerafshSample.Modern.csproj
dotnet run --project modern/DerafshSample.Modern/DerafshSample.Modern.csproj
```

Open the local URL printed by `dotnet run`. The SQLite database is created and seeded automatically on first start.

## Architecture

```text
Razor Pages form
  └── StoreSettingsDto
       ├── Contacts[]
       ├── ShippingZones[]
       └── NotificationRecipients[]
                |
                v
        StoreSettingsStore
                |
                v
    Derafsh 2.x graph synchronization
                |
                v
              SQLite
```

The sample references the published `Derafsh` 2.0.0 package so it can be built independently of a local checkout of the library. If that package version is not available in the configured NuGet source, publish/restore it first or temporarily switch the reference to a local project reference during development.


## Relationship-heavy commerce sample

The separate commerce project is intended to stress graph persistence beyond the settings form. It models orders, customers, addresses, order lines, products, categories, tags, payments, shipments, shipment items, and discounts.

- [Commerce sample README: trade-offs, graph shape, test coverage, and run commands](DerafshSample.Commerce/README.md)
- [Commerce integration tests](DerafshSample.Commerce.Tests/)

Run the commerce integration tests from the repository root:

```bash
dotnet test modern/DerafshSample.Commerce.Tests/DerafshSample.Commerce.Tests.csproj --configuration Release
```

The README distinguishes Derafsh's graph-persistence role from Dapper's hand-written SQL strengths and explicitly documents that the many-to-many example uses an association entity.
