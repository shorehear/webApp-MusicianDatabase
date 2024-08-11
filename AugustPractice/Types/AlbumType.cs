using MusiciansAPI.Database;

namespace MusiciansAPI.Types
{
    public class AlbumType : ObjectType<AlbumDto>
    {
        protected override void Configure(IObjectTypeDescriptor<AlbumDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<NonNullType<IdType>>();
            descriptor.Field(a => a.AlbumTitle).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.NumberOfTracks).Type<NonNullType<IntType>>();
            descriptor.Field(a => a.ReleaseYear).Type<NonNullType<IntType>>();
            descriptor.Field(a => a.Collective).Type<CollectiveType>();
            descriptor.Field(a => a.Musician).Type<MusicianType>();
        }
    }
}
