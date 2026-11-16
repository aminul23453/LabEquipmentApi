
using LabEquipmentApi.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Configure OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion =
        Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

// Register PostgreSQL database
builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("LabDbConnection")));

// Register FluentValidation validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Enable Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "LabEquipmentApi v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
