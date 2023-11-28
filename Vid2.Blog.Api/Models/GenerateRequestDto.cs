namespace Vid2.Blog.Api.Models;

public class GenerateRequestDto
{
    public required string YoutubeVideoId { get; set; }
    public bool Force { get; set; }
}