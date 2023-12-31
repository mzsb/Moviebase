﻿#region Usings

using Microsoft.AspNetCore.Mvc;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Interfaces;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpPost("login")]
    public async Task<ActionResult<LoggedDto>> LoginAsync(
        [FromBody] LoginDto loginDto)
    {
        try
        {
            return await _accountService.LoginAsync(loginDto);
        }
        catch (AccountException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoggedDto>> RegisterAsync(
        [FromBody] RegisterDto registerDto)
    {
        try
        {
            return await _accountService.RegisterAsync(registerDto);
        }
        catch (AccountException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
