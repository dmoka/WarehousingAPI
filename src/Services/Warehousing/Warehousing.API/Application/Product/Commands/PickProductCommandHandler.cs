using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.Core.Domain;
using Warehousing.Domain.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class PickProductCommandHandler : AbstractCommandHandler<PickProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public PickProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IValidator<PickProductCommand> validator) : base(validator)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public override async Task<Result<bool>> HandleRequest(PickProductCommand request)
        {
            var product = await _productRepository.GetAsync(request.Id);

            product.Pick(request.Quantity);

            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}
