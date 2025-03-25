using CaixaDeBanco.Api;
using CaixaDeBanco.Database.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conString = builder.Configuration["CaixaDeBancoDatabase"] ??
     throw new InvalidOperationException("Connection string 'CaixaDeBancoDatabase'" +
    " not found.");
builder.Services.AddDbContext<BancoDbContext>(options =>
    options.UseSqlServer(conString, b => b.MigrationsAssembly("CaixaDeBanco.Database")));
builder.Services.AddHandlerDependencyGroup();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
