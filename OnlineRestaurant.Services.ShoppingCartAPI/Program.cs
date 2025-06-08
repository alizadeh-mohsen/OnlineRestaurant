using OnlineRestaurant.Services.ProductAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomServices();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
