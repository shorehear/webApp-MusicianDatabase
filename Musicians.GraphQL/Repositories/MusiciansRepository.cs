using Microsoft.EntityFrameworkCore;
using Musicians.Database;

namespace Musicians.GraphQL
{
    public interface IMusiciansRepository
    {
        Task<Musician> Create(Musician musician);
        Task<Musician> Update(Musician musician);
        Task<bool> Delete(Guid id);
        Task<Musician> GetByIdAsync(Guid id);
        Task<Musician> GetMusicianByNameAsync(string name);
        Task<bool> DeleteAlbumAsync(Guid albumId);
    }

    public class MusiciansRepository : IMusiciansRepository
    {
        private readonly IDbContextFactory<MusiciansDbContext> dbContextFactory;

        public MusiciansRepository(IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public virtual async Task<Musician> Create(Musician musician)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                context.Musicians.Add(musician);
                await context.SaveChangesAsync();

                return musician;
            }
        }
        public virtual async Task<Musician> Update(Musician musician)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                context.Musicians.Update(musician);
                await context.SaveChangesAsync();

                return musician;
            }
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                Musician musician = new Musician() { Id = id };
                context.Musicians.Remove(musician);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public virtual async Task<Musician> GetByIdAsync(Guid id)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                return await context.Musicians.FindAsync(id);
            }
        }

        public virtual async Task<Musician> GetMusicianByNameAsync(string name)
        {
            using (MusiciansDbContext context = dbContextFactory.CreateDbContext())
            {
                return await context.Musicians.FirstOrDefaultAsync(m => m.MusicianName == name);
            }
        }

        public virtual async Task<bool> DeleteAlbumAsync(Guid albumId)
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
