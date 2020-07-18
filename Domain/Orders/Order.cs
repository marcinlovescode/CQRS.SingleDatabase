using System;

namespace Domain.Orders
{
    public class Order
    {
        private Guid? _discountId;
        private readonly decimal _discountValue;
        private readonly decimal _totalValue;
        private readonly decimal _value;

        private Order()
        {
        }

        protected internal Order(Guid id, Guid discountId, decimal value, decimal discountValue, decimal totalValue)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (discountValue < 0)
                throw new InvalidOperationException("Value cannot be below 0");

            if (discountValue > value)
                throw new InvalidOperationException("Discount value cannot be greater than value");

            if (value < 0)
                throw new InvalidOperationException("Value cannot be below 0");

            Id = id;
            _discountId = discountId;
            _value = value;
            _discountValue = discountValue;
            _totalValue = totalValue;
        }

        protected internal Order(Guid id, decimal value)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (value < 0)
                throw new InvalidOperationException("Value cannot be below 0");

            Id = id;
            _value = value;
            _discountValue = 0;
            _totalValue = value;
        }

        public Guid Id { get; }

        public Summary Summary => new Summary(_value, _discountValue, _totalValue);
    }
}