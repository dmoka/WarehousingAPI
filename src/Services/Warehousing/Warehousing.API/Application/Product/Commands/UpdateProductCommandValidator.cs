using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using Warehousing.API.Application.Errors;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class UpdateProductCommandValidator: Validator<UpdateProductCommand>
    {
        private readonly IProductDao _productEntityDao;

        public UpdateProductCommandValidator(IProductDao productEntityDao)
        {
            _productEntityDao = productEntityDao;
        }

        public override async  Task Validate(UpdateProductCommand @object)
        {
            await ValidateThatThereIsNoOtherProductHavingTheSameName(@object);
        }

        private async Task ValidateThatThereIsNoOtherProductHavingTheSameName(UpdateProductCommand @object)
        {
            if (await _productEntityDao.OtherProductPresentWithName(@object.Id, @object.Name))
            {
                AddError(ErrorCodes.ProductNameAlreadyReserved, "The product with this name does already exists");
            }
        }
    }
}
