using AutoMapper;
using Musicians.Database;
using Musicians.DataAccess;

namespace Musicians.Test
{
    public class MappingProfileTest
    {
        private readonly IMapper mapper;

        public MappingProfileTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = config.CreateMapper();
        }

        [Fact]
        public void TestMusicianToMusicianDtoMapping()
        {
            var musician = new Musician
            {
                Id = Guid.NewGuid(),
                MusicianName = "Freddie Mercury",
                MusicianBirthDate = DateTime.Parse("05.09.1946"),
                Country = new Country { Id = Guid.NewGuid(), CountryName = "UK" },
                Collective = new Collective { Id = Guid.NewGuid(), CollectiveName = "Queen" },
            };

            var musicianDto = mapper.Map<MusicianDto>(musician);

            Assert.NotNull(musician);
            Assert.Equal(musicianDto.MusicianName, musician.MusicianName);
            Assert.Equal(musicianDto.MusicianBirthDate, musician.MusicianBirthDate);
        }

        [Fact]
        public void TestMusicianDtoToMusicianMapping()
        {
            var musicianDto = new MusicianDto
            {
                Id = Guid.NewGuid(),
                MusicianName = "Freddie Mercury",
                MusicianBirthDate = DateTime.Parse("05.09.1946"),
                Country = new CountryDto { Id = Guid.NewGuid(), CountryName = "UK" },
                Collective = new CollectiveDto { Id = Guid.NewGuid(), CollectiveName = "Queen" },

            };

            var musician = mapper.Map<Musician>(musicianDto);

            Assert.NotNull(musician);
            Assert.Equal(musicianDto.MusicianName, musician.MusicianName);
            Assert.Equal(musicianDto.MusicianBirthDate, musician.MusicianBirthDate);

            Assert.Null(musician.Country);
            Assert.Null(musician.Collective);
        }
    }
}
