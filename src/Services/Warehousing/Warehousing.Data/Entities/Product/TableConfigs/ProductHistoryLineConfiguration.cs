using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product.TableConfigs
{
    public class ProductHistoryLineConfiguration : IEntityTypeConfiguration<ProductHistoryLine>
    {
        public static string SchemaName = "dbo";
        public static string TableName = "productHistoryLines";

        private static readonly ProductHistoryTypeConverter ProductHistoryTypeConverter = new ProductHistoryTypeConverter();

        public void Configure(EntityTypeBuilder<ProductHistoryLine> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.OccurredOn)
                .IsRequired();

            builder.Property(p => p.DeltaQuantity)
                .IsRequired();

            builder.Property(c => c.Type)
                .HasMaxLength(20)
                .IsRequired()
                .HasConversion(
                    @enum => ProductHistoryTypeConverter.EnumToCode(@enum),
                    code => ProductHistoryTypeConverter.CodeToEnum(code));
        }
    }
}
