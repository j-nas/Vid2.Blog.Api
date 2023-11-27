using Microsoft.EntityFrameworkCore;
using Vid2.Blog.Api.Models; // Assuming your models are in this namespace

namespace Vid2.Blog.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<BlogPost> Blogs { get; set; }
    }
}
