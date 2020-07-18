using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Exceptions;

namespace BuildingBlocks.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, object> _handlersResolver;

        public CommandBus(Func<Type, object> typeResolver)
        {
            _handlersResolver = typeResolver;
        }

        public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            var handler = GetHandler(command);
            if (handler == null)
                throw new HandlerNotFoundException();
            await handler.HandleAsync(command, cancellationToken);
        }

        private ICommandHandler<TCommand> GetHandler<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            return (ICommandHandler<TCommand>) _handlersResolver.Invoke(handlerType);
        }
    }
}