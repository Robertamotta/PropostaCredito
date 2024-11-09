using Clientes.Dominio.Entidades;
using Clientes.Infraestrutura.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Clientes.Infraestrutura.Dados;

[ExcludeFromCodeCoverage]
public class ClienteRepositorio(ClientesContext context) : IClienteRepositorio
{
    public async Task<IEnumerable<Cliente>> Listar() => await context.Clientes.ToListAsync();

    public async Task<Cliente> Obter(int id) => await context.Clientes.FindAsync(id);

    public async Task Cadastrar(Cliente cliente)
    {
        await context.Clientes.AddAsync(cliente);
        await context.SaveChangesAsync();
    }

    public async Task Atualizar(Cliente cliente)
    {
        context.Clientes.Update(cliente);
        await context.SaveChangesAsync();
    }

    public async Task Deletar(int id)
    {
        var cliente = await context.Clientes.FindAsync(id);

        if (cliente != null)
        {
            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();
        }
    }
}
