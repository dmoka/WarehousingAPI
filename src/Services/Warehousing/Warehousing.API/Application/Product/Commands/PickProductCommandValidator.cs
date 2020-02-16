using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using Warehousing.API.Application.Errors;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class PickProductCommandValidator : Validator<PickProductCommand>
    {
        private readonly IProductDao _productDao;

        public PickProductCommandValidator(IProductDao productDao)
        {
            _productDao = productDao;
        }
        public override async Task Validate(PickProductCommand pickProductCommand)
        {
            if (!await _productDao.HasMinimumQuantity(pickProductCommand.Id, pickProductCommand.Quantity))
            {
                AddError(Error.Create(ErrorCodes.ProductHasNotEnoughQuantity, $"The product {pickProductCommand.Name} has not enough quantity to be picked"));
            }
        }
    }
}
