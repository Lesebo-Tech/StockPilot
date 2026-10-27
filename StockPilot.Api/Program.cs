
using Microsoft.EntityFrameworkCore;
using StockPilot.Api.Data;
using StockPilot.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the SQL Server database context.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Register the Inventory Service.
builder.Services.AddScoped<InventoryService>();

// Register API controllers.
builder.Services.AddControllers();

// Enable OpenAPI documentation.
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable OpenAPI in development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Configure middleware.
app.UseHttpsRedirection();

app.UseAuthorization();

// Map API endpoints.
app.MapControllers();

app.Run();
