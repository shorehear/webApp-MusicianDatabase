using Microsoft.EntityFrameworkCore;
using MusiciansAPI.Database;

namespace MusiciansAPI.Repositories
{
    public class CountriesRepository
    {
        private readonly IDbContextFactory<MusiciansDbContext> dbContextFactory;
        public CountriesRepository(IDbContextFactory<MusiciansDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<Country> GetOrCreateCountryAsync(string countryName)
        {
            using var context = dbContextFactory.CreateDbContext();
            var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryName == countryName);

            if (country == null)
            {
                country = new Country
                {
                    Id = Guid.NewGuid(),
                    CountryName = countryName
                };

                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            return country;
        }
    }
}
