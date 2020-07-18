using System.Threading;
using System.Threading.Tasks;
using Domain.Discounts;

namespace Application.Discounts.Repositories
{
    public interface IDiscountRepository
    {
        Task Add(Discount discount, CancellationToken cancellationToken = default);
        Task<Discount> FindByCode(string discountCode, CancellationToken cancellationToken = default);
    }
}