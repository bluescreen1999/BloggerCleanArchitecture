using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BloggerSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloggerSample.Infrastructure.Configurations
{
    public sealed class BlogConfigurations : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> _)    
        {
            _.ToTable("Blogs");

            _.HasKey(_ => _.Id);

            _.HasIndex(_ => _.Id)
                .IsUnique()
                .HasDatabaseName("blogsIndex");

            _.Property(_ => _.Id)
                .ValueGeneratedNever()
                .IsRequired();

            _.Property(_ => _.Title)
                .HasMaxLength(150)
                .IsRequired();

            _.Property(_ => _.Body)
                .HasMaxLength(1500)
                .IsRequired();

            _.Property(_ => _.IsDeleted)
                .IsRequired();

            _.Property(_ => _.CreationDateTime)
                .IsRequired();
        }
    }
}
