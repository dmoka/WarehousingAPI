using Warehousing.Domain.Product;

namespace Warehousing.Testhelpers.Fakes
{
    public class ProductFakes
    {
        public static Product ProductWithAllPropsFilled1() => ProductWithAllPropsFilled1(5);

        public static Product ProductWithAllPropsFilled1(int quantity) => new ProductBuilder()
            .WithName("Ariston")
            .WithType(ProductType.GasBoiler)
            .WithArticleNumber("123456789")
            .WithCustomTariffNumer("730900")
            .WithQuantity(quantity)
            .WithUnit(Unit.Piece)
            .WithNetUnitPrice(40000)
            .WithNetValue(40000)
            .WithVat(27)
            .WithVatSum(11000)
            .WithGrossUnitPrice(51000)
            .WithGrossValue(51000)
            .WithNotes("Egyeb")
            .Build();

        public static Product ProductWithAllPropsFilled1WithNewName() => new ProductBuilder()
            .WithName("Ariston new")
            .WithType(ProductType.GasBoiler)
            .WithArticleNumber("123456789")
            .WithCustomTariffNumer("730900")
            .WithQuantity(5)
            .WithUnit(Unit.Piece)
            .WithNetUnitPrice(40000)
            .WithNetValue(40000)
            .WithVat(27)
            .WithVatSum(11000)
            .WithGrossUnitPrice(51000)
            .WithGrossValue(51000)
            .WithNotes("Egyeb")
            .Build();

        public static Product ProductWithAllPropsFilled2() => new ProductBuilder()
            .WithName("Ariston 2")
            .WithType(ProductType.Filter)
            .WithArticleNumber("987654321")
            .WithCustomTariffNumer("730901")
            .WithQuantity(3)
            .WithUnit(Unit.Piece)
            .WithNetUnitPrice(30000)
            .WithNetValue(30000)
            .WithVat(25)
            .WithVatSum(7500)
            .WithGrossUnitPrice(51000)
            .WithGrossValue(37500)
            .WithNotes("Egyeb 2")
            .Build();
    }
}
