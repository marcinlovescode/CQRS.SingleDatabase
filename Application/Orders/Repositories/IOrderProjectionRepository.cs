using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Orders.Projections;

namespace Application.Orders.Repositories
{
    public interface IOrderProjectionRepository
    {
        Task<OrderProjection> Find(Guid id, CancellationToken cancellationToken = default);
    }
}