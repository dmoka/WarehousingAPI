using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warehousing.API.Application.Errors;
using Warehousing.API.Application.Product.Commands;
using Warehousing.API.Application.Product.Queries;
using Warehousing.Data.Entities.Product;

namespace Warehousing.API.Application.Product.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var queryResult = await _mediator.Send(new GetAllProductsQuery());

            if (!queryResult.IsSuccess)
            {
                _logger.LogError(ErrorFormatter.Format(queryResult.Errors));

                return ApiErrorResult.Create(StatusCodes.Status500InternalServerError, queryResult.Errors);
            }

            return Ok(queryResult.Payload);
        }

        /// <summary>
        /// Create product
        /// </summary>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct(CreateProductCommand request)
        {
            var commandResult = await _mediator.Send(request);

            if (!commandResult.IsSuccess)
            {
                _logger.LogError(ErrorFormatter.Format(commandResult.Errors));

                return ApiErrorResult.Create(StatusCodes.Status500InternalServerError, commandResult.Errors);
            }

            return Created(string.Empty, null);
        }


        /// <summary>
        /// Update product
        /// </summary>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand request)
        {
            var commandResult = await _mediator.Send(request);

            if (!commandResult.IsSuccess)
            {
                _logger.LogError(ErrorFormatter.Format(commandResult.Errors));

                return ApiErrorResult.Create(StatusCodes.Status500InternalServerError, commandResult.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Action for picking product with given quantity
        /// </summary>
        [HttpPost]
        [Route("{productId}/pick")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> PickProduct([FromRoute]Guid productId, [FromBody]PickProductRequest request)
        {
            var commandResult = await _mediator.Send(new PickProductCommand(productId, request.Name, request.Quantity));

            if (!commandResult.IsSuccess)
            {
                _logger.LogError(ErrorFormatter.Format(commandResult.Errors));

                return ApiErrorResult.Create(StatusCodes.Status500InternalServerError, commandResult.Errors);
            }

            return Accepted();
        }

        /// <summary>
        /// Action for unpicking product with given quantity
        /// </summary>
        [HttpPost]
        [Route("{productId}/unpick")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> UnpickProduct([FromRoute]Guid productId, [FromBody]PickProductRequest request)
        {
            var commandResult = await _mediator.Send(new UnpickProductCommand(productId, request.Name, request.Quantity));

            if (!commandResult.IsSuccess)
            {
                _logger.LogError(ErrorFormatter.Format(commandResult.Errors));

                return ApiErrorResult.Create(StatusCodes.Status500InternalServerError, commandResult.Errors);
            }

            return Accepted();
        }

        /// <summary>
        /// Get all product history lines for the given product
        /// </summary>
        [HttpGet]
        [Route("{productId}/history")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductHistoryLines([FromRoute]Guid productId)
        {
            var queryResult = await _mediator.Send(new GetAllProductHistoryLinesQuery(productId));

            if (!queryResult.IsSuccess)
            {
                _logger.LogError(ErrorFormatter.Format(queryResult.Errors));

                return ApiErrorResult.Create(StatusCodes.Status500InternalServerError, queryResult.Errors);
            }

            return Ok(queryResult.Payload);
        }
    }
}
