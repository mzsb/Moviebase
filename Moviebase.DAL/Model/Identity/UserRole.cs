#region Usings

using Microsoft.AspNetCore.Identity;

#endregion

namespace Moviebase.DAL.Model.Identity;

public class UserRole : IdentityUserRole<Guid>
{
    public User? User { get; set; }

    public Role? Role { get; set; }
}
