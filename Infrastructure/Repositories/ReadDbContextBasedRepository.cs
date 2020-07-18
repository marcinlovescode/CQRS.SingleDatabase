using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Orders.Projections;
using Application.Orders.Repositories;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReadDbContextBasedRepository : IOrderProjectionRepository, IDiscountedOrderProjectionRepository
    {
        private readonly ReadDbContext _dbContext;

        public ReadDbContextBasedRepository(ReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<OrderFromNewsletterProjection>> List(CancellationToken cancellationToken = default)
        {
            return await _dbContext.DiscountedOrders.ToListAsync(cancellationToken);
        }

        public Task<OrderProjection> Find(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Orders.SingleOrDefaultAsync(discount => discount.OrderId == id, cancellationToken);
        }
    }
}