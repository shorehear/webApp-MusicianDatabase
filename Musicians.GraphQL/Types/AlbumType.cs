using HotChocolate.Types;
using Musicians.DataAccess;

namespace Musicians.GraphQL
{
    public class AlbumType : ObjectType<AlbumDto>
    {
        protected override void Configure(IObjectTypeDescriptor<AlbumDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<NonNullType<IdType>>();
            descriptor.Field(a => a.AlbumTitle).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.Collective).Type<CollectiveType>();
            descriptor.Field(a => a.Musician).Type<MusicianType>();
        }
    }
}
