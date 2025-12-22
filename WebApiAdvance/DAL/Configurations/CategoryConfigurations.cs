using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(c => c.Description)
                .HasMaxLength(1000);
                        
            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
