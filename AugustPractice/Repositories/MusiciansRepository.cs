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

        public async Task<Musician> GetByIdAsync(Guid id)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                return await context.Musicians.FindAsync(id);
            }
        }

        public async Task<Musician> GetMusicianByNameAsync(string name)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                return await context.Musicians.FirstOrDefaultAsync(m => m.MusicianName == name);
            }
        }

        public async Task<bool> DeleteAlbumAsync(Guid albumId)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                var album = await context.Albums.FindAsync(albumId);

                if (album == null)
                {
                    return false;
                }

                context.Albums.Remove(album);
                return await context.SaveChangesAsync() > 0;
            }

        }
    }
}
