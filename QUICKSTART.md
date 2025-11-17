# 🚀 Quick Start Guide

## Get Up and Running in 5 Minutes

### Prerequisites Check
- ✅ .NET 8.0 SDK installed
- ✅ SQL Server or LocalDB installed
- ✅ Visual Studio Code or Visual Studio

### Step 1: Navigate to Backend
```powershell
cd c:\Users\USER\Downloads\PRA\backend
```

### Step 2: Restore Packages
```powershell
dotnet restore
```

### Step 3: Install EF Core Tools (if not installed)
```powershell
dotnet tool install --global dotnet-ef --version 8.0.0
```

If you get an error, try:
```powershell
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef --version 8.0.0
```

Then restart your terminal.

### Step 4: Create Database
```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Step 5: Run the Application
```powershell
dotnet run
```

### Step 6: Open Swagger
Open your browser and go to:
```
https://localhost:5001/swagger
```

### Step 7: Test the API

1. **Click on** `POST /api/auth/login`
2. **Click** "Try it out"
3. **Paste this JSON**:
```json
{
  "email": "admin@performancemanagement.com",
  "password": "Admin@123"
}
```
4. **Click** "Execute"
5. **Copy the token** from the response
6. **Click** "Authorize" button at the top
7. **Enter**: `Bearer <your-token-here>`
8. **Now you can test** all other endpoints!

## 🎯 What You Just Did

- ✅ Built a complete ASP.NET Core Web API
- ✅ Created a SQL Server database
- ✅ Set up authentication with JWT
- ✅ Configured role-based authorization
- ✅ Implemented the repository pattern
- ✅ Created a service layer
- ✅ Set up Swagger documentation

## 📱 Connect Frontend

To connect your React frontend:

1. Make sure backend is running on `https://localhost:5001`
2. Frontend should call API at `https://localhost:5001/api/`
3. Include JWT token in Authorization header: `Bearer <token>`

## 🔥 Hot Tips

- Use `dotnet watch run` for auto-restart on file changes
- Check `logs/` folder for application logs
- Swagger UI is your friend for testing
- Default admin password should be changed immediately

## ⚠️ Troubleshooting

**"Could not find dotnet-ef"**
- Restart your terminal after installing tools
- Make sure .NET SDK is in your PATH

**Database connection error**
- Check if SQL Server LocalDB is running
- Verify connection string in appsettings.json

**Port already in use**
- Change ports in Properties/launchSettings.json
- Or stop other applications using ports 5000/5001

## 📚 Need More Help?

- See `README.md` for detailed documentation
- See `SETUP.md` for step-by-step instructions
- See `BACKEND_IMPLEMENTATION_SUMMARY.md` for what's included
- Check `API_SPECIFICATION.md` for API details

## 🎉 You're Ready!

Your backend is now running and ready to handle requests from the frontend!
