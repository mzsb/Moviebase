#region Usings

using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(User user);
}
