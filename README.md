# Derafsh Samples

This repository contains the original ASP.NET MVC sample and a separate modern sample area. The original project is preserved.

## Samples

### Original MVC sample
The original sample demonstrates dynamic insert, edit, and index forms for related models. See the existing `DataBaseScripts/GenerateDb.sql` instructions before running it.

### Modern .NET 10 UI
A responsive Razor Pages sample with a modern admin interface, dynamic related collections, and Derafsh graph persistence.

- [Modern sample overview and run instructions](modern/README.md)
- [Modern UI project](modern/DerafshSample.Modern/)
- [Commerce graph / Dapper integration sample](modern/DerafshSample.Commerce/)
- [Commerce integration tests](modern/DerafshSample.Commerce.Tests/)

## Why Derafsh?

Dapper is a great micro-ORM for explicit SQL and query mapping. It does not automatically coordinate a connected graph of entities for inserts, generated key propagation, graph loading, and synchronization. Derafsh adds graph-oriented operations for mapped relationships while leaving Dapper available for reports and specialized SQL.

The commerce sample demonstrates the intended balance: use Derafsh for graph persistence, and Dapper for a concise captured-payment revenue query. It also documents limitations and the explicit association-entity pattern for many-to-many relationships. See [the commerce sample README](modern/DerafshSample.Commerce/README.md) before interpreting the sample as a production recommendation.

## Requirements

- .NET 10 SDK for the modern samples.
- Network access to restore NuGet packages on first build.
- No external database server is required by the commerce integration tests; they use SQLite in-memory.
