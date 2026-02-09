# Student Registration System

A complete ASP.NET Core Razor Pages application for managing student registrations using Entity Framework Core with SQLite database.

## Features

- **Full CRUD Operations**: Create, Read, Update, and Delete student records
- **Data Validation**: Comprehensive validation using Data Annotations
- **Responsive UI**: Bootstrap 5 with modern styling
- **Database**: SQLite with Entity Framework Core
- **Unique Constraints**: Registration numbers are unique across students

## Technologies Used

- ASP.NET Core 8.0
- Razor Pages
- Entity Framework Core
- SQLite Database
- Bootstrap 5
- jQuery Validation

## Student Entity Properties

- **Id**: Primary key (auto-generated)
- **Full Name**: Required, max 100 characters
- **Registration Number**: Required, unique, max 20 characters
- **Course**: Required, max 100 characters
- **Year of Study**: Required, range 1-5

## Project Structure

```
StudentRegistrationSystem/
├── Models/
│   └── Student.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Pages/
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Students/
│   │   ├── Index.cshtml & Index.cshtml.cs
│   │   ├── Create.cshtml & Create.cshtml.cs
│   │   ├── Edit.cshtml & Edit.cshtml.cs
│   │   └── Delete.cshtml & Delete.cshtml.cs
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── wwwroot/
│   ├── css/site.css
│   └── js/site.js
├── Program.cs
├── appsettings.json
└── StudentRegistrationSystem.csproj
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or any C# IDE

### Installation & Setup

1. **Navigate to the project directory:**
   ```bash
   cd StudentRegistrationSystem
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Install Entity Framework Core tools (if not already installed):**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Create and apply database migrations:**
   ```bash
   # Create initial migration
   dotnet ef migrations add InitialCreate
   
   # Apply migration to create database
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```

6. **Open your browser and navigate to:**
   ```
   https://localhost:7000/Students
   ```

## Entity Framework Commands

### Creating Migrations
```bash
# Create a new migration
dotnet ef migrations add [MigrationName]

# Example: Add new field
dotnet ef migrations add AddEmailToStudent
```

### Applying Migrations
```bash
# Apply all pending migrations
dotnet ef database update

# Apply specific migration
dotnet ef database update [MigrationName]
```

### Database Management
```bash
# Drop database (removes all data)
dotnet ef database drop

# View migration history
dotnet ef migrations list

# Remove last migration (if not applied)
dotnet ef migrations remove
```

### Code First Workflow
```bash
# Full workflow for model changes:
1. Modify your model classes
2. dotnet ef migrations add [DescriptiveName]
3. dotnet ef database update
```

## Configuration

### Database Connection
The SQLite database connection is configured in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=students.db"
  }
}
```

### Dependency Injection
The `ApplicationDbContext` is registered in `Program.cs`:
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## Validation Rules

- **Full Name**: Required, maximum 100 characters
- **Registration Number**: Required, unique, maximum 20 characters
- **Course**: Required, maximum 100 characters
- **Year of Study**: Required, must be between 1 and 5

## Key Features Implementation

### Async/Await Pattern
All database operations use async/await for better performance:
```csharp
public async Task<IActionResult> OnPostAsync()
{
    await _context.SaveChangesAsync();
    return RedirectToPage("./Index");
}
```

### Data Annotations Validation
```csharp
[Required(ErrorMessage = "Full Name is required")]
[StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
public string FullName { get; set; }
```

### Unique Constraint
Registration numbers are enforced as unique in the database:
```csharp
entity.HasIndex(e => e.RegistrationNumber).IsUnique();
```

## Troubleshooting

### Common Issues

1. **Migration Error**: If you get migration errors, try:
   ```bash
   dotnet ef database drop
   dotnet ef migrations remove
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

2. **Port Already in Use**: Change the port in `Properties/launchSettings.json` or use:
   ```bash
   dotnet run --urls="https://localhost:7001"
   ```

3. **Package Restore Issues**:
   ```bash
   dotnet clean
   dotnet restore
   dotnet build
   ```

## Development Guidelines

- Follow the Repository pattern for larger applications
- Implement proper error handling and logging
- Add unit tests for business logic
- Consider implementing caching for better performance
- Add authentication and authorization for production use

## License

This project is created for educational purposes as part of Selected Topics in Software Engineering coursework.