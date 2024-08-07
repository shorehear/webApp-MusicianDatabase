using MusiciansAPI.Database;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using MusiciansAPI.Queries;
using MusiciansAPI.Repositories;
using MusiciansAPI.Mutations;

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
    .AddType<Genre>();

builder.Services.AddPooledDbContextFactory<MusiciansDbContext>(m => m.UseSqlite(connectionString).LogTo(Console.WriteLine));
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
app.UseAuthorization();

app.MapGraphQL();  
app.MapRazorPages();

app.Run();
