using AutoMapper;
using BiteBuddy.Services.CouponAPI.Data;
using BiteBuddy.Services.CouponAPI.Extensions;
using BiteBuddy.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//Add below when creating ApplicationDbContext Class to tell application that we will we 
// using sql for database
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    // get connection string here from appsetting.cs
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiteBuddyConStr"));
});

IMapper mapper = BiteBuddy.Services.CouponAPI.MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
IServiceCollection serviceCollection = builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register Response Dto class for DI
builder.Services.AddScoped<ResponseDto>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(option=>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, 
        securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

builder.AddAppAuthentication(); // calls extension method

builder.Services.AddAuthorization();
//

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
//ApplyMigration();
app.Run();

//void ApplyMigration()
//{
//    using(var scope = app.Services.CreateScope())
//    {
//        var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
//        if(db.Database.GetPendingMigrations().Count()>0)
//        {
//            db.Database.Migrate();
//        }
//    }
//}