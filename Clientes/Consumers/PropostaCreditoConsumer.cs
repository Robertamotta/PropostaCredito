using Clientes.Dominio;
using Clientes.Dominio.DTOs;
using MassTransit;

namespace Clientes.Api.Consumers;

public class PropostaCreditoConsumer(IPropostaCreditoServico propostaCreditoServico) : IConsumer<PropostaCredito>
{
    public async Task Consume(ConsumeContext<PropostaCredito> context)
    {
        await propostaCreditoServico.SalvarPropostaCredito(context.Message);
    }
}
