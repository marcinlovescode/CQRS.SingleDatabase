using System.Threading.Tasks;
using Application.Orders.Queries;
using BuildingBlocks.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/discounted-orders")]
    [Produces("application/json")]
    public class DiscountedOrdersController : ControllerBase
    {
        private readonly IQueryBus _queryBus;

        public DiscountedOrdersController(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        [HttpGet(Name = "GetDiscountedOrders")]
        public async Task<IActionResult> GetDiscountedOrders()
        {
            var query = new RetrieveOrdersFromNewsletter();
            return Ok(await _queryBus.SendAsync(query));
        }
    }
}