#region Usings

using System.ComponentModel;

#endregion

namespace Moviebase.BLL.Dtos;

public class RegisterDto
{
    [DefaultValue("user")]
    public string Username { get; set; }

    [DefaultValue("user")]
    public string Password { get; set; }

    [DefaultValue("user@examp.le")]
    public string Email { get; set; }
}
