# StockPilot

**Inventory Management REST API | In Development**

StockPilot is a backend inventory management project built to help businesses manage product records and track stock coming into inventory. It is also a practical project for developing my skills in ASP.NET Core Web API, database design, and RESTful backend development.

> **Project status:** In development. The features below describe the current scope as discussed; check the source code for the latest implementation.

## Why I built it

Inventory information can become difficult to maintain when products and stock movements are tracked manually or across disconnected records. StockPilot explores a structured, API-based approach to keeping product and inventory data in one place.

## Current functionality

- **Product management:** Create, retrieve, update, and delete product records through REST endpoints.
- **Stock-in:** Record incoming stock and update inventory quantities.
- **Database persistence:** Store and retrieve data using Entity Framework Core and SQL Server.

## Planned improvements

- Stock-out operations and stock movement history.
- Stronger request validation and consistent API error responses.
- Automated tests for business rules and API endpoints.
- Additional documentation and deployment configuration.

*Planned features are not yet presented as implemented.*

## Tech stack

| Layer | Technology |
| --- | --- |
| Language | C# |
| API framework | ASP.NET Core Web API (.NET 10) |
| Data access | Entity Framework Core |
| Database | Microsoft SQL Server / SQL Server LocalDB |
| Version control | Git and GitHub |

## How it works

1. A client sends an HTTP request to an API endpoint.
2. The API processes the request and applies the relevant application rules.
3. Entity Framework Core reads or writes the corresponding data in SQL Server.
4. The API returns an HTTP response to the client.

The exact routes, request bodies, and response examples should be taken from the current controllers or OpenAPI documentation. They are intentionally not invented here.

## Getting started

**Prerequisites:**

- .NET 10 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio or another .NET-compatible editor

1. Clone the repository:

   ```bash
   git clone https://github.com/Lesebo-Tech/StockPilot.git
   cd StockPilot
   ```

2. Open the solution in Visual Studio or locate the Web API project folder.
3. Configure the database connection string for your local SQL Server instance using your local development configuration or user secrets. **Do not commit passwords or private connection strings.**
4. Restore dependencies and build the API:

   ```bash
   dotnet restore
   dotnet build
   ```

5. Apply database migrations **if the repository includes EF Core migrations** and the correct project/startup project has been selected:

   ```bash
   dotnet ef database update
   ```

6. Run the Web API from its project directory:

   ```bash
   dotnet run
   ```

> The precise project path, connection-string key, startup command, and available API documentation route depend on the repository's current configuration. Check `Program.cs`, `appsettings.json`, and the project files before following these steps.

## Testing the API

Use the API's configured OpenAPI/Swagger interface (if enabled), Postman, or another HTTP client to exercise the implemented product and stock-in endpoints. Verify that the response codes and stored database values match the requested operation.

**Testing status:** Manual API testing can be documented here; an automated test suite is a planned improvement, not a claimed existing feature.

## What I'm learning

- Designing and implementing RESTful endpoints in ASP.NET Core.
- Using Entity Framework Core to persist application data.
- Separating API requests, business rules, and data access concerns.
- Handling inventory updates and planning for reliable validation and tests.

## Roadmap

- [x] Product CRUD endpoints
- [x] Stock-in functionality
- [ ] Stock-out functionality
- [ ] Automated tests
- [ ] Expanded API documentation
- [ ] Deployment and production configuration

## Author

**Owami Mashiya**  
GitHub: [Lesebo-Tech](https://github.com/Lesebo-Tech)

---

*StockPilot is an ongoing learning and portfolio project. Features and setup instructions will evolve as development continues.*
