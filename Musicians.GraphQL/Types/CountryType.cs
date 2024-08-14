using Musicians.DataAccess;
using HotChocolate.Types;

namespace Musicians.GraphQL
{
    public class CountryType : ObjectType<CountryDto>
    {
        protected override void Configure(IObjectTypeDescriptor<CountryDto> descriptor)
        {
            descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
            descriptor.Field(c => c.CountryName).Type<NonNullType<StringType>>();
        }
    }
}
