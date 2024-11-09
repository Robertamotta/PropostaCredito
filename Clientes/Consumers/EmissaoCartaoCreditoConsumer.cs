using Clientes.Dominio;
using Clientes.Dominio.DTOs;
using MassTransit;

namespace Clientes.Api.Consumers;

public class EmissaoCartaoCreditoConsumer(IEmissaoCartaoCreditoServico emissaoCartaoCreditoServico) : IConsumer<EmissaoCartaoCredito>
{
    public async Task Consume(ConsumeContext<EmissaoCartaoCredito> context)
    {
        await emissaoCartaoCreditoServico.SalvarEmissaoCartaoCredito(context.Message);
    }
}
