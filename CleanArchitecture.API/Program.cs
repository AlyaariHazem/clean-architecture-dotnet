using CleanArchitecture.Infrastracture;
using CleanArchitecture.Infrastracture.Persistence.Data;
using CleanArchitecture.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// service registration (IserviceCollection)

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddInfrastructureRegistration();
builder.AddServiceRegistrations();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(""));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
