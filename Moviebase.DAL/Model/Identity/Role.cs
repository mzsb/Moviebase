#region Usings

using Microsoft.AspNetCore.Identity;

#endregion

namespace Moviebase.DAL.Model.Identity;

public class Role : IdentityRole<Guid>
{
    public ICollection<UserRole>? UserRoles { get; set; }
}
