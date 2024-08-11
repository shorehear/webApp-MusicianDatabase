using AutoMapper;
using MusiciansAPI.Database;

namespace MusiciansAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Musician, MusicianDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Collective, opt => opt.MapFrom(src => src.Collective))
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums));

            CreateMap<Country, CountryDto>();
            CreateMap<Collective, CollectiveDto>()
                .ForMember(dest => dest.CollectiveMembers, opt => opt.MapFrom(src => src.CollectiveMembers))
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums));
            CreateMap<Album, AlbumDto>();

            CreateMap<MusicianDto, Musician>()
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.Collective, opt => opt.Ignore())
                .ForMember(dest => dest.Albums, opt => opt.Ignore());

            CreateMap<CountryDto, Country>();
            CreateMap<CollectiveDto, Collective>()
                .ForMember(dest => dest.CollectiveMembers, opt => opt.Ignore())
                .ForMember(dest => dest.Albums, opt => opt.Ignore());
            CreateMap<AlbumDto, Album>();
        }
    }

}
