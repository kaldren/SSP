# Copilot Instructions for SSP (System Shogun Platform)

## Project Overview

SSP (System Shogun Platform) is a platform for paid content creation and distribution. It's built with a monolithic architecture where content creators can upload videos, articles, and other media for subscription access.

**Current Status**: Early development phase. Initially designed for a single content creator, with potential future expansion to support multiple creators.

## Technology Stack

- **.NET 10.0** - Latest .NET runtime
- **ASP.NET Core Razor Pages** - Web UI framework
- **Entity Framework Core 10.0.2** - ORM for data access
  - Supports both SQL Server and InMemory database
  - InMemory is used by default (`UseInMemoryDatabase: true` in appsettings.json)
- **Microsoft Identity Web** - Azure AD authentication
- **LiteBus 4.2.0** - Lightweight CQRS/mediator library for command handling
- **Markdig 0.41.2** - Markdown processing

## Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
SSP/
├── src/
│   ├── SSP.Domain/          # Domain entities and interfaces (no dependencies)
│   ├── SSP.Application/     # Business logic, commands, CQRS handlers
│   ├── SSP.Infrastructure/  # Data access, EF Core, repositories
│   └── SSP.Web/            # Presentation layer (Razor Pages)
```

### Layer Dependencies

- **SSP.Domain**: No external dependencies (pure domain logic)
- **SSP.Application**: Depends on SSP.Domain
- **SSP.Infrastructure**: Depends on SSP.Application and SSP.Domain
- **SSP.Web**: Depends on SSP.Infrastructure and SSP.Application

### Vertical Slices Architecture

The project uses **Vertical Slices Architecture** (also known as feature-based organization) within the Application layer. Instead of organizing code by technical layers (Controllers, Services, Models), each feature is self-contained in its own folder with all related components.

**Key principles**:
- **Feature-based organization**: Code is grouped by business feature (e.g., `Courses/CreateCourse/`) rather than technical role
- **Minimal coupling**: Each slice is independent and minimizes coupling between features
- **Complete ownership**: A vertical slice contains everything needed for that feature (request, response, command, handler)
- **Easy to change**: Changes to a feature are isolated to that feature's folder

**Structure example**:
```
SSP.Application/
└── Courses/
    ├── CreateCourse/
    │   ├── CreateCourseCommand.cs
    │   ├── CreateCourseRequest.cs
    │   ├── CreateCourseResponse.cs
    │   └── CreateCourseCommandHandler.cs (all logic for this feature)
    ├── UpdateCourse/
    │   └── ... (complete feature slice)
    └── GetCourse/
        └── ... (complete feature slice)
```

This approach makes it easier to understand, modify, and test features independently. Read more about Vertical Slice Architecture at [Jimmy Bogard's blog](https://www.jimmybogard.com/vertical-slice-architecture/).

## Domain Model

The core domain revolves around courses and learning content:

- **Course**: A collection of modules with title, description, difficulty level
- **Module**: A section within a course containing multiple lessons
- **Lesson**: Individual content items (video, article, etc.) with type and content

### Key Enums
- `DifficultyLevel`: Defines course difficulty
- `LessonType`: Differentiates lesson content types

## Development Patterns

### CQRS with LiteBus

The application uses CQRS (Command Query Responsibility Segregation) via LiteBus:

```csharp
// Command definition
public record CreateCourseCommand(CreateCourseRequest request) : ICommand<CreateCourseResponse>;

// Command handler
public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, CreateCourseResponse>
{
    // Implementation
}
```

**When adding new features**:
1. Create Request/Response DTOs in `SSP.Application/[Feature]/[Action]/`
2. Create Command/Query classes implementing `ICommand<T>` or `IQuery<T>`
3. Create Handler classes implementing `ICommandHandler<TCommand, TResponse>` or `IQueryHandler<TQuery, TResponse>`

### Repository Pattern

Repositories are used for data access:
- Generic `IRepository<T>` interface in SSP.Domain
- Specific interfaces like `ICourseRepository` in SSP.Domain
- Concrete implementations in `SSP.Infrastructure/Persistence/Repositories/`

### Entity Framework Configuration

- Entity configurations are in `SSP.Infrastructure/Persistence/Configuration/`
- Configurations are auto-discovered via `ApplyConfigurationsFromAssembly()`
- Database context: `ApplicationDbContext`

## Building and Running

### Prerequisites
- .NET 10 SDK (10.0.102 or later)

### Commands

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the web application
cd src/SSP.Web
dotnet run

# Run with hot reload
dotnet watch run
```

### Build Notes

⚠️ **Known Build Warnings**: The project currently has nullable reference type warnings in SSP.Domain:
- `IRepository.cs(5,11)`: CS8632 - nullable annotation outside nullable context
- `Lesson.cs(10,18)` and `Lesson.cs(11,18)`: CS8632 - nullable annotations

These are benign warnings due to the Domain project not having `<Nullable>enable</Nullable>` in its .csproj file. The warnings do not affect functionality.

## Database Configuration

The application supports two database modes:

1. **InMemory** (default): Set `"UseInMemoryDatabase": true` in appsettings.json
2. **SQL Server**: Set `"UseInMemoryDatabase": false` and configure connection string

Database seeding occurs automatically on startup via `ApplicationDbContextSeed.SeedAsync()`.

## Authentication

The project uses **Azure AD (Entra ID)** via Microsoft Identity Web:
- Configuration in `appsettings.json` under `"AzureAd"`
- Default policy requires all pages to be authenticated
- Uses OpenID Connect

## File Organization

### Naming Conventions
- **Entities**: PascalCase, singular (e.g., `Course.cs`, `Module.cs`)
- **Commands**: `[Action][Entity]Command.cs` (e.g., `CreateCourseCommand.cs`)
- **Handlers**: `[Action][Entity]CommandHandler.cs`
- **Requests/Responses**: `[Action][Entity]Request.cs` / `[Action][Entity]Response.cs`

