#region Usings

using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Interfaces;
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

        await MigrateDbAsync(service);
        await SeedAsync(service);

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    
    private static async Task MigrateDbAsync(IServiceProvider service)
    {
        try
        {
            var context = service.GetRequiredService<MoviebaseDbContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }

    private static async Task SeedAsync(IServiceProvider service)
    {
        try
        {
            var seedService = service.GetRequiredService<ISeedService>();
            await seedService.Seed();
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}
