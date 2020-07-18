using System.Collections.Generic;
using Application.Orders.Projections;
using BuildingBlocks.Queries;

namespace Application.Orders.Queries
{
    public class RetrieveOrdersFromNewsletter : IQuery<ICollection<OrderFromNewsletterProjection>>
    {
    }
}