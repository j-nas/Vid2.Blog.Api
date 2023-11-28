using Microsoft.EntityFrameworkCore;

namespace Vid2.Blog.Api.Models;

    [PrimaryKey("ClerkId")]
public class Author
{
    public required string ClerkId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public List<BlogPost>? BlogPosts { get; set; }
    
}