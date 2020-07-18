using System;
using Domain.Discounts;

namespace Domain.Orders
{
    public interface IOrderProcess
    {
        Order PlaceOrder(Guid id, decimal value);
        Order PlaceOrder(Guid id, decimal value, Discount discount);
    }

    public class OrderProcess : IOrderProcess
    {
        private readonly IValueCalculator _valueCalculator;

        public OrderProcess(IValueCalculator valueCalculator)
        {
            _valueCalculator = valueCalculator;
        }

        public Order PlaceOrder(Guid id, decimal value, Discount discount)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            if (value < 0)
                throw new InvalidOperationException("Value cannot be below 0");

            var discountValue = _valueCalculator.CalculateDiscount(value, discount);

            return new Order(id, discount.Id, value, discountValue, value - discountValue);
        }

        public Order PlaceOrder(Guid id, decimal value)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (value < 0)
                throw new InvalidOperationException("Value cannot be below 0");

            return new Order(id, value);
        }
    }
}