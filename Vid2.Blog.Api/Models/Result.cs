using Microsoft.EntityFrameworkCore;

namespace Vid2.Blog.Api.Models;

[Index(nameof(YoutubeId), IsUnique = true)]
public class Result
{
    public Guid Id { get; set; }
    public required string CompletionResult { get; set; }
    public required string YoutubeId { get; set; }
}