using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Entidades;
using PropostaCredito.Dominio.Interfaces;
using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PropostaCredito.Infraestrutura.Servico;

[ExcludeFromCodeCoverage]
public class Mensageria(ILogger<Mensageria> logger) : IMensageria
{

    public Task EnviarPropostaCreditoEmissaoCartoes(PropostaCreditoDto propostaCredito)
    {
        return EnviarMensagemParaFila("queue.emissaocartaocredito.v1", propostaCredito);
    }

    public Task EnviarPropostaCreditoClientes(ClienteProposta cliente)
    {
        return EnviarMensagemParaFila("queue.respostapropostacredito.v1", cliente);
    }

    private Task EnviarMensagemParaFila<T>(string queue, T mensagem)
    {
        try
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue, false, false, false, null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mensagem));

            channel.BasicPublish(exchange: string.Empty, routingKey: queue, basicProperties: null, body: body);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Ocorreu um erro ao postar a mensagem na fila {queue}");
        }

        return Task.CompletedTask;
    }
}
