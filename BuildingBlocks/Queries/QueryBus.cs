using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Exceptions;

namespace BuildingBlocks.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly Func<Type, object> _handler;

        public QueryBus(Func<Type, object> handler)
        {
            _handler = handler;
        }

        public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var queryBusInternal = (QueryBusInternal<TResult>) Activator.CreateInstance(
                typeof(QueryBusInternalImpl<,>).MakeGenericType(query.GetType(), typeof(TResult)), _handler);
            return await queryBusInternal.SendAsync(query, cancellationToken);
        }
    }

    internal abstract class QueryBusInternal<TResult>
    {
        private readonly Func<Type, object> _handlersResolver;

        protected QueryBusInternal(Func<Type, object> typeResolver)
        {
            _handlersResolver = typeResolver;
        }

        protected THandler GetHandler<THandler>()
        {
            var handler = _handlersResolver.Invoke(typeof(THandler));
            if (handler == null)
                throw new HandlerNotFoundException();
            return (THandler) handler;
        }

        public abstract Task<TResult> SendAsync(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }

    internal class QueryBusInternalImpl<TQuery, TResult> : QueryBusInternal<TResult>
        where TQuery : IQuery<TResult>
    {
        public QueryBusInternalImpl(Func<Type, object> handler) : base(handler)
        {
        }

        public override async Task<TResult> SendAsync(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            return await GetHandler<IQueryHandler<TQuery, TResult>>()
                .HandleAsync((TQuery) query, cancellationToken);
        }
    }
}