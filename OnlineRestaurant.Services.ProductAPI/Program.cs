using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.ProductAPI.Data;
using OnlineRestaurant.Services.ProductAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.AddCustomServcies();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigrations();
app.Run();

void ApplyMigrations()
{

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (dbContext.Database.GetMigrations().Count() > 0)
        {
            dbContext.Database.Migrate();
        }
    }
}