using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MusiciansAPI.Database
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
                    new Country { Id = Guid.NewGuid(), CountryName = "Germany" }
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
                    }
                };

                context.Musicians.AddRange(musicians);

                var albums = new[]
                {
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Abbey Road",
                        NumberOfTracks = 17,
                        ReleaseYear = 1969,
                        Collective = collectives[0]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "A Night at the Opera",
                        NumberOfTracks = 12,
                        ReleaseYear = 1975,
                        Collective = collectives[1]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Nevermind",
                        NumberOfTracks = 13,
                        ReleaseYear = 1991,
                        Collective = collectives[2]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Parachutes",
                        NumberOfTracks = 10,
                        ReleaseYear = 2000,
                        Collective = collectives[3]
                    },
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        AlbumTitle = "Discovery",
                        NumberOfTracks = 14,
                        ReleaseYear = 2001,
                        Collective = collectives[4]
                    }
                };

                context.Albums.AddRange(albums);

                context.SaveChanges();

            }
        }
    }
}
