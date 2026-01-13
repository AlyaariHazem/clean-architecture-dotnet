using CleanArchitecture.Infrastracture;
using CleanArchitecture.Infrastracture.Persistence.Data;
using CleanArchitecture.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// service registration (IserviceCollection)

builder.Services.AddControllers();

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


builder.AddInfrastructureRegistration();
builder.AddServiceRegistrations();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(""));

var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
