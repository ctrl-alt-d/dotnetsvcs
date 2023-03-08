namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Models;

public class Blog
{
    public int Id { get; set; }
    public virtual string Titol { get; set; } = default!;

    public virtual int Rating { get; set; } = default!;

    public virtual bool EsVisible { get; set; } = default!;

    public virtual decimal? Preu { get; set; } = default!;

    // Categoria
    public virtual Categoria? Categoria { get; set; } = default!;

    // Posts
    public virtual IEnumerable<Post> Posts { get; set; } = default!;

    public byte[] TimeStamp { get; set; } = default!;

}

