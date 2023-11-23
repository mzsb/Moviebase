#region Usings

using Moviebase.BLL.Helpers;
using Moviebase.BLL.Interfaces;
using Moviebase.BLL.Services;
using Moviebase.DAL;

#endregion

namespace Moviebase.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) => services
        .AddScoped<ITestService, TestService>()
        .AddAutoMapper(typeof(AutoMapperProfiles).Assembly)
        .AddSqliteDbContext<MoviebaseDbContext>(configuration.GetDefaultConnectionString());
}
