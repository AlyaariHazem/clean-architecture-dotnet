using CleanArchitecture.API.Filters;
using CleanArchitecture.API.Middleware;
using CleanArchitecture.Infrastracture;
using CleanArchitecture.Infrastracture.Persistence.Data;
using CleanArchitecture.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// service registration (IserviceCollection)

builder.Services.AddControllers(options =>
{
    // Add global validation filter
    options.Filters.Add<ValidationFilterAttribute>();
});

// Configure API behavior for consistent error responses
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // Disable default model state validation to use our custom filter
    options.SuppressModelStateInvalidFilter = false;
});

// Configure CORS to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clean Architecture API",
        Version = "v1",
        Description = "API documentation for Clean Architecture implementation"
    });
});


// Register DbContext first
// For development, you can use InMemory database or configure a connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=CleanArchitectureDb;Trusted_Connection=true;TrustServerCertificate=true";

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));

// Register Infrastructure (Unit of Work)
builder.AddInfrastructureRegistration();

// Register Application Services
builder.AddServiceRegistrations();

var app = builder.Build();

// Configure global exception handling - must be first in pipeline
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure Swagger - must be early in the pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture API v1");
        c.RoutePrefix = "swagger"; // Swagger UI will be available at /swagger
    });
}

app.UseRouting();

// Enable CORS - must be after UseRouting and before UseAuthorization
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
