using MusiciansAPI.Database;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace MusiciansAPI.Queries
{
    public class Query
    {
        private readonly IMapper mapper;
        public Query(IMapper mapper)
        {
            this.mapper = mapper;
        }

        #region get all musicians
        [UseSorting]
        public async Task<IEnumerable<MusicianDto>> GetMusicians([Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            using var context = dbContextFactory.CreateDbContext();
            return await context.Musicians
                .Include(m => m.Country)
                .Include(m => m.Collective)
                .Include(m => m.Albums)
                .ProjectTo<MusicianDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [UseDbContext(typeof(MusiciansDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [HotChocolate.Data.UseFiltering]
        [UseSorting]
        public IQueryable<MusicianDto> GetPaginatedMusicians([ScopedService] MusiciansDbContext context)
        {
            //var context = dbContextFactory.CreateDbContext();
            return context.Musicians
                .Include(m => m.Country)
                .Include(m => m.Collective)
                .Include(m => m.Albums)
                .ProjectTo<MusicianDto>(mapper.ConfigurationProvider);
        }
        #endregion

        #region get musicians by country
        public IQueryable<MusicianDto> GetMusiciansByCountry(string countryName, [Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            var context = dbContextFactory.CreateDbContext();
            return context.Musicians
                .Where(m => m.Country.CountryName == countryName)
                .Include(m => m.Country)
                .Include(m => m.Collective)
                .Include(m => m.Albums)
                .ProjectTo<MusicianDto>(mapper.ConfigurationProvider);
        }
        #endregion

        #region get collectives
        public async Task<IEnumerable<CollectiveDto>> GetCollectives([Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            using var context = dbContextFactory.CreateDbContext();

            return await context.Collectives
                .Include(c => c.CollectiveMembers)
                    .ThenInclude(m => m.Country)
                .Include(c => c.Albums)
                .ProjectTo<CollectiveDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }


        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [HotChocolate.Data.UseFiltering]
        [UseSorting]
        public IQueryable<CollectiveDto> GetPaginatedCollectives([ScopedService] MusiciansDbContext context)
        {
            //var context = dbContextFactory.CreateDbContext();
            return context.Collectives
                .Include(c => c.CollectiveMembers)
                    .ThenInclude(m => m.Country)
                .ProjectTo<CollectiveDto>(mapper.ConfigurationProvider);
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

            return mapper.Map<CollectiveDto>(musician.Collective);
        }
        #endregion

        #region get collectives by genre
        public IQueryable<CollectiveDto> GetCollectivesByGenre(string genre, [Service] IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            var context = dbContextFactory.CreateDbContext();
            if (!Enum.TryParse<Genre>(genre, ignoreCase: true, out var parsedGenre))
            {
                return Enumerable.Empty<CollectiveDto>().AsQueryable();
            }

            return context.Collectives
                .Where(c => c.CollectiveGenre == parsedGenre)
                .Include(c => c.CollectiveMembers)
                    .ThenInclude(m => m.Country)
                .Include(c => c.Albums)
                .ProjectTo<CollectiveDto>(mapper.ConfigurationProvider);
        }

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [HotChocolate.Data.UseFiltering]
        [UseSorting]
        public IQueryable<CollectiveDto> GetPaginatedCollectiveByGenre(string genre, [ScopedService] MusiciansDbContext context)
        {
            //var context = dbContextFactory.CreateDbContext();
            if (!Enum.TryParse<Genre>(genre, ignoreCase: true, out var parsedGenre))
            {
                return Enumerable.Empty<CollectiveDto>().AsQueryable();
            }

            return context.Collectives
                .Where(c => c.CollectiveGenre == parsedGenre)
                .Include(c => c.CollectiveMembers)
                    .ThenInclude(m => m.Country)
                .Include(c => c.Albums)
                .ProjectTo<CollectiveDto>(mapper.ConfigurationProvider);
        }
        #endregion
    }
}
