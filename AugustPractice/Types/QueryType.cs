using MusiciansAPI.Queries;

namespace MusiciansAPI.Types
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(q => q.GetMusicians(default))
                .Type<NonNullType<ListType<NonNullType<MusicianType>>>>();
            descriptor.Field(q => q.GetMusiciansByCountry(default, default))
                .Type<NonNullType<ListType<NonNullType<MusicianType>>>>();
            descriptor.Field(q => q.GetCollectives(default))
                .Type<NonNullType<ListType<NonNullType<CollectiveType>>>>();
            descriptor.Field(q => q.GetCollectiveByMusician(default, default))
                .Type<CollectiveType>();
            descriptor.Field(q => q.GetCollectivesByGenre(default, default))
                .Type<NonNullType<ListType<NonNullType<CollectiveType>>>>();
        }
    }
}
