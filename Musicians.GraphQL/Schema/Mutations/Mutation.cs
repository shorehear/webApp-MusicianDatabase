using AutoMapper;
using HotChocolate.Authorization;
using Musicians.DataAccess;
using Musicians.Database;


namespace Musicians.GraphQL
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

        //public async Task<User> RegisterUser([Service] MusiciansDbContext context, string username, string password)
        //{
        //    var user = new User
        //    {
        //        Username = username,
        //        Password = BCrypt.Net.BCrypt.HashPassword(password)
        //    };

        //    context.Users.Add(user);
        //    await context.SaveChangesAsync();

        //    return user;
        //}

        //public async Task<string> Login([Service] MusiciansDbContext context, string username, string password)
        //{
        //    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        //    {
        //        throw new Exception("Invalid credentials");
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes("yoursecretkey");
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //        new Claim(ClaimTypes.Name, user.Username)
        //    }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        Issuer = "yourissuer",
        //        Audience = "youraudience",
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        #region create musician
        //[Authorize(Policy = "AuthenticatedUser")]
        //[Authorize]
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
        //[Authorize]
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
        //[Authorize]
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
