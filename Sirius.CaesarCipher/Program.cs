using Sirius.CaesarCipher;

namespace Sirius.CaesarCipher;

public static class Program
{
    public static void Main(string[] args) =>
        CreateHostBuilder(args).Build().Run();

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(
                builder =>
                {
                    builder.UseStartup<Startup>();
                });
}

