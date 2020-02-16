using KaliGasService.Core.Data;
using KaliGasService.Core.Types;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public class ProductHistoryTypeConverter : BasicEnumConverter<string, ProductHistoryType>
    {
        public ProductHistoryTypeConverter() : base(new BiDictionary<string, ProductHistoryType>
        {
            {nameof(ProductHistoryType.Pick), ProductHistoryType.Pick},
            {nameof(ProductHistoryType.Unpick), ProductHistoryType.Unpick}
        })
        { }
    }
}
