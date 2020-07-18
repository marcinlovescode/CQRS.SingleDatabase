using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Newsletters.Projections;
using Application.Newsletters.Repositories;
using Dapper;
using Infrastructure.SqlConnections;

namespace Infrastructure.Repositories
{
    public class DapperBasedSubscriberDiscountProjectionRepository : ISubscriberDiscountProjectionRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public DapperBasedSubscriberDiscountProjectionRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<SubscriberDiscountProjection> Find(Guid id, CancellationToken cancellationToken = default)
        {
            var connection = _sqlConnectionFactory.GetConnection();
            var sql = new[]
            {
                "SELECT DISTINCT",
                "discount.Code as DiscountCode,",
                "subscriber.Email",
                "FROM",
                "Discounts discount",
                "JOIN",
                "Subscribers subscriber",
                "ON",
                "discount.SubscriberId = subscriber.Id",
                "WHERE",
                "subscriber.Id = @SubscriberId"
            };
            var subscriberDiscountProjections = await connection.QueryAsync<SubscriberDiscountProjection>(string.Join(" ", sql), new {SubscriberId = id});
            return subscriberDiscountProjections.SingleOrDefault();
        }
    }
}