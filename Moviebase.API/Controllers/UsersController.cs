﻿#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Interfaces;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    private const string _exampleUserId = "e214ce42-f4f4-4859-ba1d-db6ab1f21f75";

    private readonly IUserService _userService = userService;

    //[Authorize(Policy = "RequireAdminRole")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync() =>
        await _userService.GetUsersAsync();

    //[Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUserAsync(
        [DefaultValue(typeof(Guid), _exampleUserId)]  Guid userId)
    {
        try
        {
            await _userService.DeleteUserAsync(userId);
            return Ok("User deletion success");
        }
        catch (UserException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
