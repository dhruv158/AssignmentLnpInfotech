# AssignmentLnp

Simple ASP.NET Core Web API for managing Employees and Departments.

## Overview

- Target framework: .NET 10
- REST API with Employee and Department endpoints
- Uses Entity Framework Core with SQL Server
- EF Core Code-First Migrations
- AutoMapper for DTO mapping
- Repository and Service layers
- Dependency Injection
- Async/Await
- Employee search, filtering, sorting and pagination
- Employee soft delete
- Entity relationships between Employee and Department
- Global exception handling middleware returns JSON error responses
- Swagger/OpenAPI for API testing

## Prerequisites

- .NET 10 SDK
- SQL Server
- SQL Server Management Studio (SSMS)
- Visual Studio 2022/2026 or VS Code (optional)

## Configuration

1. Set the database connection string in `appsettings.json` under:

`ConnectionStrings:DefaultConnection`

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=AssignmentLnp;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance.

## Database Setup

This project uses Entity Framework Core Code-First Migrations.

### Using .NET CLI

Create a migration:

```bash
dotnet ef migrations add InitialCreate
```

Update the database:

```bash
dotnet ef database update
```

If the migration already exists in the project, only run:

```bash
dotnet ef database update
```

### Using Package Manager Console

```powershell
Add-Migration InitialCreate
```

Then:

```powershell
Update-Database
```

The database and required tables will be created/updated based on the EF Core models.

## Run (CLI)

1. Open a terminal in the solution folder (`AssignmentLnp`).

2. Restore dependencies:

```bash
dotnet restore
```

3. Build the project:

```bash
dotnet build
```

4. Move to the API project if required:

```bash
cd AssignmentLnp
```

5. Run the application:

```bash
dotnet run
```

The API will listen on the URLs configured in `launchSettings.json`.

When running in Development, Swagger UI is available at:

```text
https://localhost:7117/swagger
```

The port may differ depending on the configured launch profile.

## Run Using Visual Studio

1. Open the solution in Visual Studio.
2. Make sure SQL Server is running.
3. Verify the connection string in `appsettings.json`.
4. Select the HTTPS launch profile.
5. Press `F5` or click Run.
6. Open Swagger if it does not open automatically.

Example:

```text
https://localhost:7117/swagger
```

## API Endpoints

### Employees

- GET `/api/employee?search=&departmentId=&isActive=&sortBy=&ascending=&pageNumber=&pageSize=`
  - Get employees with search, filtering, sorting and pagination.

- GET `/api/employee/{id}`
  - Get an employee by ID.

- POST `/api/employee`
  - Create a new employee.

- PUT `/api/employee`
  - Update an existing employee.

- DELETE `/api/employee/{id}`
  - Soft delete an employee.

### Departments

- GET `/api/department`
  - Get all departments.

- GET `/api/department/{id}`
  - Get a department by ID.

- POST `/api/department`
  - Create a new department.

## Employee Search, Filter, Sorting and Pagination

The Employee GET endpoint supports:

- Search
- Department filtering
- Active/inactive filtering
- Sorting
- Pagination

Example:

```text
/api/employee?search=John&departmentId=1&isActive=true&sortBy=Salary&ascending=false&pageNumber=1&pageSize=10
```

### Query Parameters

| Parameter | Description |
|---|---|
| `search` | Search employees |
| `departmentId` | Filter employees by department |
| `isActive` | Filter employees by active status |
| `sortBy` | Field used for sorting |
| `ascending` | `true` for ascending, `false` for descending |
| `pageNumber` | Page number |
| `pageSize` | Number of records per page |

Example:

```text
GET /api/employee?search=John&departmentId=1&isActive=true&sortBy=Salary&ascending=false&pageNumber=1&pageSize=10
```

## Soft Delete

Employee deletion is implemented using soft delete.

When an employee is deleted, the record is not physically removed from the database. Instead, the employee is marked as deleted.

Deleted employees are excluded from normal employee queries.

## Entity Relationship

An Employee belongs to a Department.

The relationship is implemented using:

```csharp
public int DepartmentId { get; set; }

public Department Department { get; set; } = null!;
```

The Department entity contains the employee collection:

```csharp
public ICollection<Employee> Employees { get; set; }
    = new List<Employee>();
```

