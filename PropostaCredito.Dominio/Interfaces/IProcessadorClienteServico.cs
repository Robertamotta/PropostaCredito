using PropostaCredito.Dominio.DTOs;

namespace PropostaCredito.Dominio.Interfaces;

public interface IProcessadorClienteServico
{
    Task Processar(Cliente cliente);
}
