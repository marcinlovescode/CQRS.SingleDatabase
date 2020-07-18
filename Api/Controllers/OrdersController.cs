using System;
using System.Threading.Tasks;
using Api.Dtos;
using Application;
using Application.Orders.Commands;
using Application.Orders.Queries;
using BuildingBlocks.Commands;
using BuildingBlocks.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/orders")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IIdentityProvider _identityProvider;
        private readonly IQueryBus _queryBus;

        public OrdersController(IIdentityProvider identityProvider, ICommandBus commandBus, IQueryBus queryBus)
        {
            _identityProvider = identityProvider;
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var query = new RetrieveOrder
            {
                Id = id
            };
            return Ok(await _queryBus.SendAsync(query));
        }

        [HttpPost(Name = "PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto placeOrder)
        {
            var orderId = _identityProvider.Next();
            await _commandBus.SendAsync(new PlaceOrder
            {
                Id = orderId,
                Value = placeOrder.Value,
                DiscountCode = placeOrder.DiscountCode
            });
            return CreatedAtAction(nameof(GetOrder), "orders", new
                {
                    id = orderId
                },
                null
            );
        }
    }
}