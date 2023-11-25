#region Usings

using System.ComponentModel;

#endregion

namespace Moviebase.BLL.Dtos;

public class CreateMovieDto
{
    [DefaultValue("Example")]
    public string Title { get; set; }
}
