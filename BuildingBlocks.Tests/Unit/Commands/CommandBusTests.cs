using System;
using System.Threading.Tasks;
using BuildingBlocks.Commands;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Tests.Fixtures;
using NUnit.Framework;

namespace BuildingBlocks.Tests.Unit.Commands
{
    public class CommandBusTests
    {
        [Test]
        public async Task Given_TestCommand_When_SendAsync_Then_CommandHandlerIsFired()
        {
            //Arrange
            var command = new TestCommand();
            var commandHandler = new TestCommandHandler();
            Func<Type, object> resolver = type => { return commandHandler; };
            var commandBus = new CommandBus(resolver);
            //Act
            await commandBus.SendAsync(command);
            //Assert
            Assert.That(commandHandler.IsFired, Is.True);
        }

        [Test]
        public void
            Given_TestCommand_AndTestCommandHandlerNotRegistered_When_SendAsync_Then_HandlerNotFoundExceptionIsThrown()
        {
            //Arrange
            var command = new TestCommand();
            Func<Type, object> resolver = type => { return null; };
            var commandBus = new CommandBus(resolver);
            //Act
            Func<Task> handleAction = () => commandBus.SendAsync(command);
            //Assert
            Assert.ThrowsAsync<HandlerNotFoundException>(() => handleAction.Invoke());
        }
    }
}