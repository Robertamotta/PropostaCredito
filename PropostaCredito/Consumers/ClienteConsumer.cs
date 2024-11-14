using Newtonsoft.Json;
using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PropostaCredito.Api.Consumers;

[ExcludeFromCodeCoverage]
public class ClienteConsumer : BackgroundService
{
    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly IServiceProvider services;
    private const string Queue = "queue.cadastrocliente.v1";

    public ClienteConsumer(IServiceProvider services)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
            
        };

        connection = connectionFactory.CreateConnection();

        channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: Queue,
            durable: false,
            exclusive: false,
            autoDelete: false);
        
        this.services = services;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var cliente = JsonConvert.DeserializeObject<ClienteDto>(contentString);

            await Complete(cliente);

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        channel.BasicConsume(Queue, false, consumer);

        return Task.CompletedTask;
    }

    public async Task Complete(ClienteDto cliente)
    {
        using var serviceScope = services.CreateScope();

        var processadorClienteServico = serviceScope.ServiceProvider.GetRequiredService<IProcessadorPropostaCreditoClienteServico>();

        await processadorClienteServico.Processar(cliente);
    }
}
