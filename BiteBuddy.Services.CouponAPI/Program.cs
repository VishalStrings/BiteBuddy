using AutoMapper;
using BiteBuddy.Services.CouponAPI.Data;
using BiteBuddy.Services.CouponAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Register Response Dto class for DI
builder.Services.AddScoped<ResponseDto>();

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
// Add services to the container.
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