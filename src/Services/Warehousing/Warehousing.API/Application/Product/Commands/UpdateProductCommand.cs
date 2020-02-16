using System;
using KaliGasService.Core.Application.CQRS;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class UpdateProductCommand : Command<Result<bool>>, IProductFields
    {
        public UpdateProductCommand() { } //Needed for deserializing from json when API endpoint is called

        public UpdateProductCommand(Guid id, string name, string articleNumber, ProductType type, string customTariffNumber, int quantity, Unit unit, decimal netUnitPrice, decimal netValue, decimal vat, decimal vatSum, decimal grossUnitPrice, decimal grossValue, string notes)
        {
            Id = id;
            Name = name;
            ArticleNumber = articleNumber;
            Type = type;
            CustomTariffNumber = customTariffNumber;
            Quantity = quantity;
            Unit = unit;
            NetUnitPrice = netUnitPrice;
            NetValue = netValue;
            Vat = vat;
            VatSum = vatSum;
            GrossUnitPrice = grossUnitPrice;
            GrossValue = grossValue;
            Notes = notes;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ArticleNumber { get; set; }
        public ProductType Type { get; set; }
        public string CustomTariffNumber { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public decimal NetUnitPrice { get; set; }
        public decimal NetValue { get; set; }
        public decimal Vat { get; set; }
        public decimal VatSum { get; set; }
        public decimal GrossUnitPrice { get; set; }
        public decimal GrossValue { get; set; }
        public string Notes { get; set; }

    }
}
