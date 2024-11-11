using Clientes.Dominio.DTOs;
using Clientes.Dominio.Interfaces;
using MassTransit;

namespace Clientes.Api.Consumers;

public class PropostaCreditoConsumer(ILogger<PropostaCreditoConsumer> logger, IPropostaCreditoServico propostaCreditoServico) : IConsumer<PropostaCredito>
{
    public async Task Consume(ConsumeContext<PropostaCredito> context)
    {
        try
        {
            await propostaCreditoServico.SalvarPropostaCredito(context.Message);
        }
        catch (Exception ex)
        {
            logger.LogError($"Ocorreu um erro ao processar solicitacao. {ex.Message}");
            throw;
        }
    }
}
