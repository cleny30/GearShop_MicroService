using DataAccess.Core;
using DataAccess.DAO;
using DataAccess.IRepository;
using DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductDAO>(); // ??ng ký ProductDAO
builder.Services.AddScoped<CategoryDAO>(); // ??ng ký ProductDAO
builder.Services.AddScoped<BrandDAO>(); // ??ng ký ProductDAO
builder.Services.AddScoped<HomeDAO>();    // ??ng ký HomeDAO
builder.Services.AddScoped<IHomeRepository, HomeRepository>();

builder.Services.AddControllers();
builder.Services.ConfigureDependencyInjection();
// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
