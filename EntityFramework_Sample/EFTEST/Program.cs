using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

Console.WriteLine("Hello, World!");

string dbName = "TestDatabase.db";
using (var dbContext = new BloggingContext())
{
    if (File.Exists(dbName))
    {
        foreach (var blog in dbContext.Blogs)
        {
            Console.WriteLine(blog.BlogId);
        }
    }
    else
    {
        dbContext.Database.EnsureCreated();
        if (!dbContext.Blogs.Any())
        {
            dbContext.Blogs.AddRange(new Blog[]
            {
                new Blog{ BlogId=1, Url = "test1"},
                new Blog{ BlogId=2, Url = "tes3"},
                new Blog{ BlogId=3, Url = "test41"}
            });
            dbContext.SaveChanges();
        }
        foreach (var blog in dbContext.Blogs)
        {
            Console.WriteLine(blog.BlogId);
        }
    }

}
Console.ReadLine();


public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost:49156;Database=postgres;Username=postgres;Password=postgrespw"); // sample
}


public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}
public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}