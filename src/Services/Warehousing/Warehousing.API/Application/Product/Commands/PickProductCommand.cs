using System;
using KaliGasService.Core.Application.CQRS;

namespace Warehousing.API.Application.Product.Commands
{
    public class PickProductCommand : Command<Result<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }


        public PickProductCommand(Guid id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }
    }
}
