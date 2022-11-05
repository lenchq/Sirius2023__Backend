namespace Sirius.LibraryGraphQL.Types;

public class QueryType : ObjectType<Query.Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query.Query> descriptor)
    {
        descriptor.Name("Query");
        
        descriptor.Field(static _ => _.BookById(default!, default!))    
            .Name("bookById")
            .Argument("id",
                arg => arg.Type<NonNullType<StringType>>()
            )
            .Type<BookType>();

        descriptor.Field(static _ => _.Books(default!))
            .Name("books")
            .Type<ListType<BookType>>();

        descriptor.Field(static _ => _.AuthorById(default!, default!))
            .Name("authorById")
            .Type<AuthorType>();

        descriptor.Field(static _ => _.Rents(default!, default!, default!))
            .Name("rents")
            .Argument("readerId", static _ => _.Type<NonNullType<UuidType>>())
            .Type<ListType<RentType>>();
    }
}