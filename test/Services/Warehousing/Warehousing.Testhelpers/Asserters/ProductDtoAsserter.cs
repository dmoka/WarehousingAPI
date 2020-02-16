using KaliGasService.TestHelpers.Asserters;
using KaliGasService.TestHelpers.Extensions;
using NFluent;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;

namespace Warehousing.Testhelpers.Asserters
{
    public class ProductDtoAsserter : AbstractAsserter<ProductDto, ProductDtoAsserter>
    {
        private string GetCustomMessage(string fieldName) => $"{fieldName} is not as expected";

        public static ProductDtoAsserter AssertThat(ProductDto actual)
        {
            return new ProductDtoAsserter(actual);
        }

        public ProductDtoAsserter(ProductDto actual) : base(actual)
        {
        }

        public ProductDtoAsserter HasName(string name)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Name))).That(Actual.Name).IsEqualToValue(name);
            return this;
        }

        public ProductDtoAsserter HasArticleNumber(string articleNumber)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.ArticleNumber))).That(Actual.ArticleNumber).IsEqualToValue(articleNumber);
            return this;
        }

        public ProductDtoAsserter HasProductType(ProductType productType)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Type))).That(Actual.Type).IsEqualTo(productType);
            return this;
        }

        public ProductDtoAsserter HasCustomTariffNumber(string customTariffNumber)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.CustomTariffNumber))).That(Actual.CustomTariffNumber).IsEqualToValue(customTariffNumber);
            return this;
        }

        public ProductDtoAsserter HasQuantity(int quantity)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Quantity))).That(Actual.Quantity).IsEqualTo(quantity);
            return this;
        }
        public ProductDtoAsserter HasUnit(Unit unit)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Unit))).That(Actual.Unit).IsEqualTo(unit);
            return this;
        }

        public ProductDtoAsserter HasNetUnitPrice(decimal netUnitPrice)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.NetUnitPrice))).That(Actual.NetUnitPrice).IsEqualTo(netUnitPrice);
            return this;
        }


        public ProductDtoAsserter HasNetValue(decimal netValue)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.NetValue))).That(Actual.NetValue).IsEqualTo(netValue);
            return this;
        }

        public ProductDtoAsserter HasVat(decimal vat)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Vat))).That(Actual.Vat).IsEqualTo(vat);
            return this;
        }

        public ProductDtoAsserter HasVatSum(decimal vatSum)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.VatSum))).That(Actual.VatSum).IsEqualTo(vatSum);
            return this;
        }

        public ProductDtoAsserter HasGrossUnitPrice(decimal grossUnitPrice)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.GrossUnitPrice))).That(Actual.GrossUnitPrice).IsEqualTo(grossUnitPrice);
            return this;
        }

        public ProductDtoAsserter HasGrossValue(decimal grossValue)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.GrossValue))).That(Actual.GrossValue).IsEqualTo(grossValue);
            return this;
        }


        public ProductDtoAsserter HasNotes(string notes)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Notes))).That(Actual.Notes).IsEqualToValue(notes);
            return this;
        }


    }
}
