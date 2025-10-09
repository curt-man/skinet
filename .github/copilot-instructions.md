# Copilot Instructions for SkiNet E-Commerce Codebase

## Big Picture Architecture
- **Monorepo Structure:** Contains backend (`Skinet.API`, `Skinet.Core`, `Skinet.Infrastructure`) and frontend (`client/`) in a single repo.
- **Backend:**
  - Built with .NET 8 (ASP.NET Core API).
  - `Skinet.Core`: Domain models and interfaces.
  - `Skinet.Infrastructure`: Data access, migrations, and service implementations.
  - `Skinet.API`: API controllers, middleware, configuration, and startup logic.
  - Uses SQL Server (via Entity Framework Core) and Redis for caching.
  - Payment integration via Stripe.
- **Frontend:**
  - Angular 18 app in `client/`.
  - Uses Angular Material and Tailwind CSS for UI.
  - Implements product catalog, cart, authentication, and order flows.

## Developer Workflows
- **Build:**
  - Backend: `dotnet build` or `dotnet run` in `Skinet.API`.
  - Frontend: `ng build` or `ng serve` in `client/`.
- **Tests:**
  - Angular: `ng test` (unit), `ng e2e` (end-to-end).
  - .NET: Standard xUnit/MSTest patterns (no custom test runner found).
- **Docker:**
  - Use `docker-compose.yml` to start SQL Server and Redis containers.
  - `docker compose up -d` before running backend locally.
- **Stripe:**
  - Stripe API keys must be set in `Skinet.API/appsettings.json`.
  - Webhook secret (`whsecret`) used for payment verification.

## Project-Specific Conventions
- **Angular Standalone Components:**
  - Uses new Angular v17+ `input`/`output` APIs for component bindings.
  - Property binding (`[property]`) preferred for dynamic values.
- **Pagination:**
  - Product catalog uses offset-based pagination, but may be refactored for keyset pagination.
- **DTOs:**
  - API uses DTOs for request/response models, located in `Skinet.API/DTOs/`.
- **Error Handling:**
  - Custom middleware in `Skinet.API/Middlewares/ExceptionMiddleware.cs`.
- **Seed Data:**
  - Seed files in `Skinet.Infrastructure/Data/SeedData/` are copied to output for migrations.

## Integration Points
- **Redis:** Used for caching, configured in `Skinet.API/appsettings.json`.
- **Stripe:** Payment processing and webhooks.
- **Docker:** For local dev databases and cache.
- **OpenTelemetry/Seq:** Logging can be exported to Seq or Azure Monitor (see `Program.cs`).

## Key Files & Directories
- `Skinet.API/Controllers/`: API endpoints.
- `Skinet.API/DTOs/`: Data transfer objects.
- `Skinet.Core/Entities/`: Domain models.
- `client/src/app/`: Angular app source.
- `docker-compose.yml`: Container orchestration.
- `README.md`: Setup and workflow instructions.

## Examples
- To add a new API endpoint: create a controller in `Skinet.API/Controllers/`, update DTOs as needed, and implement logic in `Skinet.Infrastructure/Services/`.
- To add a new Angular feature: generate a standalone component in `client/src/app/features/`, use `input`/`output` APIs for bindings.

---

If any section is unclear or missing, please provide feedback so this guide can be improved for future AI agents.
