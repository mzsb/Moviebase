#region Usings

using Microsoft.Extensions.Configuration;

#endregion

namespace Moviebase.BLL.Extensions;

public static class ConfigurationExtensions
{
    public static string GetValue(this IConfiguration configuration, string name) =>
        configuration[name] is var value &&
        !string.IsNullOrEmpty(value) ?
            value :
            throw new Exception($"Invalid {name} key");
}
