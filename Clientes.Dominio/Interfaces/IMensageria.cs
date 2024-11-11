using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.Interfaces;

public interface IMensageria
{
    Task EnviarCadastroClienteNovo(Cliente cliente);
}
