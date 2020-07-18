using System;

namespace Domain.Discounts
{
    public interface IDiscountCodeGenerator
    {
        Discount GenerateCodeForSubscriber(Guid id, Guid newsletterId);
    }

    public class DiscountCodeGenerator : IDiscountCodeGenerator
    {
        public Discount GenerateCodeForSubscriber(Guid id, Guid subscriberId)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (subscriberId == default)
                throw new ArgumentException("Value cannot be equal to default");

            return new Discount(id, MakeCode(), subscriberId);
        }

        private static string MakeCode()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}