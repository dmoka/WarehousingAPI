using AutoMapper;
using Warehousing.API.Application.Product.Commands;

namespace Warehousing.API.Application.Config
{
    public class WarehousingAutomapperProfile : Profile
    {
        public WarehousingAutomapperProfile()
        {
            CreateMap<UpdateProductCommand, Domain.Product.Product>();
        }
    }
}
