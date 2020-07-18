namespace Domain.Orders
{
    public struct Summary
    {
        public decimal Value { get; }
        public decimal DiscountValue { get; }
        public decimal TotalValue { get; }

        public Summary(decimal value, decimal discountValue, decimal totalValue)
        {
            Value = value;
            DiscountValue = discountValue;
            TotalValue = totalValue;
        }
    }
}