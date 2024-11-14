using PropostaCredito.Dominio.DTOs;

namespace PropostaCredito.Dominio.Interfaces;

public interface IProcessadorPropostaCreditoClienteServico
{
    Task Processar(ClienteDto cliente);
}
