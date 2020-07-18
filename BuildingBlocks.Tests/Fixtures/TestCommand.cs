using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Commands;

namespace BuildingBlocks.Tests.Fixtures
{
    public class TestCommand : ICommand
    {
    }

    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public bool IsFired { get; set; }

        public async Task HandleAsync(TestCommand command, CancellationToken cancellationToken)
        {
            IsFired = true;
            await Task.Yield();
        }
    }
}