using Microsoft.EntityFrameworkCore;
using BiteBuddy.Services.AuthAPI.Data;
using Microsoft.AspNetCore.Identity;
using BiteBuddy.Services.AuthAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using BiteBuddy.Services.AuthAPI.Service.IService;
using BiteBuddy.Services.AuthAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add below when creating ApplicationDbContext Class to tell application that we will we 
// using sql for database
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    // get connection string here from appsetting.cs
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiteBuddyConStr"));
});

// to get jwt token values from the appSetting.json
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
// To Add default Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); // for JWT token registration
builder.Services.AddScoped<IAuthService, AuthService>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
