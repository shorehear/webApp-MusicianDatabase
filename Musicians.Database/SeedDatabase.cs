using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Metrics;

namespace Musicians.Database
{
    public static class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (MusiciansDbContext context = new MusiciansDbContext(serviceProvider.GetRequiredService<DbContextOptions<MusiciansDbContext>>()))
            {
                if (context.Countries.Any()) { return; }

                var countries = new[]
                {
                    new Country { Id = Guid.NewGuid(), CountryName = "USA" },
                    new Country { Id = Guid.NewGuid(), CountryName = "UK" },
                    new Country { Id = Guid.NewGuid(), CountryName = "Canada" },
                    new Country { Id = Guid.NewGuid(), CountryName = "Australia" },
                    new Country { Id = Guid.NewGuid(), CountryName = "Germany" },
                    new Country { Id = Guid.NewGuid(), CountryName = "France" },
                    new Country { Id = Guid.NewGuid(), CountryName = "Japan" },
                    new Country { Id = Guid.NewGuid(), CountryName = "Sweden" },
                    new Country { Id = Guid.NewGuid(), CountryName = "Brazil" },
                    new Country { Id = Guid.NewGuid(), CountryName = "South Korea" }
                };
                context.Countries.AddRange(countries);

                var collectives = new[]
                {
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "The Beatles",
                        CollectiveGenre = Genre.Rock
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "Queen",
                        CollectiveGenre = Genre.Rock
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "Nirvana",
                        CollectiveGenre = Genre.Rock
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "Coldplay",
                        CollectiveGenre = Genre.Pop
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "Daft Punk",
                        CollectiveGenre = Genre.Electronic
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "ABBA",
                        CollectiveGenre = Genre.Pop
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "Pink Floyd",
                        CollectiveGenre = Genre.Rock
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "Led Zeppelin",
                        CollectiveGenre = Genre.Rock
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "The Rolling Stones",
                        CollectiveGenre = Genre.Rock
                    },
                    new Collective
                    {
                        Id = Guid.NewGuid(),
                        CollectiveName = "BTS",
                        CollectiveGenre = Genre.Pop
                    }
                };
                context.Collectives.AddRange(collectives);

                var musicians = new[]
                {
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "John Lennon",
                        MusicianBirthDate = new DateTime(1940, 10, 9),
                        MusicianDeathDate = new DateTime(1980, 12, 8),
                        Country = countries[1],
                        Collective = collectives[0]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Paul McCartney",
                        MusicianBirthDate = new DateTime(1942, 6, 18),
                        Country = countries[1],
                        Collective = collectives[0]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Freddie Mercury",
                        MusicianBirthDate = new DateTime(1946, 9, 5),
                        MusicianDeathDate = new DateTime(1991, 11, 24),
                        Country = countries[1],
                        Collective = collectives[1]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Kurt Cobain",
                        MusicianBirthDate = new DateTime(1967, 2, 20),
                        MusicianDeathDate = new DateTime(1994, 4, 5),
                        Country = countries[0],
                        Collective = collectives[2]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Chris Martin",
                        MusicianBirthDate = new DateTime(1977, 3, 2),
                        Country = countries[1],
                        Collective = collectives[3]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Thomas Bangalter",
                        MusicianBirthDate = new DateTime(1975, 1, 3),
                        Country = countries[4],
                        Collective = collectives[4]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Benny Andersson",
                        MusicianBirthDate = new DateTime(1946, 12, 16),
                        Country = countries[7],
                        Collective = collectives[5]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "David Gilmour",
                        MusicianBirthDate = new DateTime(1946, 3, 6),
                        Country = countries[1],
                        Collective = collectives[6]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Robert Plant",
                        MusicianBirthDate = new DateTime(1948, 8, 20),
                        Country = countries[1],
                        Collective = collectives[7]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Mick Jagger",
                        MusicianBirthDate = new DateTime(1943, 7, 26),
                        Country = countries[1],
                        Collective = collectives[8]
                    },
                    new Musician
                    {
                        Id = Guid.NewGuid(),
                        MusicianName = "Kim Nam-joon",
                        MusicianBirthDate = new DateTime(1994, 9, 12),
                        Country = countries[9],
                        Collective = collectives[9]
                    }
                };
                context.Musicians.AddRange(musicians);

                var albums = new[]
                {
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Abbey Road",
                        Collective = collectives[0]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "A Night at the Opera",
                        Collective = collectives[1]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Nevermind",
                        Collective = collectives[2]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Parachutes",
                        Collective = collectives[3]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Discovery",
                        Collective = collectives[4]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Voulez-Vous",
                        Collective = collectives[5]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "The Dark Side of the Moon",
                        Collective = collectives[6]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Led Zeppelin IV",
                        Collective = collectives[7]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Sticky Fingers",
                        Collective = collectives[8]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Map of the Soul: 7",
                        Collective = collectives[9]
                    }
                };

                context.Albums.AddRange(albums);

                context.SaveChanges();
            }
        }
    }
}
