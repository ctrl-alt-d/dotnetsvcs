using Dotnetsvcs.Svc.Integration.Test.StackElements.ModelConfiguration.Helpers;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.ModelConfiguration;

public class BlogConfiguration
       : IEntityTypeConfiguration<Blog> {
    public void Configure(EntityTypeBuilder<Blog> mConfiguration) {

        //
        // Sqlite workaround.
        //
        // mConfiguration
        //    .ConfigureTimeStamp();
        //
        mConfiguration
            .Property(p => p.TimeStamp)
            .IsRowVersion()
            .HasConversion(new SqliteTimestampConverter())
            .HasColumnType("INTEGER")
            .HasDefaultValueSql("1");

        mConfiguration
            .HasKey(x => x.Id);

        mConfiguration
            .Property(o => o.Titol)
            .HasMaxLength(250)
            .IsRequired();

        mConfiguration
            .Property(b => b.EsVisible)
            .IsRequired();

        mConfiguration
            .Property(b => b.Rating)
            .IsRequired();

        mConfiguration
            .Property(b => b.Preu)
            .HasColumnType("REAL"); // money a sqlserver

        mConfiguration
            .HasOne(x => x.Categoria)
            .WithMany(x => x.Blogs)
            .HasForeignKey("CategoriaId")
            .OnDelete(DeleteBehavior.SetNull);


    }
}

