using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Discounts.Repositories;
using Application.Orders.Commands;
using Application.Orders.Repositories;
using BuildingBlocks.Commands;
using Domain.Orders;

namespace Application.Orders.CommandHandlers
{
    public class PlaceOrderHandler : ICommandHandler<PlaceOrder>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderProcess _orderProcess;
        private readonly IOrderRepository _orderRepository;

        public PlaceOrderHandler(IDiscountRepository discountRepository, IOrderRepository orderRepository, IOrderProcess orderProcess)
        {
            _discountRepository = discountRepository;
            _orderRepository = orderRepository;
            _orderProcess = orderProcess;
        }

        public async Task HandleAsync(PlaceOrder command, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(command.DiscountCode))
                await HandleOrderWithDiscountCode(command, cancellationToken);
            else
                await HandleOrder(command, cancellationToken);
        }

        private async Task HandleOrderWithDiscountCode(PlaceOrder command, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.FindByCode(command.DiscountCode, cancellationToken);
            if (discount == null)
                throw new InvalidOperationException($"Couldn't find discount of following code: {command.DiscountCode}");
            var order = _orderProcess.PlaceOrder(command.Id, command.Value, discount);
            await _orderRepository.Add(order, cancellationToken);
        }

        private async Task HandleOrder(PlaceOrder command, CancellationToken cancellationToken)
        {
            var order = _orderProcess.PlaceOrder(command.Id, command.Value);
            await _orderRepository.Add(order, cancellationToken);
        }
    }
}