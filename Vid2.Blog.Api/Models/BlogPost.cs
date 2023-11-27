using System.ComponentModel.DataAnnotations;

namespace Vid2.Blog.Api.Models;

public class BlogPost
{
    public int Id { get; set; }
    [MaxLength(125)]
    public required string Title { get; set; }
    [MaxLength(125)]
    public required string Slug { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    [MaxLength(125)]
    public required string Author { get; set; }
    public required string AuthorId { get; set; }
    public required string YoutubeVideoId { get; set; }
}
