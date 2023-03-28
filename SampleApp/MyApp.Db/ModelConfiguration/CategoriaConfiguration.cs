using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Db.ModelConfiguration;
public class CategoriaConfiguration
       : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> mConfiguration)
    {
        mConfiguration.HasKey(x => x.Id);

        mConfiguration
            .Property(t => t.Id)
            .ValueGeneratedNever();

        mConfiguration
            .Property(o => o.Name)
            .HasMaxLength(250)
            .IsRequired();

        mConfiguration
            .Property(o => o.Name)
            .HasMaxLength(250)
            .IsRequired();

        mConfiguration
            .HasOne(x => x.Parent)
            .WithMany(x => x!.SubCategories)
            .HasForeignKey("PareId")
            .OnDelete(DeleteBehavior.Restrict);

    }
}
