#region Usings

using System.ComponentModel;

#endregion

namespace Moviebase.BLL.Dtos;

public class CreateReviewDto
{
    [DefaultValue("a25cbf32-d93d-4ab9-984f-21290f393d8c")]
    public Guid UserId { get; set; }

    [DefaultValue("35856fc5-f427-458f-a0a5-13a8ab381f33")]
    public Guid MovieId { get; set; }

    [DefaultValue("Lorem ipsum dolor sit amet.")]
    public string Content { get; set; }
}
