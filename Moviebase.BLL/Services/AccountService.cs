#region Usings

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Extensions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.BLL.Services;

public class AccountService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenService tokenService,
    IMapper mapper) : IAccountService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IMapper _mapper = mapper;

    public async Task<LoggedDto> LoginAsync(LoginDto loginDto)
    {
        if (string.IsNullOrEmpty(loginDto.Username)) throw new AccountException("Invalid username");

        if (string.IsNullOrEmpty(loginDto.Password)) throw new AccountException("Invalid password");

        var user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.Username) 
            ?? throw new AccountException("Invalid username");

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) throw new AccountException("Invalid password");

        return new LoggedDto
        {
            User = _mapper.Map<UserDto>(user),
            Token = await _tokenService.CreateTokenAsync(user)
        };
    }

    public async Task<LoggedDto> RegisterAsync(RegisterDto registerDto)
    {
        if (string.IsNullOrEmpty(registerDto.Username)) throw new AccountException("Invalid username");

        if (string.IsNullOrEmpty(registerDto.Password)) throw new AccountException("Invalid password");

        if (string.IsNullOrEmpty(registerDto.Email) ||
            !registerDto.Email.IsValidEmail()) throw new AccountException("Invalid email");

        var user = new User 
        { 
            UserName = registerDto.Username,
            Email = registerDto.Email
        };

        try
        {
            await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, "User");
        }
        catch (InvalidOperationException) { throw new AccountException("Registration failed"); }

        return await LoginAsync(new LoginDto
        {
            Username = user.UserName, 
            Password = registerDto.Password
        });
    }
}
