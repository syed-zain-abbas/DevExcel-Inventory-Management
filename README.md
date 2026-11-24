# Inventory Management System

An inventory management application developed as part of the DevExcel IT Solutions technical assessment using .NET 10 and ASP.NET Core MVC.

## Technology Stack

- .NET 10 / ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views and Bootstrap
- Swagger
- Clean Architecture

## Features

- Product creation, editing, and soft deactivation.
- Unique SKU validation.
- Stock In and Stock Out operations.
- Automatic stock calculation and low-stock indicators.
- Stock movement history.
- Insufficient-stock validation.
- Transaction-based concurrency handling.
- JSON API for product stock information.

## Project Structure

- **Inventory.Domain:** Entities and enumerations.
- **Inventory.Application:** Business logic, DTOs, and interfaces.
- **Inventory.Infrastructure:** Database access, repositories, migrations, and transactions.
- **Inventory.Web:** MVC controllers, API controllers, and Razor Views.

## How to Run

### Prerequisites

- .NET 10 SDK
- SQL Server LocalDB or SQL Server Express

### Setup

1. Clone the repository.
2. Configure the database connection string in `Inventory.Web/appsettings.json` or through User Secrets.
3. Execute the following commands from the repository root.

Restore dependencies:

```bash
dotnet restore
```

Install EF Core CLI if not already installed:

```bash
dotnet tool install --global dotnet-ef --version "10.*"
```

Apply database migrations:

```bash
dotnet ef database update --project Inventory.Infrastructure --startup-project Inventory.Web
```

Run the application:

```bash
dotnet run --project Inventory.Web
```

Initial sample data is automatically seeded during database setup if the Products table is empty.

## API Endpoint

```http
GET /api/products/{id}/stock
```

Swagger UI is available at `/swagger` in the Development environment.

## Assumptions and Design Decisions

- Stock is calculated from movement history rather than stored directly.
- Stock movement quantities must be positive integers.
- Products are soft-deactivated instead of permanently deleted.
- Inactive products cannot receive new stock movements.
- SQL Server pessimistic locking is used to protect against concurrent stock operations producing negative stock.
- All audit timestamps are stored in UTC.
- Authentication is outside the current scope.

## Git Commit History

The application was developed incrementally with meaningful commits throughout the development process.

| Commit | Description |
|---|---|
| `cc774c0` | Addded the API controller |
| `d287d0b` | Add initial inventory seed data |
| `ef37b08` | Implement product details and stock movement history view |
| `332858a` | Implement product create and edit views with reusable form |
| `50879d9` | Implement product listing view with search and stock status |
| `55aef6f` | Implement MVC product controller and form validation |
| `e4163ad` | Implement product repository and duplicate SKU handling |
| `d691a9c` | Configure EF Core entities, relationships and constraints |
| `e8032b8` | Implement stock out with availability validation |
| `e6357c8` | Implement product service and business validation |
| `3bf2ded` | Add repository and stock transaction interfaces |
| `4cfa9c9` | Add application DTOs for products and stock movements |
| `e25a000` | Add domain entities and stock movement enum |
| `52bc68b` | Initial commit |

The complete Git history can be viewed using:

```bash
git log --oneline --graph --all
```