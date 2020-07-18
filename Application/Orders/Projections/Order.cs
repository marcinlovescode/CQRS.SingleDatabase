using System;

namespace Application.Orders.Projections
{
    public class OrderProjection
    {
        public Guid OrderId { get; set; }
        public decimal Value { get; set; }
    }
}