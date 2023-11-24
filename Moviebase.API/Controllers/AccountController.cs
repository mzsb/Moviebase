#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Interfaces;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            return await accountService.LoginAsync(loginDto);
        }
        catch (AccountException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            return await accountService.RegisterAsync(registerDto);
        }
        catch (AccountException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
