namespace Warehousing.Domain.Product
{
    public class ProductBuilder 
    {
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

        public Product Build()
        {
            return new Product(
                Name,
                Type,
                ArticleNumber,
                CustomTariffNumber,
                Quantity,
                Unit,
                NetUnitPrice,
                NetValue,
                Vat,
                VatSum,
                GrossUnitPrice,
                GrossValue, 
                Notes);
        }

        public ProductBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public ProductBuilder WithType(ProductType type)
        {
            Type = type;
            return this;
        }

        public ProductBuilder WithCustomTariffNumer(string customTariffNumber)
        {
            CustomTariffNumber = customTariffNumber;
            return this;
        }

        public ProductBuilder WithArticleNumber(string articleNumber)
        {
            ArticleNumber = articleNumber;
            return this;
        }

        public ProductBuilder WithNetUnitPrice(decimal netUnitPrice)
        {
            NetUnitPrice = netUnitPrice;
            return this;
        }

        public ProductBuilder WithNetValue(decimal netValue)
        {
            NetValue = netValue;
            return this;
        }

        public ProductBuilder WithVat(decimal vat)
        {
            Vat = vat;
            return this;
        }

        public ProductBuilder WithVatSum(decimal vatSum)
        {
            VatSum = vatSum;
            return this;
        }


        public ProductBuilder WithGrossUnitPrice(decimal grossUnitPrice)
        {
            GrossUnitPrice = grossUnitPrice;
            return this;
        }

        public ProductBuilder WithGrossValue(decimal grossValue)
        {
            GrossValue = grossValue;
            return this;
        }

        public ProductBuilder WithQuantity(int quantity)
        {
            Quantity = quantity;
            return this;
        }

        public ProductBuilder WithUnit(Unit unit)
        {
            Unit = unit;
            return this;
        }

        public ProductBuilder WithNotes(string notes)
        {
            Notes = notes;
            return this;
        }

    }
}
