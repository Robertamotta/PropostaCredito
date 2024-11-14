using MassTransit;
using PropostaCredito.Api.Consumers;
using PropostaCredito.Dominio.Entidades;
using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;
using System.Security.Authentication;

namespace PropostaCredito.Api.MassTransitConfig;

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

        //services.AddMassTransit(x =>
        //{
        //    x.AddConsumer<ClienteConsumer>()
        //    .Endpoint(e =>
        //    {
        //        e.Name = "queue.cadastrocliente.v1";
        //    });

        //    x.UsingRabbitMq((context, cfg) => 
        //    {
        //       cfg.Host(host, port, vhost, h =>
        //       {
        //           h.Username(username);
        //           h.Password(password);
        //           if (useSsl)
        //               h.UseSsl(s => s.Protocol = SslProtocols.Tls12);
        //           h.ConfigureBatchPublish(b => b.Enabled = true);
        //           h.PublisherConfirmation = true;
        //       });
        //       //cfg.ClearSerialization();
        //       cfg.UseJsonSerializer();

        //       cfg.ConfigureJsonSerializerOptions(options =>
        //       {
        //           options.PropertyNameCaseInsensitive = true;
        //           return options;
        //       });


        //       cfg.Publish<EmissaoCartaoCredito>(ConfigurePublishEventoCredito);
        //       cfg.Publish<ClienteProposta>(ConfigurePublishEventoProposta);

        //       ConfigureEndpoint<ClienteConsumer>(context, cfg, "queue.cadastrocliente.v1");
        //   });
        //});
    }

    private static void ConfigureEndpoint<TConsumer>(IBusRegistrationContext context,
                                                     IRabbitMqBusFactoryConfigurator cfg,
                                                     string queue,
                                                     bool configureConsumeTopology = true,
                                                     int prefetchCount = 1, int attemts = 2)
        where TConsumer : class, IConsumer
    {
        cfg.ReceiveEndpoint(queue, e =>
        {
            e.ConfigureConsumeTopology = configureConsumeTopology;
            e.PrefetchCount = prefetchCount;
            e.UseMessageRetry(r =>
            {
                r.Interval(attemts, TimeSpan.FromSeconds(5));
            });
            e.ConfigureConsumer<TConsumer>(context);
            //e.BindDeadLetterExchange("dlx.exchange", dlx => { dlx.ExchangeType = ExchangeType.Fanout; dlx.AutoDelete = false; dlx.Durable = true; });
            //e.DiscardFaultedMessages();
        });
    }

    public static void ConfigurePublishEventoCredito(IRabbitMqMessagePublishTopologyConfigurator<EmissaoCartaoCredito> publishTopologyConfigurator)
    {
        var exchange = publishTopologyConfigurator.Exchange.ExchangeName;
        var queue = "queue.emissaocartaocredito.v1";
        publishTopologyConfigurator.BindQueue(exchange, queue);
    }
    public static void ConfigurePublishEventoProposta(IRabbitMqMessagePublishTopologyConfigurator<ClienteProposta> publishTopologyConfigurator)
    {
        var exchange = publishTopologyConfigurator.Exchange.ExchangeName;
        var queue = "queue.cadastrocliente.v1";
        publishTopologyConfigurator.BindQueue(exchange, queue);
    }
}
