#region Usings

using Microsoft.Extensions.Configuration;

#endregion

namespace Moviebase.BLL.Extensions;

public static class TokenExtensions
{
    public static string GetTokenKey(this IConfiguration configuration) =>
        configuration["TokenKey"] is var tokenKey &&
        !string.IsNullOrEmpty(tokenKey) ?
            tokenKey :
            throw new Exception("Invalid token key");
}
