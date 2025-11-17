# Performance Management System - Backend API

ASP.NET Core 8.0 Web API for the Performance Management System

## Prerequisites

- .NET 8.0 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio Code or Visual Studio 2022

## Getting Started

### 1. Database Setup

The application uses Entity Framework Core with SQL Server. Update the connection string in `appsettings.json` if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PerformanceManagementDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 2. Create Database Migrations

```powershell
# Add Entity Framework tools if not already installed
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migration to create database
dotnet ef database update
```

### 3. Run the Application

```powershell
# Restore packages
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:5001/swagger`

## Default Admin Credentials

```
Email: admin@performancemanagement.com
Password: Admin@123
```

**Important:** Change the admin password after first login!

## API Documentation

The API includes Swagger/OpenAPI documentation. Access it at `/swagger` when the application is running.

### Key Endpoints

#### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh` - Refresh JWT token
- `POST /api/auth/logout` - Logout user
- `POST /api/auth/change-password` - Change password

#### Users
- `GET /api/users/me` - Get current user profile
- `GET /api/users/{id}` - Get user by ID
- `GET /api/users` - Get all users (HR/Admin only)
- `PUT /api/users/{id}` - Update user profile
- `GET /api/users/{id}/direct-reports` - Get direct reports

#### Appraisals
- `POST /api/appraisals` - Create appraisal (HR/Admin only)
- `GET /api/appraisals/{id}` - Get appraisal by ID
- `GET /api/appraisals` - Get all appraisals with filtering
- `PUT /api/appraisals/{id}` - Update appraisal (HR/Admin only)
- `GET /api/appraisals/employee/{employeeId}` - Get employee appraisals
- `GET /api/appraisals/manager/{managerId}` - Get manager's appraisals

## Project Structure

```
backend/
├── Controllers/          # API endpoints
├── Data/                 # Database context and configurations
├── Middleware/           # Custom middleware (exception handling)
├── Models/
│   ├── DTOs/             # Data Transfer Objects
│   ├── Entities/         # Database entities
│   └── Enums/            # Enumerations
├── Repositories/         # Data access layer
├── Services/             # Business logic layer
├── appsettings.json      # Configuration
└── Program.cs            # Application entry point
```

## Key Features

- **JWT Authentication** - Secure token-based authentication
- **Role-Based Authorization** - Support for Employee, Manager, HR, and Admin roles
- **Repository Pattern** - Clean separation of data access
- **Service Layer** - Business logic separation
- **Global Exception Handling** - Consistent error responses
- **Entity Framework Core** - ORM with code-first migrations
- **Swagger Documentation** - Interactive API documentation
- **Logging** - Serilog integration for structured logging

## Configuration

### JWT Settings

Update JWT settings in `appsettings.json`:

```json
{
  "JwtSettings": {
    "Secret": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "PerformanceManagementAPI",
    "Audience": "PerformanceManagementClient",
    "ExpiryMinutes": 60,
    "RefreshTokenExpiryDays": 7
  }
}
```

### CORS Configuration

Configure allowed origins in `appsettings.json`:

```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:5173",
      "http://localhost:3000"
    ]
  }
}
```

## Development

### Adding a New Migration

```powershell
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### Rolling Back a Migration

```powershell
dotnet ef database update <PreviousMigrationName>
dotnet ef migrations remove
```

## Testing

Use Swagger UI or tools like Postman to test the API endpoints.

### Sample Login Request

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@performancemanagement.com",
  "password": "Admin@123"
}
```

### Using the JWT Token

Include the token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

## Troubleshooting

### Database Connection Issues

- Ensure SQL Server LocalDB is installed
- Check the connection string in `appsettings.json`
- Verify SQL Server service is running

### Migration Issues

```powershell
# Remove all migrations and start fresh
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Next Steps

1. Update the admin password
2. Create test users for different roles
3. Test all API endpoints
4. Configure production database connection
5. Set up environment-specific configurations
6. Implement comprehensive logging
7. Add input validation with FluentValidation
8. Implement unit and integration tests

## Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [JWT Authentication in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/)
