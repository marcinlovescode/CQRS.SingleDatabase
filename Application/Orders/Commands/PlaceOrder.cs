using System;
using BuildingBlocks.Commands;

namespace Application.Orders.Commands
{
    public class PlaceOrder : ICommand
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string DiscountCode { get; set; }
    }
}