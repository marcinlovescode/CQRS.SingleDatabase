using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Queries;

namespace BuildingBlocks.Tests.Fixtures
{
    public class TestQuery : IQuery<string>
    {
        public string ReturnValue { get; set; }
    }

    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public Task<string> HandleAsync(TestQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(query.ReturnValue);
        }
    }
}