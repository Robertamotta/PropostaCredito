using Clientes.Dominio.Entidades;

namespace Clientes.Dominio;

public interface IMensageria
{
    Task EnviarCadastroClienteNovo(Cliente cliente);
}
