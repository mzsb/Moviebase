#region Usings

using System.ComponentModel;

#endregion

namespace Moviebase.BLL.Dtos;

public class UpdateReviewDto
{
    [DefaultValue("Consectetur adipiscing elit.")]
    public string Content { get; set; }
}
