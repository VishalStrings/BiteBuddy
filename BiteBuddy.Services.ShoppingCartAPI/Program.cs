using AutoMapper;
using BiteBuddy.Services.ShoppingCartAPI;
using BiteBuddy.Services.ShoppingCartAPI.Data;
using BiteBuddy.Services.ShoppingCartAPI.Models.Dto;
using BiteBuddy.Services.ShoppingCartAPI.Service;
using BiteBuddy.Services.ShoppingCartAPI.Service.IService;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// using sql for database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // get connection string here from appsetting.cs
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiteBuddyConStr"));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();

// below will add http client for product with the base Url 
builder.Services.AddHttpClient("Product", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:ProductAPI"]));

// below will add http client for coupon with the base Url 
builder.Services.AddHttpClient("Coupon", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:CouponAPI"]));

//To start logging using Serilog
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("/log/ShoppingCartApiLogs.txt", rollingInterval: RollingInterval.Month).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddScoped<ResponseDto>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
