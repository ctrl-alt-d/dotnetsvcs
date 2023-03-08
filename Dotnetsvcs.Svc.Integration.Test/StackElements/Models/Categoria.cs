namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Models;


public class Categoria
{
    public int Id { get; set; }
    public virtual string Titol { get; set; } = default!;
    public virtual Categoria? Pare { get; set; } = default!;
    public virtual IEnumerable<Categoria> CategoriesFilles { get; set; } = default!;
    public virtual IEnumerable<Blog> Blogs { get; set; } = default!;

}

