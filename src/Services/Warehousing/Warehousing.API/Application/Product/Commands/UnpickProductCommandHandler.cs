using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.Core.Domain;
using Warehousing.Domain.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class UnpickProductCommandHandler : AbstractCommandHandler<UnpickProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public UnpickProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IValidator<UnpickProductCommand> validator) : base(validator)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public override async Task<Result<bool>> HandleRequest(UnpickProductCommand request)
        {
            var product = await _productRepository.GetAsync(request.Id);

            product.Unpick(request.Quantity);

            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}
