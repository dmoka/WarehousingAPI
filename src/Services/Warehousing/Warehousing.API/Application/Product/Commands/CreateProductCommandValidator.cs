using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using Warehousing.API.Application.Errors;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class CreateProductCommandValidator : Validator<CreateProductCommand>
    {
        private readonly IProductDao _productDao;

        public CreateProductCommandValidator(IProductDao productDao)
        {
            _productDao = productDao;
        }

        public override async Task Validate(CreateProductCommand @object)
        {
            if (await _productDao.Exists(@object.Name))
            {
                AddError(ErrorCodes.ProductNameAlreadyReserved, "The product with this name does already exists");
            }
        }
    }
}