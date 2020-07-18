using System;
using System.Threading.Tasks;
using Api.Dtos;
using Application;
using Application.Newsletters.Commands;
using Application.Newsletters.Queries;
using BuildingBlocks.Commands;
using BuildingBlocks.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/subscriptions")]
    [Produces("application/json")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IIdentityProvider _identityProvider;
        private readonly IQueryBus _queryBus;

        public SubscriptionsController(IIdentityProvider identityProvider, ICommandBus commandBus, IQueryBus queryBus)
        {
            _identityProvider = identityProvider;
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet("{id}", Name = "GetSubscription")]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            var query = new RetrieveSubscriberWithDiscountCode
            {
                SubscriberId = id
            };
            return Ok(await _queryBus.SendAsync(query));
        }

        [HttpPost(Name = "SubscribeToNewsletter")]
        public async Task<IActionResult> SubscribeToNewsletter([FromBody] SubscribeToTheNewsletterDto subscribeToTheNewsletter)
        {
            var newsletterId = _identityProvider.Next();
            await _commandBus.SendAsync(new SubscribeToTheNewsletter
            {
                Email = subscribeToTheNewsletter.Email,
                Id = newsletterId
            });
            return CreatedAtAction(nameof(GetSubscription), "subscriptions", new
                {
                    id = newsletterId
                },
                null
            );
        }
    }
}