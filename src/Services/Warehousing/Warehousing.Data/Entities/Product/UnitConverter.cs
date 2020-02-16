using KaliGasService.Core.Data;
using KaliGasService.Core.Types;
using Warehousing.Domain.Product;

namespace Warehousing.Data.Entities.Product
{
    public class UnitConverter : BasicEnumConverter<string, Unit>
    {
        public UnitConverter() : base(new BiDictionary<string, Unit>
        {
            {nameof(Unit.Piece), Unit.Piece},
            {nameof(Unit.Package), Unit.Package}
        })
        { }
    }
}
