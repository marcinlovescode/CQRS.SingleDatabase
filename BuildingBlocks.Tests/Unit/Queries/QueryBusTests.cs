using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Queries;
using BuildingBlocks.Tests.Fixtures;
using NUnit.Framework;

namespace BuildingBlocks.Tests.Unit.Queries
{
    public class QueryBusTests
    {
        [Test]
        public async Task Given_TestQuery_When_SendAsync_Then_ResultIsReturned()
        {
            //Arrange
            var query = new TestQuery
            {
                ReturnValue = "ReturnValue"
            };
            var queryHandler = new TestQueryHandler();
            Func<Type, object> resolver = type => { return queryHandler; };
            var queryBus = new QueryBus(resolver);
            //Act
            var result = await queryBus.SendAsync(query, CancellationToken.None);
            //Assert
            Assert.That(result, Is.EqualTo(query.ReturnValue));
        }

        [Test]
        public void
            Given_TestQuery_AndTestQueryHandlerNotRegistered_When_SendAsync_Then_HandlerNotFoundExceptionIsThrown()
        {
            //Arrange
            var query = new TestQuery
            {
                ReturnValue = "ReturnValue"
            };
            Func<Type, object> resolver = type => null;
            var queryBus = new QueryBus(resolver);
            //Act
            Func<Task> handleAction = () => queryBus.SendAsync(query);
            //Assert
            Assert.ThrowsAsync<HandlerNotFoundException>(() => handleAction.Invoke());
        }
    }
}