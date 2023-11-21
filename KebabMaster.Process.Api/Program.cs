using System.Text;
using KebabMaster.Process.Api.Interfaces;
using KebabMaster.Process.Api.Mappings;
using KebabMaster.Process.Api.Services;
using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Interfaces;
using KebabMaster.Process.Domain.Services;
using KebabMaster.Process.Infrastructure.Database;
using KebabMaster.Process.Infrastructure.Logger;
using KebabMaster.Process.Infrastructure.Repositories;
using KebabMaster.Process.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IOrderApiService, OrderApiService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IApplicationLogger, ApplicationLogger>();
builder.Services.AddTransient<IUserManagementService, UserManagementService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("Database"));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddAutoMapper(conf => conf.AddProfile<UserProfile>());
builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<OrderProfile>();  
    cfg.AddProfile<UserProfile>();  

});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = builder.Configuration["TokenData:Issuer"],
        ValidIssuer = builder.Configuration["TokenData:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenData:Secret"]))
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // SetupDatabase();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SetupDatabase()
{
    DatabaseOptions settings = builder.Configuration.GetSection("Database").Get<DatabaseOptions>();
    var database = new ApplicationDbContext(Options.Create(settings));
    database.Database.EnsureCreated();

    if (!database.MenuItems.Any())
    {

        database.MenuItems.Add(new MenuItem("Chicken Kebab", 9.99));
        database.MenuItems.Add(new MenuItem("Beef Kebab", 10.99));
        database.MenuItems.Add(new MenuItem("Lamb Kebab", 12.99));
        database.MenuItems.Add(new MenuItem("Vegetable Kebab", 8.99));
        database.MenuItems.Add(new MenuItem("Mixed Kebab", 11.99));
    }

    database.SaveChanges();
}