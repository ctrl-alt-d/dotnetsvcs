namespace MyApp.Models;


public class Category
{
    public int Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual Category? Parent { get; set; } = default!;
    public virtual IEnumerable<Category> SubCategories { get; set; } = default!;
    public virtual IEnumerable<Blog> Blogs { get; set; } = default!;

}

