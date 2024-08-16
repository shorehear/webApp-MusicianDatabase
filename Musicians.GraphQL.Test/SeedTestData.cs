using Musicians.Database;

namespace Musicians.Test
{
    public static class SeedTestData
    {
        public static void Initialize(MusiciansDbContext context)
        {
            context.Musicians.RemoveRange(context.Musicians);
            context.Countries.RemoveRange(context.Countries);

            var country = new Country { Id = Guid.NewGuid(), CountryName = "USA" };
            context.Countries.Add(country);

            var collective = new Collective { CollectiveName = "The Beatles", CollectiveGenre = Genre.Rock };
            context.Collectives.Add(collective);

            context.Musicians.AddRange(
                new Musician
                {
                    MusicianName = "John Lennon",
                    MusicianBirthDate = DateTime.Parse("11.01.1911"),
                    Country = country,
                    CountryId = country.Id,
                    Collective = collective,
                    CollectiveId = collective.Id
                },
                new Musician
                {
                    MusicianName = "Paul McCartney",
                    MusicianBirthDate = DateTime.Parse("10.01.1910"),
                    Country = country,
                    CountryId = country.Id,
                    Collective = collective,
                    CollectiveId = collective.Id
                }
            );

            context.SaveChanges();
        }
    }
}
