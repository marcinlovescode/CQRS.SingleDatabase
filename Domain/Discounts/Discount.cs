using System;

namespace Domain.Discounts
{
    public class Discount
    {
        private Guid? _subscriberId;

        private Discount()
        {
        }

        protected internal Discount(Guid id, string code, Guid subscriberId)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");
            if (subscriberId == default)
                throw new ArgumentException("Value cannot be equal to default");
            Id = id;
            Code = code;
            _subscriberId = subscriberId;
        }

        public Guid Id { get; }

        public string Code { get; }

        public string GetCode()
        {
            return Code;
        }
    }
}