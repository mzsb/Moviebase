#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Interfaces;
using System.ComponentModel;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService)
{
    private const string _exampleUserId = "e214ce42-f4f4-4859-ba1d-db6ab1f21f75";

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync() =>
        await userService.GetUsersAsync();

    [HttpDelete("{userId}")]
    public async Task DeleteUserAsync(
        [DefaultValue(typeof(Guid), _exampleUserId)]  Guid userId) =>
        await userService.DeleteUserAsync(userId);
}