Entity Framework Core manages this relationship using the `DepartmentId` foreign key.

When retrieving employees, the related Department can be loaded using:

```csharp
.Include(e => e.Department)
```

This allows the API to return department information along with employee details.

## DTOs

DTOs are used to separate API models from database entities.

The application uses DTOs for:

- Employee requests/responses
- Department requests/responses

This prevents database entities from being directly exposed through the API.

## AutoMapper

AutoMapper is used for mapping between entities and DTOs.

Example:

```csharp
CreateMap<EmployeeDto, Employee>();

CreateMap<Employee, EmployeeDto>();

CreateMap<DepartmentDto, Department>();
```

## Repository Pattern

The project uses the Repository Pattern to separate database access from business logic.

The flow is:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
ApplicationDbContext
    ↓
SQL Server
```

Repositories are responsible for database operations, while services contain business logic.

## Dependency Injection

Dependencies are registered in `Program.cs`.

Examples:

```csharp
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
```

ASP.NET Core Dependency Injection automatically provides these dependencies to controllers and services.

## Async Programming

The application uses asynchronous programming for database operations.

Examples:

```csharp
await repository.GetAllAsync();
```

and:

```csharp
await repository.SaveChangesAsync();
```

This prevents blocking while waiting for database operations.

## Validation

API input data is validated before processing.

Invalid requests return an appropriate HTTP response such as:

```text
400 Bad Request
```

## Error Handling

The project includes a global `ExceptionMiddleware` that logs unhandled exceptions and returns a JSON payload:

```json
{
  "statusCode": 500,
  "message": "Internal Server Error"
}
```

In Development, the middleware will include the exception message for easier debugging.

## HTTP Status Codes

| Status Code | Description |
|---|---|
| 200 | OK |
| 201 | Created |
| 400 | Bad Request |
| 404 | Not Found |
| 500 | Internal Server Error |

## Swagger

Swagger is enabled for API documentation and testing.

Open:

```text
https://localhost:7117/swagger
```

The port may differ depending on `launchSettings.json`.

Swagger can be used to test all Employee and Department endpoints.

## Postman

A Postman collection is included with the project for API testing.

Import the following file into Postman:

```text
AssignmentLnp API.postman_collection.json
```

The collection contains requests for:

### Employee

- Get All Employees
- Search/Filter/Sort/Pagination
- Get Employee By ID
- Create Employee
- Update Employee
- Delete Employee

### Department

- Get All Departments
- Get Department By ID
- Create Department

Update the `baseUrl` variable in Postman if the application is running on a different port.

Example:

```text
https://localhost:7117
```

## EF Core Migrations

Migration files are included in the project.

To create a new migration:

```bash
dotnet ef migrations add MigrationName
```

Example:

```bash
dotnet ef migrations add InitialCreate
```

To update the database:

```bash
dotnet ef database update
```

To remove the last migration:

```bash
dotnet ef migrations remove
```

To list migrations:

```bash
dotnet ef migrations list
```

## Troubleshooting

### Database Connection Error

Check the connection string in:

```text
appsettings.json
```

Make sure:

- SQL Server is running.
- The server name is correct.
- The database connection string is correct.
- `TrustServerCertificate=True` is included if required for local development.

### Migration Command Not Found

Install the EF Core CLI tool:

```bash
dotnet tool install --global dotnet-ef
```

Then verify:

```bash
dotnet ef --version
```

### HTTPS Certificate Error

For local development, run:

```bash
dotnet dev-certs https --trust
```

Then restart Visual Studio and the browser.

## Deliverables

The project includes:

- Complete source code
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server integration
- EF Core Code-First Migration files
- Employee CRUD APIs
- Department APIs
- Search, filtering, sorting and pagination
- Soft delete
- DTOs
- AutoMapper
- Repository layer
- Service layer
- Dependency Injection
- Validation
- Global exception handling
- Swagger/OpenAPI
- Postman collection
- README documentation

## Notes

- Use Visual Studio to run and debug the API, or use the CLI steps above.
- Make sure SQL Server is running before starting the API.
- Update the connection string before running the application.
- Run `dotnet ef database update` before testing the APIs if the database has not been created.
- Use the HTTPS launch profile to avoid HTTP-to-HTTPS redirect issues during Swagger testing.
