using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Configurations
{
    public class ProductConfigurations: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);
            
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            builder.Property(p => p.DiscountPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(p => p.Barcode)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
