//using Entities.Concrete;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DataAccess.Configurations
//{
//    public class ProductConfiguration : IEntityTypeConfiguration<Product>
//    {
//        public void Configure(EntityTypeBuilder<Product> builder)
//        {
//            builder.HasKey(x => x.ProductId);
//            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(200);
//            //builder.Property(x => x.UserId).HasColumnType("decimal(18,2)"); 
//        }
//    }
//}
