using Clientes.Dominio;
using Clientes.Dominio.Entidades;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Clientes.Infraestrutura.Servico;

[ExcludeFromCodeCoverage]
public class Mensageria(IPublishEndpoint publishEndpoint, ILogger<Mensageria> logger) : IMensageria
{
    public async Task EnviarCadastroClienteNovo(Cliente cliente)
    {
        try
        {
            await publishEndpoint.Publish(cliente);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"Ocorreu um erro ao postar a mensagem na fila de novos clientes");
        }
    }
}
