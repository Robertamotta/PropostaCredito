using Clientes.Dominio.Entidades;
using Clientes.Infraestrutura.Interfaces;

namespace Clientes.Aplicacao.Servicos;

public class ClienteServico(IClienteRepositorio clienteRepositorio)
{
    public async Task<IEnumerable<Cliente>> ListarClientes()
    {
        return await clienteRepositorio.Listar();
    } 

    public async Task<Cliente> ObterCliente(int id)
    {
        return await clienteRepositorio.Obter(id);
    }

    public async Task CadastrarCliente(Cliente cliente)
    {
        await clienteRepositorio.Cadastrar(cliente);
    }

    public async Task AtualizarCliente(Cliente cliente)
    {
        await clienteRepositorio.Atualizar(cliente);
    }

    public async Task RemoverCliente(int id)
    {
        await clienteRepositorio.Deletar(id);
    }
}
