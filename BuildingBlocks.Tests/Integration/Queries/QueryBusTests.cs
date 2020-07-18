using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Queries;
using BuildingBlocks.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BuildingBlocks.Tests.Integration.Queries
{
    public class QueryBusTests
    {
        [Test]
        public async Task Given_TestQuery_When_SendAsync_Then_ResultIsReturned()
        {
            //Arrange
            var services = new ServiceCollection();
            var query = new TestQuery
            {
                ReturnValue = "ReturnValue"
            };
            var queryHandler = new TestQueryHandler();
            services.AddScoped<IQueryHandler<TestQuery, string>>(provider => queryHandler);
            var serviceProvider = services.BuildServiceProvider();
            var queryBus = new QueryBus(serviceProvider.GetService);
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
            var services = new ServiceCollection();
            var query = new TestQuery
            {
                ReturnValue = "ReturnValue"
            };
            var serviceProvider = services.BuildServiceProvider();
            var queryBus = new QueryBus(serviceProvider.GetService);
            //Act
            Func<Task> handleAction = () => queryBus.SendAsync(query);
            //Assert
            Assert.ThrowsAsync<HandlerNotFoundException>(() => handleAction.Invoke());
        }
    }
}