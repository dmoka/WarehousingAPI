using KaliGasService.Core.Data;
using KaliGasService.Core.Types;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public class ProductTypeConverter : BasicEnumConverter<string, ProductType>
    {
        public ProductTypeConverter() : base(new BiDictionary<string, ProductType>
        {
            {nameof(ProductType.FlueGasDrainage), ProductType.FlueGasDrainage},
            {nameof(ProductType.Controller), ProductType.Controller},
            {nameof(ProductType.Filter), ProductType.Filter},
            {nameof(ProductType.GasBoiler), ProductType.GasBoiler},
            {nameof(ProductType.Other), ProductType.Other},
            {nameof(ProductType.Pipe), ProductType.Pipe},
            {nameof(ProductType.Valve), ProductType.Valve},
        })
        { }
    }
}
