using Clientes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Clientes.Infraestrutura.Dados;

[ExcludeFromCodeCoverage]
public class ClientesContext(DbContextOptions<ClientesContext> options) : DbContext(options)
{
    public required DbSet<Cliente> Cliente { get; set; }
}
