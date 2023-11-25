#region Usings

using Moviebase.BLL.Dtos;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
}
