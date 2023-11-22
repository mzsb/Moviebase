#region Usings

using Moviebase.DAL;

#endregion

namespace Moviebase.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSqliteDbContext<MoviebaseDbContext>(configuration.GetDefaultConnectionString());
}
