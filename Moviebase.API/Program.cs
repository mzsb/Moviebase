#region Usings

using Microsoft.EntityFrameworkCore;
using Moviebase.DAL;

#endregion

namespace Moviebase.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var service = scope.ServiceProvider;
        try
        {
            var context = service.GetRequiredService<MoviebaseDbContext>();
            await context.Database.MigrateAsync();
            await context.SeedTestItemsAsync();
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
