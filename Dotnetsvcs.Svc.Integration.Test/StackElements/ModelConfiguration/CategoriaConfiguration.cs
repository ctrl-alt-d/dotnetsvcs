using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.ModelConfiguration;
public class CategoriaConfiguration
       : IEntityTypeConfiguration<Categoria> {
    public void Configure(EntityTypeBuilder<Categoria> mConfiguration) {
        mConfiguration.HasKey(x => x.Id);

        mConfiguration
            .Property(t => t.Id)
            .ValueGeneratedNever();

        mConfiguration
            .Property(o => o.Titol)
            .HasMaxLength(250)
            .IsRequired();

        mConfiguration
            .Property(o => o.Titol)
            .HasMaxLength(250)
            .IsRequired();

        mConfiguration
            .HasOne(x => x.Pare)
            .WithMany(x => x!.CategoriesFilles)
            .HasForeignKey("PareId")
            .OnDelete(DeleteBehavior.Restrict);

    }
}
