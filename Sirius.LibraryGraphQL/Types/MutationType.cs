namespace Sirius.LibraryGraphQL.Types;

public class MutationType : ObjectType<Mutation.Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation.Mutation> descriptor)
    {
        descriptor.Name("Mutation");
//        
// /* BOOK --------------*/
//
         descriptor.Field(_ => _.AddBookAsync(default!, default!, default!))
             .Name("addBook")
             .Argument("input", _ => _.Type<AddBookInputType>())
             .Type<BookType>()
             .UseMutationConvention();
         descriptor.Field(_ => _.UpdateBookAsync(default!, default!))
             .Name("updateBook")
             .Argument("input", static _ => _.Type<UpdateBookInputType>())
             .Type<BookType>();
         descriptor.Field(_ => _.DeleteBookAsync(default!, default!))
             .Name("deleteBook")
             .Argument("input",
                 _ => _.Type<NonNullType<UuidType>>())
             .Type<BookType>();
//         
// /* READER --------------*/
//
         descriptor.Field(_ => _.AddReaderAsync(default!, default!))
             .Name("addReader")
             .Argument("input", _ => _.Type<NonNullType<AddReaderInputType>>())
             .Type<ReaderType>();
         descriptor.Field( _ => _.UpdateReaderAsync(default!, default!))
             .Name("updateReader")
             .Argument("input", static _ => _.Type<UpdateReaderInputType>())
             .Type<ReaderType>();
         descriptor.Field( _ => _.DeleteReaderAsync(default!, default!))
             .Name("deleteReader")
             .Argument("input",
                 _ => _.Type<NonNullType<UuidType>>())
             .Type<ReaderType>();
//
// /* AUTHOR --------------*/
//
        descriptor.Field( _ => _.AddAuthorAsync(default!, default!))
            .Name("addAuthor")
            .Argument("input", static _ => _.Type<AddAuthorInputType>())
            .Type<AuthorType>();
        descriptor.Field( _ => _.UpdateAuthorAsync(default!, default!))
            .Name("updateAuthor")
            .Argument("input", static _ => _.Type<UpdateAuthorInputType>())
            .Type<AuthorType>();
        descriptor.Field( _ => _.DeleteAuthorAsync(default!, default!))
            .Name("deleteAuthor")
            .Argument("input",
                _ => _.Type<NonNullType<UuidType>>())
            .Type<AuthorType>();
        
//
// /* RENT --------------*/
//
        descriptor.Field(_ => _.RentBookAsync(default!, default!, default!, default!))
            .Name("rentBook")
            .Argument("input",
                static _ => _.Type<RentBookInputType>())
            .Type<RentType>();
        descriptor.Field(_ => _.ReturnBookAsync(default!, default!, default!, default!))
            .Name("returnBook")
            .Argument("input",
                static _ => _.Type<ReturnBookInputType>())
            .Type<RentType>();
    }
}