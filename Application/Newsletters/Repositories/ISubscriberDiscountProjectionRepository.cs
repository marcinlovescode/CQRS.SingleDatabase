using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Newsletters.Projections;

namespace Application.Newsletters.Repositories
{
    public interface ISubscriberDiscountProjectionRepository
    {
        Task<SubscriberDiscountProjection> Find(Guid id, CancellationToken cancellationToken = default);
    }
}