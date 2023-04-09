using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Db.ModelConfiguration;
public class PostConfiguration
       : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> mConfiguration)
    {
        mConfiguration.HasKey(x => x.Id);

        mConfiguration
            .Property(o => o.Descripcio)
            .HasMaxLength(250)
            .IsRequired();

        mConfiguration
            .Property(b => b.EsVisible)
            .IsRequired();

        mConfiguration
            .HasOne(x => x.Blog)
            .WithMany(x => x.Posts)
            .HasForeignKey("BlogId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}

