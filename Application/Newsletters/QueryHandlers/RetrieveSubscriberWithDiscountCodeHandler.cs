using System.Threading;
using System.Threading.Tasks;
using Application.Newsletters.Projections;
using Application.Newsletters.Queries;
using Application.Newsletters.Repositories;
using BuildingBlocks.Queries;

namespace Application.Newsletters.QueryHandlers
{
    public class RetrieveSubscriberWithDiscountCodeHandler : IQueryHandler<RetrieveSubscriberWithDiscountCode, SubscriberDiscountProjection>
    {
        private readonly ISubscriberDiscountProjectionRepository _subscriberDiscountProjectionRepository;

        public RetrieveSubscriberWithDiscountCodeHandler(ISubscriberDiscountProjectionRepository subscriberDiscountProjectionRepository)
        {
            _subscriberDiscountProjectionRepository = subscriberDiscountProjectionRepository;
        }

        public Task<SubscriberDiscountProjection> HandleAsync(RetrieveSubscriberWithDiscountCode query, CancellationToken cancellationToken = default)
        {
            return _subscriberDiscountProjectionRepository.Find(query.SubscriberId, cancellationToken);
        }
    }
}