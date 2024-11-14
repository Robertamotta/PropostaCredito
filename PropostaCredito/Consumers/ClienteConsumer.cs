using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Interfaces;
using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PropostaCredito.Api.Consumers;

public class ClienteConsumer : BackgroundService
{

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string Queue = "queue.cadastrocliente.v1";
    private readonly IProcessadorClienteServico _processadorClienteServico;
    public ClienteConsumer(ILogger<ClienteConsumer> logger, IProcessadorClienteServico clienteServico, IServiceProvider serviceProvider)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        _connection = connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: Queue,
            durable: false,
            exclusive: false,
            autoDelete: false);
        
        _processadorClienteServico = clienteServico;

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var cliente = JsonConvert.DeserializeObject<Cliente>(contentString);

            Console.WriteLine($"Teste");

            Complete(cliente).Wait();

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(Queue, false, consumer);

        return Task.CompletedTask;
    }

    public async Task Complete(Cliente cliente)
    {
        await _processadorClienteServico.Processar(cliente);
    }
}
