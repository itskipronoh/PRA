# Backend Setup Instructions

## Complete These Steps to Finish Setup

### 1. Install Entity Framework Core Tools

You need to install the EF Core tools globally to run migrations:

```powershell
# Uninstall existing version first (if needed)
dotnet tool uninstall --global dotnet-ef

# Install the latest version
dotnet tool install --global dotnet-ef --version 8.0.0
```

If you encounter issues, restart your terminal and try again.

### 2. Create Database Migration

```powershell
cd backend
dotnet ef migrations add InitialCreate
```

### 3. Apply Migration to Database

```powershell
dotnet ef database update
```

This will create the database and all tables based on your models.

### 4. Run the Application

```powershell
dotnet run
```

Or use the watch mode for development:

```powershell
dotnet watch run
```

### 5. Test the API

1. Open your browser and navigate to: `https://localhost:5001/swagger`
2. Test the login endpoint with default admin credentials:
   - Email: `admin@performancemanagement.com`
   - Password: `Admin@123`

### 6. Update Configuration (Important!)

Before deploying to production:

1. **Update JWT Secret** in `appsettings.json`:
   - Change the `JwtSettings:Secret` to a strong, unique value
   
2. **Update Connection String** for your production database:
   - Update `ConnectionStrings:DefaultConnection`

3. **Configure CORS** to allow only your frontend URL:
   - Update `Cors:AllowedOrigins` in `appsettings.json`

## Project Status

✅ ASP.NET Core 8.0 project created
✅ Entity models implemented
✅ DTOs created for all operations
✅ Database context configured with relationships
✅ Repository pattern implemented
✅ Service layer created
✅ JWT authentication configured
✅ Authorization with role-based access control
✅ API controllers created for:
   - Authentication (login, refresh, logout, change password)
   - Users (CRUD operations)
   - Appraisals (CRUD operations)
✅ Global exception handling middleware
✅ Swagger/OpenAPI documentation
✅ CORS configuration
✅ Logging with Serilog

## Note on Additional Controllers

The backend includes service implementations for all entities. You can create additional controllers following the same pattern as `AppraisalsController.cs` and `UsersController.cs` for:

- Self Assessments
- Manager Reviews
- Meetings
- Appeals
- Mediations
- Goals
- Documents
- Notifications
- Dashboards

Each service interface is already defined in `ServiceInterfaces.cs` and implemented in `CoreServices.cs` and `AdditionalServices.cs`.

## Database Schema

The database includes the following tables:
- Users
- Appraisals
- SelfAssessments
- ManagerReviews
- Meetings
- Appeals
- Mediations
- Goals
- Documents
- Notifications

All relationships are properly configured with foreign keys and cascading behaviors.

## Default Admin User

After running migrations, a default admin user will be seeded:
- **Email:** admin@performancemanagement.com
- **Password:** Admin@123
- **Role:** Admin

**IMPORTANT:** Change this password immediately after first login!

## Troubleshooting

### "Could not execute dotnet-ef"
- Ensure .NET 8.0 SDK is installed
- Reinstall dotnet-ef tools
- Restart your terminal/IDE

### Database Connection Errors
- Check if SQL Server LocalDB is installed
- Verify connection string in appsettings.json
- Ensure SQL Server service is running

### Build Errors
- Run `dotnet restore`
- Run `dotnet clean` then `dotnet build`
- Check for missing using statements

## Next Steps

1. Complete the database setup (migrations)
2. Run and test the backend API
3. Connect the frontend to the backend
4. Test end-to-end workflows
5. Implement remaining controllers if needed
6. Add comprehensive unit tests
7. Configure production deployment
