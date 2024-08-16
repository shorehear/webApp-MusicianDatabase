using Microsoft.EntityFrameworkCore;
using Musicians.Database;

namespace Musicians.Tests
{
    public class DatabaseTests
    {
        public static Guid NotDefinedCountryId { get; } = new Guid("00000000-0000-0000-0000-000000000000");

        private readonly DbContextOptions<MusiciansDbContext> options;
        private readonly MusiciansDbContext context;

        public DatabaseTests()
        {
            options = new DbContextOptionsBuilder<MusiciansDbContext>()
                           .UseSqlite("DataSource=:memory:")
                           .Options;
            context = new MusiciansDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            FillDatabase(context);
        }

        private void PreparationToFill(MusiciansDbContext context)
        {
            context.Countries.RemoveRange(context.Countries);
            context.Musicians.RemoveRange(context.Musicians);
            context.Collectives.RemoveRange(context.Collectives);
            context.Albums.RemoveRange(context.Albums);
            context.SaveChanges();
        }

        private void FillDatabase(MusiciansDbContext context)
        {
            PreparationToFill(context);

            var country = new Country { Id = NotDefinedCountryId, CountryName = "TestCountry" };
            context.Countries.Add(country);
            context.SaveChanges();

            var musician = new Musician 
            { 
                Id = Guid.NewGuid(), 
                CountryId = NotDefinedCountryId, 
                Country = country,
                MusicianBirthDate = DateTime.Parse("20.05.1990"), 
                MusicianName = "TestMusician" 
            };
            context.Musicians.Add(musician);
            context.SaveChanges();

            var collective = new Collective 
            { 
                Id = Guid.NewGuid(), 
                CollectiveGenre = Genre.Multy, 
                CollectiveName = "TestCollective"
            };
            context.Collectives.Add(collective);
            context.SaveChanges();

            var album = new Album
            {
                Id = Guid.NewGuid(),
                AlbumTitle = "TestAlbum",
                CollectiveId = collective.Id,
                Collective = collective
            };
            context.Albums.Add(album);
            context.SaveChanges();
        }

        [Fact]
        public void TestMusicianCount()
        {
            var musiciansCount = context.Musicians.Select(m => m.Id).Count();
            Assert.Equal(1, musiciansCount);
        }

        [Fact]
        public void TestMusicianCountry()
        {
            var musician = context.Musicians.Include(m => m.Country).FirstOrDefault();
            Assert.NotNull(musician);
            Assert.Equal("TestCountry", musician.Country.CountryName);
        }

        [Fact]
        public void TestAddMultipleMusicians()
        {
            var country = context.Countries.First();
            var musician1 = new Musician 
            { 
                CountryId = country.Id,
                Country = country,
                Id = Guid.NewGuid(), 
                MusicianBirthDate = DateTime.Parse("01.01.1991"),
                MusicianName = "Musician1" 
            };
            var musician2 = new Musician 
            { 
                CountryId = country.Id, 
                Country = country,
                Id = Guid.NewGuid(), 
                MusicianBirthDate = DateTime.Parse("02.02.1992"),
                MusicianName = "Musician2" };

            context.Musicians.AddRange(musician1, musician2);
            context.SaveChanges();

            var musiciansCount = context.Musicians.Count();
            Assert.Equal(3, musiciansCount);
        }

        [Fact]
        public void TestDeleteMusician()
        {
            var musician = context.Musicians.FirstOrDefault();
            Assert.NotNull(musician);

            context.Musicians.Remove(musician);
            context.SaveChanges();

            var musiciansCount = context.Musicians.Count();
            Assert.Equal(0, musiciansCount);
        }
    }
}