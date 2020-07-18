using System;

namespace Application.Orders.Projections
{
    public class OrderFromNewsletterProjection
    {
        public Guid OrderId { get; set; }
        public decimal Value { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TotalValue { get; set; }
        public string SubscriberEmail { get; set; }
    }
}