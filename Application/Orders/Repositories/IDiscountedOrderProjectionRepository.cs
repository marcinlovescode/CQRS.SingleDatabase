using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Orders.Projections;

namespace Application.Orders.Repositories
{
    public interface IDiscountedOrderProjectionRepository
    {
        Task<ICollection<OrderFromNewsletterProjection>> List(CancellationToken cancellationToken = default);
    }
}