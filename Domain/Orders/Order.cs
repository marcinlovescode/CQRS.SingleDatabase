using System;
using Domain.Discounts;

namespace Domain.Orders
{
    public class Order
    {
        private Guid? _discountId;
        private readonly decimal _discountValue;
        private readonly decimal _totalValue;
        private readonly decimal _value;

        public Guid Id { get; }
        public Summary Summary => new Summary(_value, _discountValue, _totalValue);

        private Order()
        {
        }

        private Order(Guid id, Guid discountId, decimal value, decimal discountValue, decimal totalValue)
        {
            Id = id;
            _discountId = discountId;
            _value = value;
            _discountValue = discountValue;
            _totalValue = totalValue;
        }

        private Order(Guid id, decimal value)
        {
            Id = id;
            _value = value;
            _discountValue = 0;
            _totalValue = value;
        }

        public static Order PlaceDiscountedOrder(Guid id, decimal value, Discount discount, IValueCalculator valueCalculator)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            if (value < 0)
                throw new InvalidOperationException("Value cannot be below 0");

            var discountValue = valueCalculator.CalculateDiscount(value, discount);

            return new Order(id, discount.Id, value, discountValue, value - discountValue);
        }

        public static Order PlaceOrder(Guid id, decimal value)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (value < 0)
                throw new InvalidOperationException("Value cannot be below 0");

            return new Order(id, value);
        }
    }
}