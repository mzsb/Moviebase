#region Usings

using System.ComponentModel;

#endregion

namespace Moviebase.BLL.Dtos;

public class LoginDto
{
    [DefaultValue("admin")]
    public string Username { get; set; }

    [DefaultValue("admin")]
    public string Password { get; set; }
}
