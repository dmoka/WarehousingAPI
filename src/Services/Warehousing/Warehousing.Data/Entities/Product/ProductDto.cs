using System;
using KaliGasService.Core.Data.DAO;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public class ProductDto : IEntityDto, IProductFields
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ArticleNumber { get; set; }
        public ProductType Type { get; set; }
        public string CustomTariffNumber { get; set; } //VTSZ
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