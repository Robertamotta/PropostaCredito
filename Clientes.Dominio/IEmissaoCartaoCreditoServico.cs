using Clientes.Dominio.DTOs;

namespace Clientes.Dominio;

public interface IEmissaoCartaoCreditoServico
{
    Task SalvarEmissaoCartaoCredito(EmissaoCartaoCredito emissaoCartaoCredito);
}
