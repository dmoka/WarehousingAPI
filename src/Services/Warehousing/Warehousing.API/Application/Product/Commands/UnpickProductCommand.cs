using System;
using KaliGasService.Core.Application.CQRS;

namespace Warehousing.API.Application.Product.Commands
{
    public class UnpickProductCommand : Command<Result<bool>>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        public UnpickProductCommand(Guid id, string name, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
