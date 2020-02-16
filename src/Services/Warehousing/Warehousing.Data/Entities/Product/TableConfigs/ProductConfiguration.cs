using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehousing.Data.Entities.Product.TableConfigs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Product.Product>
    {
        private const string ColumnTypeForPrice = "decimal(12,2)";

        public static string SchemaName = "dbo";
        public static string TableName = "products";

        private static readonly ProductTypeConverter ProductTypeConverter = new ProductTypeConverter();
        private static readonly UnitConverter UnitConverter = new UnitConverter();

        public void Configure(EntityTypeBuilder<Domain.Product.Product> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Type)
                .HasMaxLength(20)
                .IsRequired()
                .HasConversion(
                    @enum => ProductTypeConverter.EnumToCode(@enum),
                    code => ProductTypeConverter.CodeToEnum(code));

            builder.Property(p => p.CustomTariffNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.NetUnitPrice)
                .HasColumnType(ColumnTypeForPrice)
                .IsRequired();

            builder.Property(p => p.NetValue)
                .HasColumnType(ColumnTypeForPrice)
                .IsRequired();

            builder.Property(p => p.Vat)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(p => p.VatSum)
                .HasColumnType(ColumnTypeForPrice)
                .IsRequired();

            builder.Property(p => p.GrossUnitPrice)
                .HasColumnType(ColumnTypeForPrice)
                .IsRequired();

            builder.Property(p => p.GrossValue)
                .HasColumnType(ColumnTypeForPrice)
                .IsRequired();

            builder.Property(p => p.ArticleNumber)
                .HasMaxLength(20)
                .IsRequired(); ;

            builder.Property(p => p.Quantity)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.Unit)
                .HasMaxLength(20)
                .IsRequired()
                .HasConversion(
                    @enum => UnitConverter.EnumToCode(@enum),
                    code => UnitConverter.CodeToEnum(code));

            builder.Property(p => p.Notes)
                .HasMaxLength(1000);
        }
    }
}
