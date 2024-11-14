using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaCredito.Dominio.Interfaces;
using PropostaCredito.Dominio.Entidades;
using System.Diagnostics.CodeAnalysis;
using PropostaCredito.Dominio.DTOs;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace PropostaCredito.Infraestrutura.Servico;

[ExcludeFromCodeCoverage]
public class Mensageria(ILogger<Mensageria> logger) : IMensageria
{
    public async Task EnviarPropostaCreditoEmissaoCartoes(EmissaoCartaoCredito emissaoCartao)
    {
        try
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var queue = "queue.cadastrocliente.v1"; // ARRUMAR QUEUE PARA CRIAR CARTOES DO CLIENTE
            channel.QueueDeclare(queue, false, false, false, null);

            channel.BasicPublish(exchange: string.Empty, routingKey: queue, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emissaoCartao)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"Ocorreu um erro ao postar a mensagem na fila de emissao de cartões");
        }
    }
    public async Task EnviarPropostaCreditoClientes(ClienteProposta cliente)
    {
        try
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var queue = "queue.cadastrocliente.v1"; //ARRUMAR QUEUE PARA PROPOSTA CREDITO CLIENTES
            channel.QueueDeclare(queue, false, false, false, null);

            channel.BasicPublish(exchange: string.Empty, routingKey: queue, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cliente)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao postar a mensagem na fila de clientes");
        }
    }
}
