using MusiciansAPI.Database;
namespace MusiciansAPI.Types;
public class MusicianType : ObjectType<MusicianDto>
{
    protected override void Configure(IObjectTypeDescriptor<MusicianDto> descriptor)
    {
        descriptor.Field(m => m.Id).Type<NonNullType<IdType>>();
        descriptor.Field(m => m.MusicianName).Type<NonNullType<StringType>>();
        descriptor.Field(m => m.MusicianBirthDate).Type<NonNullType<DateType>>();
        descriptor.Field(m => m.MusicianDeathDate).Type<DateType>();
        descriptor.Field(m => m.Country).Type<CountryType>();
        descriptor.Field(m => m.Collective).Type<CollectiveType>();
        descriptor.Field(m => m.Albums).Type<ListType<AlbumType>>();
    }
}