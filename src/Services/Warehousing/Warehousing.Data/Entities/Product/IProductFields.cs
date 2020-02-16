using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public interface IProductFields
    {
        string Name { get; set; }
        string ArticleNumber { get; set; }
        ProductType Type { get; set; }
        string CustomTariffNumber { get; set; }
        int Quantity { get; set; }
        Unit Unit { get; set; }
        decimal NetUnitPrice { get; set; }
        decimal NetValue { get; set; }
        decimal Vat { get; set; }
        decimal VatSum { get; set; }
        decimal GrossUnitPrice { get; set; }
        decimal GrossValue { get; set; }
        string Notes { get; set; }
    }
}