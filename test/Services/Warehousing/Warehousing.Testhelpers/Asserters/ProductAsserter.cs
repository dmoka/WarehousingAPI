using System.Linq;
using KaliGasService.TestHelpers.Asserters;
using KaliGasService.TestHelpers.Extensions;
using NFluent;
using Warehousing.Domain.Product;
using Warehousing.Domain.Product.Events;

namespace Warehousing.Testhelpers.Asserters
{
    public class ProductAsserter : AbstractAsserter<Product, ProductAsserter>
    {
        private string GetCustomMessage(string fieldName) => $"{fieldName} is not as expected";

        public static ProductAsserter AssertThat(Product actual)
        {
            return new ProductAsserter(actual);
        }

        public ProductAsserter(Product actual) : base(actual)
        {
        }

        public ProductAsserter HasName(string name)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Name))).That(Actual.Name).IsEqualToValue(name);
            return this;
        }

        public ProductAsserter HasArticleNumber(string articleNumber)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.ArticleNumber))).That(Actual.ArticleNumber).IsEqualToValue(articleNumber);
            return this;
        }

        public ProductAsserter HasProductType(ProductType productType)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Type))).That(Actual.Type).IsEqualTo(productType);
            return this;
        }

        public ProductAsserter HasCustomTariffNumber(string customTariffNumber)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.CustomTariffNumber))).That(Actual.CustomTariffNumber).IsEqualToValue(customTariffNumber);
            return this;
        }

        public ProductAsserter HasQuantity(int quantity)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Quantity))).That(Actual.Quantity).IsEqualTo(quantity);
            return this;
        }
        public ProductAsserter HasUnit(Unit unit)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Unit))).That(Actual.Unit).IsEqualTo(unit);
            return this;
        }

        public ProductAsserter HasNetUnitPrice(decimal netUnitPrice)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.NetUnitPrice))).That(Actual.NetUnitPrice).IsEqualTo(netUnitPrice);
            return this;
        }


        public ProductAsserter HasNetValue(decimal netValue)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.NetValue))).That(Actual.NetValue).IsEqualTo(netValue);
            return this;
        }

        public ProductAsserter HasVat(decimal vat)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Vat))).That(Actual.Vat).IsEqualTo(vat);
            return this;
        }

        public ProductAsserter HasVatSum(decimal vatSum)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.VatSum))).That(Actual.VatSum).IsEqualTo(vatSum);
            return this;
        }

        public ProductAsserter HasGrossUnitPrice(decimal grossUnitPrice)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.GrossUnitPrice))).That(Actual.GrossUnitPrice).IsEqualTo(grossUnitPrice);
            return this;
        }

        public ProductAsserter HasGrossValue(decimal grossValue)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.GrossValue))).That(Actual.GrossValue).IsEqualTo(grossValue);
            return this;
        }


        public ProductAsserter HasNotes(string notes)
        {
            Check.WithCustomMessage(GetCustomMessage(nameof(Actual.Notes))).That(Actual.Notes).IsEqualToValue(notes);
            return this;
        }

        public ProductAsserter HasRaisedEvent(ProductPickedEvent productPickedEvent)
        {
            var productPickedDomainEvent = GetEventByType<ProductPickedEvent>();

            Check.That(productPickedDomainEvent.Quantity).IsEqualTo(productPickedEvent.Quantity);
            
            return this;
        }

        public ProductAsserter HasRaisedEvent(ProductUnpickedEvent productUnpickedEvent)
        {
            var productPickedDomainEvent = GetEventByType<ProductUnpickedEvent>();

            Check.That(productPickedDomainEvent.Quantity).IsEqualTo(productUnpickedEvent.Quantity);

            return this;
        }

        private TEventType GetEventByType<TEventType>()
        {
            return (TEventType) Actual.Events.First(c => c.GetType() == typeof(TEventType));
        }
    }
}
