using PropostaCredito.Dominio.Entidades;

namespace PropostaCredito.Dominio.Interfaces;

public interface IMensageria
{
    Task EnviarPropostaCreditoEmissaoCartoes(EmissaoCartaoCredito emissaoCartao);
    Task EnviarPropostaCreditoClientes(ClienteProposta cliente);
}
