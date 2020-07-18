using System;
using Application.Newsletters.Projections;
using BuildingBlocks.Queries;

namespace Application.Newsletters.Queries
{
    public class RetrieveSubscriberWithDiscountCode : IQuery<SubscriberDiscountProjection>
    {
        public Guid SubscriberId { get; set; }
    }
}