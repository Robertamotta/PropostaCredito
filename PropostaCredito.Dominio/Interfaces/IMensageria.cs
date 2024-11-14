using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Entidades;

namespace PropostaCredito.Dominio.Interfaces;

public interface IMensageria
{
    Task EnviarPropostaCreditoEmissaoCartoes(PropostaCreditoDto emissaoCartao);
    Task EnviarPropostaCreditoClientes(ClienteProposta cliente);
}
