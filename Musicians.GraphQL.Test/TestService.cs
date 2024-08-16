using Musicians.Database;
using Musicians.GraphQL;
using HotChocolate;
using Musicians.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Musicians.Security;

namespace Musicians.Test
{
    public class TestService
    {
        public IServiceProvider ServiceProvider { get; }

        public TestService()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddPooledDbContextFactory<MusiciansDbContext>(options => options.UseInMemoryDatabase("TestDb"));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IMusiciansRepository, MusiciansRepository>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<ICollectivesRepository, CollectivesRepository>();

            services.AddSingleton<IJwt, MockJwt>();

            services.AddGraphQLServer()
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

            ServiceProvider = services.BuildServiceProvider();
        }
    }

    public class MockJwt : IJwt
    {
        public string GenerateJwtToken(string username)
        {
            return "test-token";
        }
    }
}
