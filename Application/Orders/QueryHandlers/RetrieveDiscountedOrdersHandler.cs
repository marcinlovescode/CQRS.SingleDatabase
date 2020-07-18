using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Orders.Projections;
using Application.Orders.Queries;
using Application.Orders.Repositories;
using BuildingBlocks.Queries;

namespace Application.Orders.QueryHandlers
{
    public class RetrieveDiscountedOrdersHandler : IQueryHandler<RetrieveOrdersFromNewsletter, ICollection<OrderFromNewsletterProjection>>
    {
        private readonly IDiscountedOrderProjectionRepository _discountedOrderProjectionRepository;

        public RetrieveDiscountedOrdersHandler(IDiscountedOrderProjectionRepository discountedOrderProjectionRepository)
        {
            _discountedOrderProjectionRepository = discountedOrderProjectionRepository;
        }

        public Task<ICollection<OrderFromNewsletterProjection>> HandleAsync(RetrieveOrdersFromNewsletter query, CancellationToken cancellationToken = default)
        {
            return _discountedOrderProjectionRepository.List(cancellationToken);
        }
    }
}