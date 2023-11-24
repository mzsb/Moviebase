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
        .AddScoped<IAccountService, AccountService>()
        .AddScoped<ITokenService, TokenService>()
        .AddAutoMapper(typeof(AutoMapperProfiles).Assembly)
        .AddSqliteDbContext<MoviebaseDbContext>(configuration.GetDefaultConnectionString());
}
