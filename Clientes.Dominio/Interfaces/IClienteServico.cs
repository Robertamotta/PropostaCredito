using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.Interfaces;

public interface IClienteServico
{
    Task<IEnumerable<Cliente>> ListarClientes();
    Task<Cliente> ObterCliente(int id);
    Task CadastrarCliente(Cliente cliente);
    Task AtualizarCliente(Cliente cliente);
    Task RemoverCliente(int id);
}
