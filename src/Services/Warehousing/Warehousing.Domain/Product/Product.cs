using KaliGasService.Core.Domain;
using Warehousing.Domain.Product.Events;

namespace Warehousing.Domain.Product
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; }

        public string ArticleNumber { get; }

        public ProductType Type { get; }

        public string CustomTariffNumber { get; } //VTSZ

        public int Quantity { get; private set; }
        public Unit Unit { get; }

        public decimal NetUnitPrice { get; }

        public decimal NetValue { get; }

        public decimal Vat { get; }
        public decimal VatSum { get; }

        public decimal GrossUnitPrice { get; }
        public decimal GrossValue { get; }

        public string Notes { get; }

        public Product() //EF needs it
        {
        }

        public Product(string name, ProductType type, string articleNumber, string customTariffNumber, int quantity,
            Unit unit, decimal netUnitPrice, decimal netValue, decimal vat, decimal vatSum,
            decimal grossUnitPrice, decimal grossValue,
            string notes)
        {
            Name = name;
            ArticleNumber = articleNumber;
            NetUnitPrice = netUnitPrice;
            Quantity = quantity;
            Notes = notes;
            GrossUnitPrice = grossUnitPrice;
            NetValue = netValue;
            Vat = vat;
            VatSum = vatSum;
            GrossValue = grossValue;
            Unit = unit;
            CustomTariffNumber = customTariffNumber;
            Type = type;
        }

        public void Pick(int quantity)
        {
            Quantity -= quantity;
            RaiseEvent(new ProductPickedEvent(Id, quantity));
        }

        public void Unpick(int quantity)
        {
            Quantity += quantity;
            RaiseEvent(new ProductUnpickedEvent(Id, quantity));
        }
    }
}
