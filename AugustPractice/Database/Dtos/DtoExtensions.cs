namespace MusiciansAPI.Database
{
    public static class DtoExtensions
    {
        public static MusicianDto ToDto(this Musician musician)
        {
            return new MusicianDto
            {
                Id = musician.Id,
                MusicianName = musician.MusicianName,
                Country = musician.Country.ToDto(), 
                MusicianBirthDate = musician.MusicianBirthDate,
                MusicianDeathDate = musician.MusicianDeathDate,
                Collective = musician.Collective?.ToDto(), 
                Albums = musician.Albums?.Select(a => a.ToDto()).ToList() 
            };
        }

        public static CountryDto ToDto(this Country? country)
        {
            if (country == null)
            {
                return null;
            }

            return new CountryDto
            {
                Id = country.Id,
                CountryName = country.CountryName
            };
        }


        public static CollectiveDto ToDto(this Collective collective)
        {
            if (collective == null)
            {
                return null;
            }
            return new CollectiveDto
            {
                Id = collective.Id,
                CollectiveName = collective.CollectiveName,
                CollectiveGenre = collective.CollectiveGenre, 
                CollectiveMembers = collective.CollectiveMembers?.Select(m => m.ToDto()) ?? new List<MusicianDto>(), 
                Albums = collective.Albums?.Select(a => a.ToDto()) 
            };
        }

        public static AlbumDto ToDto(this Album album)
        {
            if (album == null)
            {
                return null;
            }
            return new AlbumDto
            {
                Id = album.Id,
                AlbumTitle = album.AlbumTitle,
                NumberOfTracks = album.NumberOfTracks,
                ReleaseYear = album.ReleaseYear,
                Collective = album.Collective?.ToDto(), 
                Musician = album.Musician?.ToDto() 
            };
        }
    }

}
