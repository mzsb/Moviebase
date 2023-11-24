#region Usings

using Microsoft.AspNetCore.Identity;

#endregion

namespace Moviebase.DAL.Model.Identity;

public class User : IdentityUser<Guid>
{
    public DateTime Created { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}
