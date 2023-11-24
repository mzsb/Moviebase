#region Usings

using Moviebase.BLL.Dtos;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IAccountService
{
    Task<UserDto> LoginAsync(LoginDto loginDto);

    Task<UserDto> RegisterAsync(RegisterDto loginDto);
}
