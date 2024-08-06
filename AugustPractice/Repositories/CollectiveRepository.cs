using Microsoft.EntityFrameworkCore;
using MusiciansAPI.Database;

namespace MusiciansAPI.Repositories
{
    public class CollectivesRepository
    {
        private readonly IDbContextFactory<MusiciansDbContext> dbContextFactory;
        public CollectivesRepository(IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<Collective> GetOrCreateCollectiveAsync(string collectiveName)
        {
            using var context = dbContextFactory.CreateDbContext();

            var collective = await context.Collectives.FirstOrDefaultAsync(c => c.CollectiveName == collectiveName);

            if (collective == null)
            {
                collective = new Collective
                {
                    Id = Guid.NewGuid(),
                    CollectiveName = collectiveName
                };

                context.Collectives.Add(collective);
                await context.SaveChangesAsync();
            }

            return collective;
        }
    }
}

