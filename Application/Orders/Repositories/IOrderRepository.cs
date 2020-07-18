using System.Threading;
using System.Threading.Tasks;
using Domain.Orders;

namespace Application.Orders.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order, CancellationToken cancellationToken = default);
    }
}