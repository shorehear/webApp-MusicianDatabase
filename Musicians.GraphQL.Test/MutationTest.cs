using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Musicians.Database;
using CookieCrumble;

namespace Musicians.Test
{
    public class MutationTest : IClassFixture<TestService>
    {
        private readonly IServiceProvider serviceProvider;

        public MutationTest(TestService testService)
        {
            serviceProvider = testService.ServiceProvider;
        }

        [Fact]
        public async Task TestCreateMusician()
        {
            using var scope = serviceProvider.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MusiciansDbContext>>();
            using var context = contextFactory.CreateDbContext();

            SeedTestData.Initialize(context);

            var executor = await scope.ServiceProvider.GetRequiredService<IRequestExecutorResolver>()
                .GetRequestExecutorAsync();

            var mutation = ("""
                mutation {
                  createMusician(musicianName: "Freddie Mercury",
                   musicianBirthDate: "05.09.1946",
                    countryName: "UK") {
                    id
                    musicianBirthDate
                    musicianName
                  }
                }
                """);

            var result = await executor.ExecuteAsync(mutation);

            result.MatchSnapshot();
        }

        [Fact]
        public async Task TestRegisterAndLoginUser()
        {
            using var scope = serviceProvider.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MusiciansDbContext>>();
            using var context = contextFactory.CreateDbContext();

            SeedTestData.Initialize(context);

            var executor = await scope.ServiceProvider.GetRequiredService<IRequestExecutorResolver>()
                .GetRequestExecutorAsync();

            var registerMutation = """
                mutation {
                  registerUser(username: "testuser", password: "password123") {
                    id
                    username
                  }
                }
                """;

            var registerResult = await executor.ExecuteAsync(registerMutation);
            registerResult.MatchSnapshot("RegisterUserSnapshot");

            var loginMutation = """
            mutation {
              login(username: "testuser", password: "password123") 
            }
            """;

            var loginResult = await executor.ExecuteAsync(loginMutation);
            loginResult.MatchSnapshot("LoginUserSnapshot");
        }
    }
}
