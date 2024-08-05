using MusiciansAPI;
using Microsoft.EntityFrameworkCore;
using MusiciansAPI.Database;

namespace MusiciansAPI.Repositories
{
    public class MusiciansRepository
    {
        private readonly IDbContextFactory<MusiciansDbContext> dbContextFactory;

        public MusiciansRepository(IDbContextFactory<MusiciansDbContext> dbContextFactory) 
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<Musician> Create(Musician musician)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                context.Musicians.Add(musician);
                await context.SaveChangesAsync();
            
                return musician;
            }
        }

        public async Task<Musician> Update(Musician musician)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                context.Musicians.Update(musician);
                await context.SaveChangesAsync();

                return musician;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                Musician musician = new Musician() { Id = id };
                context.Musicians.Remove(musician);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
