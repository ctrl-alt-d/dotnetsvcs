using MyApp.Db.ModelConfiguration.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Db.ModelConfiguration;

public class BlogConfiguration
       : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> mConfiguration)
    {

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
            .Property(o => o.Title)
            .HasMaxLength(250)
            .IsRequired();

        mConfiguration
            .Property(b => b.IsDeleted)
            .IsRequired();

        mConfiguration
            .Property(b => b.Rating)
            .IsRequired();

        mConfiguration
            .Property(b => b.Price)
            .HasColumnType("REAL"); // money a sqlserver

        mConfiguration
            .HasOne(x => x.Category)
            .WithMany(x => x.Blogs)
            .HasForeignKey("CategoriaId")
            .OnDelete(DeleteBehavior.SetNull);


    }
}

