using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.GraphiQL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Sirius.LibraryGraphQL.Database;
using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;
using Sirius.LibraryGraphQL.Repository;
using Sirius.LibraryGraphQL.Types;

// using Sirius.LibraryGraphQL.Database;
// using Sirius.LibraryGraphQL.Interfaces;
// using Sirius.LibraryGraphQL.Model;
// using Sirius.LibraryGraphQL.Providers;
// using Sirius.LibraryGraphQL.Repository;

namespace Sirius.LibraryGraphQL;

internal sealed class Startup
{
    public IConfiguration Configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseGraphiQL(new GraphiQLOptions {QueryPath = "/graphql", Path = "/graphiql"});
            app.UseDeveloperExceptionPage();
        }
        app.UseRouting();
        //app.UseWebSockets();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
            //endpoints.MapGraphQLHttp();
            endpoints.MapGraphQLSchema();
            // endpoints.MapControllers();
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            //.AddTypes(typeof(AuthorType), typeof(BookType))
            .AddType<AuthorType>()
            .AddType<BookType>()
            .AddType<ReaderType>()
            .AddType<RentType>()
            .AddTypes(typeof(AddBookInputType), typeof(AddAuthorInputType), typeof(AddReaderInputType), typeof(UpdateAuthorInputType),
                typeof(UpdateBookInputType), typeof(UpdateReaderInputType))
            
            .ModifyRequestOptions(x =>
            {
                if (_env.IsDevelopment())
                {
                    x.IncludeExceptionDetails = true;
                }
            })
            ;

        var dbSettings = new SqliteConfiguration();
        Configuration.GetSection("SQLite").Bind(dbSettings);
        var connStr = new SqliteConnectionStringBuilder()
        {
            Mode = Enum.Parse<SqliteOpenMode>(dbSettings.Mode),
            DataSource = dbSettings.DataSource,

        }.ToString();
        services
            .AddDbContext<AppDbContext>(
                x => x.UseSqlite(connStr));

        services
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IRentRepository, RentRepository>()
            .AddScoped<IReaderRepository, ReaderRepository>();
        
        
        // var sqliteConf = new SqliteConfiguration();
        // Configuration.GetSection("SQLite").Bind(sqliteConf);
        // var connStr = new SqliteConnectionStringBuilder
        // {
        //     Mode = Enum.Parse<SqliteOpenMode>(sqliteConf.Mode),
        //     DataSource = sqliteConf.DataSource,
        //     Password = sqliteConf.Password,
        // }.ToString();
        //     
        // services.AddDbContext<AppDbContext>(
        //     options => options.UseSqlite(connStr));
        //
        // services
        //     .AddScoped<IDateTimeProvider, DateTimeProvider>()
        //     .AddScoped<ICaesarEncoder, CaesarEncoder>()
        //     .AddSingleton<IShiftRepository, ShiftRepository>()
        //     .AddSingleton<IRotStatisticsProvider, RotStatisticsProvider>()
        //     .AddControllers();
    }
}