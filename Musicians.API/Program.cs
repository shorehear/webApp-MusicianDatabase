using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Musicians.Database;
using Musicians.DataAccess;
using Musicians.GraphQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Musicians.Security;

var builder = WebApplication.CreateBuilder(args);
Batteries.Init();

var connectionString = builder.Configuration.GetConnectionString("default");
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<MusicianDto>()
    .AddType<CollectiveDto>()
    .AddType<CountryDto>()
    .AddType<AlbumDto>()
    .AddType<Genre>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwt, Jwt>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddPooledDbContextFactory<MusiciansDbContext>(options =>
    options.UseSqlite(connectionString)
           .LogTo(sql =>
           {
               Console.ForegroundColor = ConsoleColor.Magenta;
               Console.WriteLine(sql);
               Console.ResetColor();
           }, LogLevel.Information)
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors());


builder.Services.AddScoped<IMusiciansRepository, MusiciansRepository>();
builder.Services.AddScoped<ICollectivesRepository, CollectivesRepository>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedDatabase.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();
app.Run();
