using System;
using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.Core.Domain;
using Warehousing.Domain.Product;

namespace Warehousing.API.Application.Product.Commands
{
    public class CreateProductCommandHandler : AbstractCommandHandler<CreateProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IValidator<CreateProductCommand> validator) : base(validator)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public override async Task<Result<bool>> HandleRequest(CreateProductCommand request)
        {
            var product = CreateProduct(request);

            _productRepository.Insert(product);

            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }

        private static Domain.Product.Product CreateProduct(CreateProductCommand request)
        {
            return new ProductBuilder()
                .WithName(request.Name)
                .WithType(request.Type)
                .WithArticleNumber(request.ArticleNumber)
                .WithCustomTariffNumer(request.CustomTariffNumber)
                .WithQuantity(request.Quantity)
                .WithUnit(request.Unit)
                .WithNetUnitPrice(request.NetUnitPrice)
                .WithNetValue(request.NetValue)
                .WithVat(request.Vat)
                .WithVatSum(request.VatSum)
                .WithGrossUnitPrice(request.GrossUnitPrice)
                .WithGrossValue(request.GrossValue)
                .WithNotes(request.Notes)
                .Build();
        }
    }
}
