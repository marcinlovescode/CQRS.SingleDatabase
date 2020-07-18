using System.Threading;
using System.Threading.Tasks;
using Application.Orders.Projections;
using Application.Orders.Queries;
using Application.Orders.Repositories;
using BuildingBlocks.Queries;

namespace Application.Orders.QueryHandlers
{
    public class RetrieveOrderHandler : IQueryHandler<RetrieveOrder, OrderProjection>
    {
        private readonly IOrderProjectionRepository _orderProjectionRepository;

        public RetrieveOrderHandler(IOrderProjectionRepository orderProjectionRepository)
        {
            _orderProjectionRepository = orderProjectionRepository;
        }

        public Task<OrderProjection> HandleAsync(RetrieveOrder query, CancellationToken cancellationToken = default)
        {
            return _orderProjectionRepository.Find(query.Id, cancellationToken);
        }
    }
}