### Project Structure
```
SSP.Application/
├── Courses/
│   └── CreateCourse/
│       ├── CreateCourseCommand.cs
│       ├── CreateCourseRequest.cs
│       └── CreateCourseResponse.cs
└── Interfaces/
    └── IApplicationDbContext.cs

SSP.Domain/
├── Courses/
│   ├── Course.cs
│   ├── Module.cs
│   ├── Lesson.cs
│   ├── LessonType.cs
│   ├── DifficultyLevel.cs
│   └── ICourseRepository.cs
└── Interfaces/
    └── IRepository.cs

SSP.Infrastructure/
├── Persistence/
│   ├── ApplicationDbContext.cs
│   ├── ApplicationDbContextSeed.cs
│   ├── Configuration/
│   │   └── Courses/
│   └── Repositories/
│       ├── Repository.cs
│       └── CourseRepository.cs
└── DependencyInjection.cs

SSP.Web/
├── Pages/
│   ├── Index.cshtml / Index.cshtml.cs
│   ├── Courses.cshtml / Courses.cshtml.cs
│   ├── CourseDetail.cshtml / CourseDetail.cshtml.cs
│   └── LessonDetail.cshtml / LessonDetail.cshtml.cs
├── Program.cs
└── wwwroot/
```

## Coding Standards

### Domain Layer
- Rich domain models with validation methods (e.g., `SetTitle()`, `SetDescription()`)
- Validation logic in domain entities
- No infrastructure concerns

### Application Layer
- Use LiteBus for command/query handling
- Keep handlers thin - delegate to domain and repositories
- DTOs for requests/responses (records preferred)

### Presentation Layer
- Razor Pages with code-behind (`.cshtml.cs`)
- Inject services via constructor
- Keep page models focused on presentation concerns

## Common Tasks

### Adding a New Feature (Command)

1. **Create Domain Entity** (if needed) in `SSP.Domain/[Feature]/`
2. **Create Repository Interface** in `SSP.Domain/[Feature]/`
3. **Create Repository Implementation** in `SSP.Infrastructure/Persistence/Repositories/`
4. **Register Repository** in `SSP.Infrastructure/DependencyInjection.cs`
5. **Create Command/Request/Response** in `SSP.Application/[Feature]/[Action]/`
6. **Create Command Handler** in same folder
7. **Create Razor Page** in `SSP.Web/Pages/` (if needed for UI)

### Adding a New Entity

1. Create entity class in `SSP.Domain/[Feature]/`
2. Add `DbSet<TEntity>` property to `IApplicationDbContext` and `ApplicationDbContext`
3. Create EF configuration in `SSP.Infrastructure/Persistence/Configuration/[Feature]/`
4. Configuration will be auto-discovered via `ApplyConfigurationsFromAssembly()`

### Modifying Database Schema

1. Update entity in SSP.Domain
2. Update configuration in SSP.Infrastructure/Persistence/Configuration
3. For InMemory DB: Changes apply immediately on restart
4. For SQL Server: Create and apply migrations (not currently set up)

## Testing

⚠️ **No test projects currently exist** in the solution. When adding tests:
- Follow standard .NET naming: `SSP.[Layer].Tests` or `SSP.[Layer].UnitTests`
- Use xUnit, NUnit, or MSTest as preferred
- Mock repositories and DbContext for unit tests
- Consider integration tests with InMemory database

## Troubleshooting

### Build Warnings
- **CS8632 warnings**: These are safe to ignore. The Domain project intentionally doesn't enable nullable reference types globally, but some interfaces use nullable annotations. This doesn't affect runtime behavior.

### Authentication Issues
- Ensure Azure AD configuration in `appsettings.json` is correct
- For local development, consider adding `[AllowAnonymous]` attribute temporarily
- User secrets can be used to store sensitive Azure AD config

### Database Issues
- Default is InMemory - data is lost on restart
- Check `UseInMemoryDatabase` setting in appsettings.json
- Database is created and seeded automatically on startup

## Dependencies Management

### Installing New Packages
```bash
# Navigate to specific project
cd src/SSP.[ProjectName]

# Add package
dotnet add package [PackageName]
```

### Key Dependencies
- **LiteBus**: Handles commands/queries - don't replace with MediatR without careful consideration
- **Microsoft.Identity.Web**: Azure AD integration - configured in Program.cs
- **EF Core**: Version must match .NET version (currently 10.0.2)

## Git Workflow

The repository uses standard Git practices:
- `.gitignore` excludes bin/, obj/, and standard VS files
- Solution file: `SSP.slnx` (newer XML-based solution format)

## Additional Notes

- **Monolithic Architecture**: All components are in a single application for simplicity
- **Future Scalability**: Architecture supports breaking into microservices if needed
- **Content Focus**: Currently focused on course-based content (videos, articles)
- **Single Creator**: Initial design for one creator; multi-creator support is future consideration

## Questions to Ask When Uncertain

1. **Domain Logic**: Does this belong in the Domain layer, or is it application/infrastructure concern?
2. **Repository Pattern**: Is a new repository needed, or can generic repository handle it?
3. **CQRS**: Is this a Command (write) or Query (read) operation?
4. **Authentication**: Does this page/endpoint need authentication, or should it be public?
5. **Database**: Should this work with both InMemory and SQL Server, or is it test-specific?

## Getting Help

- Review `PROJECT.md` for high-level architectural decisions
- Check existing code for patterns before creating new structures
- Domain entities should be self-validating with clear business rules
- Follow Clean Architecture principles: dependencies flow inward toward Domain
