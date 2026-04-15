using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Services;
using StudentRegistrationSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Add Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StaffOrAdmin", policy => policy.RequireRole("Admin", "Staff"));
    options.AddPolicy("StudentOrHigher", policy => policy.RequireRole("Admin", "Staff", "Student"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

// Commented out for development - uncomment for production with proper SSL certificate
// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Ensure database is created and seed admin user
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Delete old database and recreate with new schema
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    // Seed admin and staff users if not exists
    if (!context.Users.Any(u => u.Email == "admin@studentreg.com"))
    {
        context.Users.Add(new User
        {
            Username = "admin@studentreg.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            FullName = "System Administrator",
            Email = "admin@studentreg.com",
            Role = "Admin",
            IsActive = true,
            CreatedAt = DateTime.Now
        });
    }

    if (!context.Users.Any(u => u.Email == "staff@studentreg.com"))
    {
        context.Users.Add(new User
        {
            Username = "staff@studentreg.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("staff123"),
            FullName = "Academic Staff",
            Email = "staff@studentreg.com",
            Role = "Staff",
            IsActive = true,
            CreatedAt = DateTime.Now
        });
    }

    context.SaveChanges();
}

app.Run();