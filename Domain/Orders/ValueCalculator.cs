using Domain.Discounts;

namespace Domain.Orders
{
    public interface IValueCalculator
    {
        decimal CalculateDiscount(decimal value, Discount discount);
    }

    public class ValueCalculator : IValueCalculator
    {
        public decimal CalculateDiscount(decimal value, Discount discount)
        {
            if (value > 0)
                return 0.2M * value;
            return 0;
        }
    }
}