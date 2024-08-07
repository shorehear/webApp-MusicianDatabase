using MusiciansAPI.Database;
using MusiciansAPI.Repositories;

namespace MusiciansAPI.Mutations
{
    public class Mutation
    {
        private readonly MusiciansRepository musiciansRepository;
        private readonly CountriesRepository countriesRepository;
        private readonly CollectivesRepository collectivesRepository;
        public Mutation(MusiciansRepository musiciansRepository, CountriesRepository countriesRepository, CollectivesRepository collectivesRepository) 
        { 
            this.musiciansRepository = musiciansRepository; 
            this.countriesRepository = countriesRepository;
            this.collectivesRepository = collectivesRepository;
        }

        #region create musician
        public async Task<MusicianDto> CreateMusician(
            string musicianName,
            DateTime musicianBirthDate,
            DateTime? musicianDeathDate,
            string countryName,
            string? collectiveName = null)
        {
            var musicianId = Guid.NewGuid();
            var country = await countriesRepository.GetOrCreateCountryAsync(countryName);

            Collective? collective = null;
            if (!string.IsNullOrEmpty(collectiveName))
            {
                collective = await collectivesRepository.GetOrCreateCollectiveAsync(collectiveName);
            }

            var musician = new Musician
            {
                Id = musicianId,
                MusicianName = musicianName,
                MusicianBirthDate = musicianBirthDate,
                MusicianDeathDate = musicianDeathDate,
                CountryId = country.Id,
                CollectiveId = collective?.Id
            };

            var createdMusician = await musiciansRepository.Create(musician);

            return createdMusician.ToDto();
        }
        #endregion

        #region update musician main parameters
        public async Task<MusicianDto> UpdateMusician(
            Guid? musicianId = null,
            string? musicianName = null,
            string? newMusicianName = null,
            DateTime? musicianBirthDate = null,
            DateTime? musicianDeathDate = null,
            string? countryName = null,
            string? collectiveName = null)
        {
            Musician musician = musicianId switch
            {
                Guid id => await musiciansRepository.GetByIdAsync(id),
                _ when !string.IsNullOrEmpty(musicianName) => await musiciansRepository.GetMusicianByNameAsync(musicianName),
                _ => throw new Exception("Musician not found with this id.")
            };

            musician.MusicianName = newMusicianName ?? musician.MusicianName;
            musician.MusicianBirthDate = musicianBirthDate ?? musician.MusicianBirthDate;
            musician.MusicianDeathDate = musicianDeathDate ?? musician.MusicianDeathDate;

            if (!string.IsNullOrEmpty(countryName))
            {
                var country = await countriesRepository.GetOrCreateCountryAsync(countryName);
                musician.CountryId = country.Id;
            }

            if (!string.IsNullOrEmpty(collectiveName))
            {
                var collective = await collectivesRepository.GetOrCreateCollectiveAsync(collectiveName);
                musician.CollectiveId = collective?.Id;
            }

            var updatedMusician = await musiciansRepository.Update(musician);

            return updatedMusician.ToDto();
        }
        #endregion

        #region delete musician and related albums
        public async Task<bool> DeleteMusician(
            string? musicianName = null, 
            Guid? musicianId = null)
        {
            Musician? musician = null;

            if (musicianName != null)
            {
                musician = await musiciansRepository.GetMusicianByNameAsync(musicianName);
            }
            else if (musicianId.HasValue)
            {
                musician = await musiciansRepository.GetByIdAsync(musicianId.Value);
            }
            else
            {
                throw new Exception("To delete musician you must specify his id or full name.");
            }

            if (musician == null)
            {
                throw new Exception("Musician not found.");
            }

            if(musician.Albums != null && musician.Albums.Any())
            {
                foreach(var album in musician.Albums)
                {
                    await musiciansRepository.DeleteAlbumAsync(album.Id);
                }
            }
            

            bool isDeleted = await musiciansRepository.Delete(musician.Id);
            return isDeleted;
        }
        #endregion
        //public async Task<CollectiveDto> CreateCollective() { return null; }

        #region add album data to collective/musician
        //public async Task<AlbumDto> AddAlbum(
        //    string albumTitle,
        //    int numberOfTracks,
        //    int releaseYear,
        //    string? collectiveName = null,
        //    string? musicianName = null,
        //    Guid? collectiveId = null,
        //    Guid? musicianId = null)
        //{
        //    Musician? musician = null;
        //    Collective? collective = null;

        //    if that is musician
        //    if (musicianId.HasValue)
        //        {
        //            musician = await musiciansRepository.GetByIdAsync(musicianId.Value);
        //        }
        //        else if (!string.IsNullOrEmpty(musicianName))
        //        {
        //            musician = await musiciansRepository.GetMusicianByNameAsync(musicianName);
        //        }

        //    if that is collective
        //    if (collectiveId.HasValue)
        //        {
        //            collective = await collectivesRepository.
        //    }
        //    if (musician == null && collective == null)
        //    {
        //        throw new Exception("You must provide musician or collective to associate with album.");
        //    }

        //    var album = new Album
        //    {
        //        Id = Guid.NewGuid(),
        //        AlbumTitle = albumTitle,
        //        NumberOfTracks = numberOfTracks,
        //        ReleaseYear = releaseYear,
        //        Musician = musician,
        //        Collective = collective
        //    };

        //    var addedAlbum = await musiciansRepository.AddAlbumAsync(album);
        //    return new
        //}
        #endregion
    }
}
