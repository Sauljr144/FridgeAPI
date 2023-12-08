using Microsoft.EntityFrameworkCore;
using FridgeAPI.Services;
using FridgeAPI.Services.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<FridgeItemService>();
builder.Services.AddScoped<ShoppingItemService>();

var connectionString = builder.Configuration.GetConnectionString("MyFridgeString");

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(connectionString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
