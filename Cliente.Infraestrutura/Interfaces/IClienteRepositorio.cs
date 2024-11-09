using Clientes.Dominio.Entidades;

namespace Clientes.Infraestrutura.Interfaces;

public interface IClienteRepositorio
{
    Task<IEnumerable<Cliente>> Listar();
    Task<Cliente> Obter(int id);
    Task Cadastrar(Cliente cliente);
    Task Atualizar(Cliente cliente);
    Task Deletar (int id);
}
