using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaCredito.Dominio.Interfaces;
using PropostaCredito.Dominio.Entidades;
using System.Diagnostics.CodeAnalysis;

namespace PropostaCredito.Infraestrutura.Servico;

[ExcludeFromCodeCoverage]
public class Mensageria(IPublishEndpoint publishEndpoint, ILogger<Mensageria> logger) : IMensageria
{
    public async Task EnviarPropostaCreditoEmissaoCartoes(EmissaoCartaoCredito emissaoCartao)
    {
        try
        {
            await publishEndpoint.Publish(emissaoCartao);
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
            await publishEndpoint.Publish(cliente);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao postar a mensagem na fila de clientes");
        }
    }
}
