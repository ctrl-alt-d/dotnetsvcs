namespace MyApp.Models;

public class Blog
{
    public int Id { get; set; }
    public virtual string Title { get; set; } = default!;

    public virtual int Rating { get; set; } = default!;

    public virtual bool IsDeleted { get; set; } = default!;

    public virtual decimal? Price { get; set; } = default!;

    // Categoria
    public virtual Category? Category { get; set; } = default!;

    // Posts
    public virtual IEnumerable<Post> Posts { get; set; } = default!;

    public byte[] TimeStamp { get; set; } = default!;

}

