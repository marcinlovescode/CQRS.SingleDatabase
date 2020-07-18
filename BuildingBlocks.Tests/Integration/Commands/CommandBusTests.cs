using System;
using System.Threading.Tasks;
using BuildingBlocks.Commands;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BuildingBlocks.Tests.Integration.Commands
{
    public class CommandBusTests
    {
        [Test]
        public async Task Given_TestCommand_When_SendAsync_Then_CommandHandlerIsFired()
        {
            //Arrange
            var services = new ServiceCollection();
            var command = new TestCommand();
            var commandHandler = new TestCommandHandler();
            services.AddScoped<ICommandHandler<TestCommand>>(provider => commandHandler);
            var serviceProvider = services.BuildServiceProvider();
            var commandBus = new CommandBus(serviceProvider.GetService);
            //Act
            await commandBus.SendAsync(command);
            //Assert
            Assert.That(commandHandler.IsFired, Is.True);
        }

        [Test]
        public void
            Given_TestCommand_AndTestCommandHandlerNotRegistered_When_SendAsync_Then_ActivationExceptionIsThrown()
        {
            //Arrange
            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();
            var command = new TestCommand();
            var commandBus = new CommandBus(serviceProvider.GetService);
            //Act
            Func<Task> handleAction = () => commandBus.SendAsync(command);
            //Assert
            Assert.ThrowsAsync<HandlerNotFoundException>(() => handleAction.Invoke());
        }
    }
}