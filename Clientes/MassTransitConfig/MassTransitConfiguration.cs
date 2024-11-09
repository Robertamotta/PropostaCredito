using Clientes.Api.Consumers;
using Clientes.Dominio.Entidades;
using MassTransit;
using System.Diagnostics.CodeAnalysis;
using System.Security.Authentication;

namespace Clientes.Api.MassTransitConfig;

[ExcludeFromCodeCoverage]
public static class MassTransitConfiguration
{
    public static void ConfigureMasTransit(this IServiceCollection services)
    {
        services.ConfigurarRabbitMQ();
    }

    private static void ConfigurarRabbitMQ(this IServiceCollection services)
    {
        var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
        var vhost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST");
        ushort.TryParse(Environment.GetEnvironmentVariable("RABBITMQ_PORT"), out ushort port);
        var username = Environment.GetEnvironmentVariable("RABBITMQ_USER");
        var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
        bool.TryParse(Environment.GetEnvironmentVariable("RABBITMQ_USE_SSL"), out bool useSsl);

        services.AddMassTransit(x =>
        {
            x.AddConsumer<PropostaCreditoConsumer>();
            x.AddConsumer<EmissaoCartaoCreditoConsumer>();

            x.UsingRabbitMq((context, cfg) =>
           {
               cfg.Host(host, port, vhost, h =>
               {
                   h.Username(username);
                   h.Password(password);
                   if (useSsl)
                       h.UseSsl(s => s.Protocol = SslProtocols.Tls12);
                   h.ConfigureBatchPublish(b => b.Enabled = true);
                   h.PublisherConfirmation = true;
               });
               cfg.ClearSerialization();
               cfg.UseRawJsonSerializer();

               cfg.Publish<Cliente>(ConfigurePublishEvento);

               ConfigureEndpoint<PropostaCreditoConsumer>(context, cfg, "", prefetchCount: 10);
               ConfigureEndpoint<EmissaoCartaoCreditoConsumer>(context, cfg, "", prefetchCount: 10);
           });
        });
    }

    private static void ConfigureEndpoint<TConsumer>(IBusRegistrationContext context,
                                                     IRabbitMqBusFactoryConfigurator cfg,
                                                     string queue,
                                                     bool configureConsumeTopology = true,
                                                     int prefetchCount = 30, int attepmts = 2)
        where TConsumer : class, IConsumer
    {
        cfg.ReceiveEndpoint(queue, e =>
        {
            e.ConfigureConsumeTopology = configureConsumeTopology;
            e.PrefetchCount = prefetchCount;
            e.UseMessageRetry(r =>
            {
                r.Interval(attepmts, TimeSpan.FromSeconds(5));
            });
            e.ConfigureConsumer<TConsumer>(context);
        });
    }

    public static void ConfigurePublishEvento(IRabbitMqMessagePublishTopologyConfigurator<Cliente> publishTopologyConfigurator)
    {
        var exchange = publishTopologyConfigurator.Exchange.ExchangeName;
        var queue = "queue.cadastrocliente.v1";
        publishTopologyConfigurator.BindQueue(exchange, queue);
    }
}
