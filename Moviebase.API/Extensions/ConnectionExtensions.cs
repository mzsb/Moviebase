#region Usings

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Moviebase.API.Extensions;

public static class ConnectionExtensions
{
    public static string GetDefaultConnectionString(this IConfiguration configuration) =>
    configuration.GetConnectionString("DefaultConnection") is var defaultConnection &&
    !string.IsNullOrEmpty(defaultConnection) ?
        defaultConnection :
        throw new Exception("Invalid DefaultConnectionString");

    public static IServiceCollection AddSqliteDbContext<TContext>(this IServiceCollection services, string? connectionString) where TContext : DbContext
    {
        var connection = new SqliteConnection(connectionString);
        connection.Open();
        return services.AddDbContext<TContext>(options =>
            options.UseSqlite(connection));
    }
}
