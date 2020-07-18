using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Discounts.Repositories;
using Application.Orders.CommandHandlers;
using Application.Orders.Commands;
using Application.Orders.Repositories;
using Domain.Discounts;
using Domain.Orders;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Unit.Orders.CommandHandlers
{
    public class PlaceOrderHandlerTests
    {
        private Mock<IDiscountRepository> _discountRepositoryMock;
        private Mock<IIdentityProvider> _identityProviderMock;
        private IOrderProcess _orderProcess;
        private Mock<IOrderRepository> _orderRepositoryMock;

        private PlaceOrderHandler _sut;
        private Mock<IValueCalculator> _valueCalculatorMock;

        [SetUp]
        public void SetUp()
        {
            _discountRepositoryMock = new Mock<IDiscountRepository>();
            _identityProviderMock = new Mock<IIdentityProvider>();
            _identityProviderMock.Setup(x => x.Next())
                .Returns(Guid.NewGuid());
            _valueCalculatorMock = new Mock<IValueCalculator>();
            _valueCalculatorMock.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<Discount>()))
                .Returns<decimal, Discount>(CalculateDiscount);
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _discountRepositoryMock = new Mock<IDiscountRepository>();
            _orderProcess = new OrderProcess(_valueCalculatorMock.Object);
            _sut = new PlaceOrderHandler(_discountRepositoryMock.Object, _orderRepositoryMock.Object, _orderProcess);
        }

        [Test]
        public async Task Given_PlaceOrderCommandWithoutDiscountCode_When_HandleAsync_Then_OrderIsCreatedAndAddedToRepository()
        {
            //Arrange
            var command = new PlaceOrder
            {
                Id = Guid.NewGuid(),
                Value = 100
            };
            Order placedOrder = null;
            _orderRepositoryMock.Setup(x => x.Add(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                .Callback<Order, CancellationToken>((order, cancellationToken) => { placedOrder = order; });
            //Act
            await _sut.HandleAsync(command, CancellationToken.None);
            //Assert
            Assert.That(placedOrder.Summary.Value, Is.EqualTo(command.Value));
            Assert.That(placedOrder.Summary.TotalValue, Is.EqualTo(command.Value));
        }

        [Test]
        public async Task Given_PlaceOrderCommandWithDiscountCode_When_HandleAsync_Then_DiscountedOrderIsCreatedAndAddedToRepository()
        {
            //Arrange
            var discountCodeGenerator = new DiscountCodeGenerator();
            var sampleDiscount = discountCodeGenerator.GenerateCodeForSubscriber(_identityProviderMock.Object.Next(), Guid.NewGuid());
            var command = new PlaceOrder
            {
                Id = Guid.NewGuid(),
                Value = 100,
                DiscountCode = sampleDiscount.GetCode()
            };
            Order placedOrder = null;
            _discountRepositoryMock.Setup(x => x.FindByCode(It.Is<string>(discountCode => discountCode == sampleDiscount.GetCode()), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sampleDiscount);
            _orderRepositoryMock.Setup(x => x.Add(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                .Callback<Order, CancellationToken>((order, cancellationToken) => { placedOrder = order; });
            //Act
            await _sut.HandleAsync(command, CancellationToken.None);
            //Assert
            Assert.That(placedOrder.Summary.Value, Is.EqualTo(command.Value));
            Assert.That(placedOrder.Summary.DiscountValue, Is.EqualTo(CalculateDiscount(command.Value, sampleDiscount)));
            Assert.That(placedOrder.Summary.TotalValue, Is.EqualTo(command.Value - CalculateDiscount(command.Value, sampleDiscount)));
        }

        [Test]
        public void Given_PlaceOrderCommandWithDiscountCodeButDiscountCodeDoensntExist_When_HandleAsync_Then_InvalidOperationExceptionIsThrown()
        {
            //Arrange
            var command = new PlaceOrder
            {
                Value = 100,
                DiscountCode = "XAXAX"
            };
            _discountRepositoryMock.Setup(x => x.FindByCode(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(Discount));
            //Act
            Func<Task> action = async () => await _sut.HandleAsync(command, CancellationToken.None);
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => action.Invoke());
        }

        private static decimal CalculateDiscount(decimal value, Discount discount)
        {
            if (value > 0)
                return 0.2M * value;
            return 0;
        }
    }
}