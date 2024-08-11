using AutoMapper;
using MusiciansAPI.Database;
using MusiciansAPI.Repositories;

namespace MusiciansAPI.Mutations
{
    public class Mutation
    {
        private readonly IMapper mapper;
        private readonly MusiciansRepository musiciansRepository;
        private readonly CountriesRepository countriesRepository;
        private readonly CollectivesRepository collectivesRepository;
        public Mutation(IMapper mapper, MusiciansRepository musiciansRepository, CountriesRepository countriesRepository, CollectivesRepository collectivesRepository) 
        { 
            this.mapper = mapper;
            this.musiciansRepository = musiciansRepository; 
            this.countriesRepository = countriesRepository;
            this.collectivesRepository = collectivesRepository;
        }

        #region create musician
        public async Task<MusicianDto> CreateMusician
        (
            string musicianName,
            string countryName,
            DateTime musicianBirthDate,
            DateTime? musicianDeathDate = null,
            string? collectiveName = null
        )
        {
            var musicianId = Guid.NewGuid();

            var country = await countriesRepository.GetOrCreateCountryAsync(countryName);
            if (country == null)
            {
                throw new Exception("Не удалось найти или создать страну");
            }

            Collective? collective = null;
            if (!string.IsNullOrEmpty(collectiveName))
            {
                collective = await collectivesRepository.GetOrCreateCollectiveAsync(collectiveName);
            }

            Musician musician = new Musician()
            {
                Id = musicianId,
                MusicianName = musicianName,
                CountryId = country.Id,
                MusicianBirthDate = musicianBirthDate,
                MusicianDeathDate = musicianDeathDate,
                CollectiveId = collective?.Id
            };

            var createdMusician = await musiciansRepository.Create(musician);

            var musicianDto = mapper.Map<MusicianDto>(createdMusician);

            if (musicianDto.Country == null)
            {
                musicianDto.Country = mapper.Map<CountryDto>(country);
            }
            if (musicianDto.Collective == null && collective != null)
            {
                musicianDto.Collective = mapper.Map<CollectiveDto>(collective);
            }

            return musicianDto;
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

            return mapper.Map<MusicianDto>(updatedMusician);
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

            if (musician.Albums != null && musician.Albums.Any())
            {
                foreach (var album in musician.Albums)
                {
                    await musiciansRepository.DeleteAlbumAsync(album.Id);
                }
            }

            return await musiciansRepository.Delete(musician.Id);
        }

        #endregion

    }
}
