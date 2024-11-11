using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Interfaces;
using MassTransit;

namespace PropostaCredito.Api.Consumers;

public class ClienteConsumer(ILogger<ClienteConsumer> logger, IProcessadorClienteServico clienteServico) : IConsumer<Cliente>
{
    public async Task Consume(ConsumeContext<Cliente> context)
    {
        try
        {
            await clienteServico.Processar(context.Message);
        }
        catch (Exception ex)
        {
            logger.LogError($"Ocorreu um erro ao processar solicitacao. {ex.Message}");
            throw;
        }
    }
}
