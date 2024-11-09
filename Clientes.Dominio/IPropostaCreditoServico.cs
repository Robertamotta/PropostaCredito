using Clientes.Dominio.DTOs;

namespace Clientes.Dominio;

public interface IPropostaCreditoServico
{
    Task SalvarPropostaCredito(PropostaCredito propostaCredito);
}
