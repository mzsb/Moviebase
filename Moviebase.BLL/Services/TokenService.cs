#region Usings

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Extensions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL.Model.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

#endregion

namespace Moviebase.BLL.Services;

public class TokenService(
    IConfiguration configuration, 
    UserManager<User> userManager) : ITokenService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private readonly SymmetricSecurityKey _tokenKey = new (Encoding.UTF8.GetBytes(configuration.GetValue("TokenKey")));

    private readonly UserManager<User> _userManager = userManager;

    public async Task<string> CreateTokenAsync(User user) => 
        _tokenHandler.WriteToken(
            _tokenHandler.CreateToken(new()
            {
                Subject = new ClaimsIdentity([
                    new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? throw new UserException("Invalid username")),
                    .. (await _userManager.GetRolesAsync(user))
                        .Select(role => new Claim(ClaimTypes.Role, role))
                ]),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new(_tokenKey, SecurityAlgorithms.HmacSha512Signature)
            })
        );
}
