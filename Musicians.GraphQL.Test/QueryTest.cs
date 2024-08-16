using AutoMapper;
using CookieCrumble;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Musicians.Database;
using Newtonsoft.Json.Linq;

namespace Musicians.Test
{ 
    public class QueryTest : IClassFixture<TestService>
    {
        private readonly IServiceProvider serviceProvider;

        public QueryTest(TestService testService) 
        {
            serviceProvider = testService.ServiceProvider;
        }

        [Fact]
        public async Task TestGetMusicians()
        {
            using var scope = serviceProvider.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MusiciansDbContext>>();
            using var context = contextFactory.CreateDbContext();

            SeedTestData.Initialize(context);

            var executor = await scope.ServiceProvider.GetRequiredService<IRequestExecutorResolver>()
                .GetRequestExecutorAsync();

            var result = await executor.ExecuteAsync("{ musicians { musicianName } }");

            result.MatchSnapshot();
        }

        [Fact]
        public async Task TestGetMusiciansByCountry()
        {
            using var scope = serviceProvider.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MusiciansDbContext>>();
            using var context = contextFactory.CreateDbContext();

            SeedTestData.Initialize(context);

            var executor = await scope.ServiceProvider.GetRequiredService<IRequestExecutorResolver>()
                .GetRequestExecutorAsync();

            var result = await executor.ExecuteAsync(@"
                {
                    musiciansByCountry(countryName: ""USA"") {
                        musicianName
                        country {
                            countryName
                        }
                    }
                }");

            result.MatchSnapshot();

            var resultData = JObject.Parse(result.ToJson());

            var musicians = resultData["data"]?["musiciansByCountry"];
            Assert.NotNull(musicians);
            Assert.NotEmpty(musicians);

        }
    }
}
