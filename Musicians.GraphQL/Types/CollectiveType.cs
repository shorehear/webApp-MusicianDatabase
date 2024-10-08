﻿using Musicians.DataAccess;
using HotChocolate.Types;

namespace Musicians.GraphQL
{
    public class CollectiveType : ObjectType<CollectiveDto>
    {
        protected override void Configure(IObjectTypeDescriptor<CollectiveDto> descriptor)
        {
            descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
            descriptor.Field(c => c.CollectiveName).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.CollectiveGenre).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.CollectiveMembers).Type<ListType<MusicianType>>();
            descriptor.Field(c => c.Albums).Type<ListType<AlbumType>>();
        }
    }
}
