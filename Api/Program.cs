using Api.Controllers;
using Api.DataAccess;
using Api.DataAccess.Interfaces;
using Api.Repositories;
using Api.Repositories.Interfaces;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var corsPolicy = "corsPolicy";

builder.Services.AddCors(option => {
    option.AddPolicy(corsPolicy, configurePolicy => {
        configurePolicy.AllowAnyMethod().AllowAnyHeader().WithOrigins("*");
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IData, DataAccess>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductController>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.ConfigureExceptionHandler();
//app.UseExceptionHandler("/api/error");
app.UseCors(corsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
