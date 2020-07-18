using Application;
using Application.Discounts.Repositories;
using Application.Mailing;
using Application.Newsletters.Repositories;
using Application.Orders.Repositories;
using BuildingBlocks.Commands;
using BuildingBlocks.Queries;
using Domain.Discounts;
using Domain.Orders;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.SqlConnections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class CompositionRoot
    {
        internal static void Compose(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ComposeHandlers(serviceCollection);
            ComposeBuses(serviceCollection);
            ComposeRepositories(serviceCollection);
            ComposeServices(serviceCollection);
            serviceCollection.AddScoped<ISqlConnectionFactory>(serviceProvider =>
                new SqlConnectionFactory(configuration.GetConnectionString("CqrsSingleDatabaseContext")));
        }

        private static void ComposeRepositories(IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, WriteDbContextBasedRepository>();
            services.AddScoped<INewsletterRepository, WriteDbContextBasedRepository>();
            services.AddScoped<IDiscountRepository, WriteDbContextBasedRepository>();
            services.AddScoped<ISubscriberDiscountProjectionRepository, DapperBasedSubscriberDiscountProjectionRepository>();
            services.AddScoped<IOrderProjectionRepository, ReadDbContextBasedRepository>();
            services.AddScoped<IDiscountedOrderProjectionRepository, ReadDbContextBasedRepository>();
        }

        private static void ComposeBuses(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICommandBus>(serviceProvider => new CommandBus(serviceProvider.GetService));
            serviceCollection.AddScoped<IQueryBus>(serviceProvider => new QueryBus(serviceProvider.GetService));
        }

        private static void ComposeHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan
                .FromAssemblyOf<IApplicationAssemblyMarker>()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }

        private static void ComposeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IIdentityProvider, GuidBasedIdentityProvider>();
            serviceCollection.AddScoped<IValueCalculator, ValueCalculator>();
            serviceCollection.AddScoped<IEmailService, EmptyMailingService>();
            serviceCollection.AddScoped<IDiscountCodeGenerator, DiscountCodeGenerator>();
            serviceCollection.AddScoped<IOrderProcess, OrderProcess>();
        }
    }
}