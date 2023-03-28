namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Models;

public class Post {
    public int Id { get; set; }
    public virtual string Descripcio { get; set; } = default!;
    public virtual bool EsVisible { get; set; } = default!;
    public virtual Blog Blog { get; set; } = default!;
    public virtual bool IsSoftDeleted { get; set; } = default!;
}

