using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.Core.Domain;
using Warehousing.Domain.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class UpdateProductCommandHandler : AbstractCommandHandler<UpdateProductCommand, Result<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            IValidator<UpdateProductCommand> validator, 
            IMapper mapper, 
            IProductRepository productRepository,
            IUnitOfWork unitOfWork) : base(validator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<Result<bool>> HandleRequest(UpdateProductCommand productInRequest)
        {
            var product = _mapper.Map<Domain.Product.Product>(productInRequest);

            _productRepository.Update(product);

            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}
