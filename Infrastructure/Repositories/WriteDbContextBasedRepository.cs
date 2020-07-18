using System.Threading;
using System.Threading.Tasks;
using Application.Discounts.Repositories;
using Application.Newsletters.Repositories;
using Application.Orders.Repositories;
using Domain.Discounts;
using Domain.Newsletters;
using Domain.Orders;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WriteDbContextBasedRepository :
        IDiscountRepository, INewsletterRepository, IOrderRepository
    {
        private readonly WriteDbContext _dbContext;

        public WriteDbContextBasedRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Discount discount, CancellationToken cancellationToken = default)
        {
            await _dbContext.Discounts.AddAsync(discount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Discount> FindByCode(string discountCode, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Discounts.SingleOrDefaultAsync(discount => discount.Code.Equals(discountCode), cancellationToken);
        }

        public async Task Add(Subscriber subscriber, CancellationToken cancellationToken = default)
        {
            await _dbContext.Subscribers.AddAsync(subscriber, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Add(Order order, CancellationToken cancellationToken = default)
        {
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}