using MusiciansAPI.Database;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HotChocolate;

namespace MusiciansAPI.Queries
{
    public class Query
    {
        #region get all musicians
        public async Task<IEnumerable<MusicianDto>> GetMusicians([Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            using var context = dbContextFactory.CreateDbContext();
            var musicians = await context.Musicians
                .Include(m => m.Country)
                .Include(m => m.Collective)
                .Include(m => m.Albums)
                .ToListAsync();

            return musicians.Select(m => m.ToDto());
        }
        #endregion

        #region get musicians by country
        public async Task<IEnumerable<MusicianDto>> GetMusiciansByCountry(string countryName, [Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            using var context = dbContextFactory.CreateDbContext();
            var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryName == countryName);

            if (country == null)
            {
                return new List<MusicianDto>();
            }

            var musicians = await context.Musicians
                .Where(m => m.CountryId == country.Id)
                .Include(m => m.Country)
                .Include(m => m.Collective)
                .Include(m => m.Albums)
                .ToListAsync();

            return musicians.Select(m => m.ToDto());
        }
        #endregion

        #region get all collectives
        public async Task<IEnumerable<CollectiveDto>> GetCollectives([Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            using var context = dbContextFactory.CreateDbContext();

            var collectives = await context.Collectives
                .Include(c => c.CollectiveMembers)
                            .ThenInclude(m => m.Country)
                .Include(c => c.Albums) 
                .ToListAsync();

            return collectives.Select(c => c.ToDto());
        }
        #endregion

        #region get collective by musician
        public async Task<CollectiveDto?> GetCollectiveByMusician(string musicianName, [Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            using var context = dbContextFactory.CreateDbContext();

            var musician = await context.Musicians
                .Include(m => m.Collective) 
                .ThenInclude(c => c.CollectiveMembers) 
                .Include(m => m.Collective)
                    .ThenInclude(c => c.Albums) 
                .FirstOrDefaultAsync(m => m.MusicianName == musicianName);

            if (musician?.Collective == null)
            {
                return null;
            }

            return musician.Collective.ToDto();
        }

        #endregion

        #region get collectives by genre
        public async Task<IEnumerable<CollectiveDto>> GetCollectivesByGenre(string genre, [Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            using var context = dbContextFactory.CreateDbContext();

            if (!Enum.TryParse<Genre>(genre, ignoreCase: true, out var parsedGenre))
            {
                return Enumerable.Empty<CollectiveDto>();
            }

            var collectives = await context.Collectives
                .Where(c => c.CollectiveGenre == parsedGenre)
                .Include(c => c.CollectiveMembers)
                            .ThenInclude(m => m.Country)
                .Include(c => c.Albums)
                .ToListAsync();

            return collectives.Select(c => c.ToDto());
        }
        #endregion
    }
}
