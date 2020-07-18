using System;
using Application.Orders.Projections;
using BuildingBlocks.Queries;

namespace Application.Orders.Queries
{
    public class RetrieveOrder : IQuery<OrderProjection>
    {
        public Guid Id { get; set; }
    }
}