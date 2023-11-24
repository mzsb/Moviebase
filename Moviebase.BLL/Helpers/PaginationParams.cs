#region Usings

using System.ComponentModel;

#endregion

namespace Moviebase.BLL.Helpers;

public class PaginationParams
{
    [DefaultValue(1)]
    public int PageNumber { get; set; }

    [DefaultValue(10)]
    public int PageSize { get; set; }
}
