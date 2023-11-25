#region Usings

using Moviebase.BLL.Dtos;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface IAccountService
{
    Task<LoggedDto> LoginAsync(LoginDto loginDto);

    Task<LoggedDto> RegisterAsync(RegisterDto loginDto);
}
