using MusiciansAPI.Database;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using MusiciansAPI.Queries;
using MusiciansAPI.Repositories;
using MusiciansAPI.Mutations;
using MusiciansAPI.Mapper;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("default");
Batteries.Init();

builder.Services
    .AddGraphQLServer()
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


builder.Services.AddScoped<MusiciansRepository>();
builder.Services.AddScoped<CollectivesRepository>();
builder.Services.AddScoped<CountriesRepository>();

builder.Services.AddRazorPages();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
//app.UseAuthorization();

app.MapGraphQL();  
app.MapRazorPages();

app.Run();
