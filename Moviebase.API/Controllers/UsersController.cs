#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Interfaces;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync() =>
        await userService.GetUsersAsync();
}
