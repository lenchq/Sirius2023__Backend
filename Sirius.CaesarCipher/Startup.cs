using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Sirius.CaesarCipher.Database;
using Sirius.CaesarCipher.Interfaces;
using Sirius.CaesarCipher.Model;
using Sirius.CaesarCipher.Providers;
using Sirius.CaesarCipher.Repository;

namespace Sirius.CaesarCipher;

public sealed class Startup
{
    public IConfiguration Configuration;
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var sqliteConf = new SqliteConfiguration();
        Configuration.GetSection("SQLite").Bind(sqliteConf);
        var connStr = new SqliteConnectionStringBuilder
        {
            Mode = Enum.Parse<SqliteOpenMode>(sqliteConf.Mode),
            DataSource = sqliteConf.DataSource,
            Password = sqliteConf.Password,
        }.ToString();
            
        services.AddDbContext<AppDbContext>(
            options => options.UseSqlite(connStr));

        services
            .AddScoped<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<ICaesarEncoder, CaesarEncoder>()
            .AddSingleton<IShiftRepository, ShiftRepository>()
            .AddSingleton<IRotStatisticsProvider, RotStatisticsProvider>()
            .AddControllers();
    }
}