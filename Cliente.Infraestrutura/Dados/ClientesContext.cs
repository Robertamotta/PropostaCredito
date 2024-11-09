using Clientes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Clientes.Infraestrutura.Dados;

[ExcludeFromCodeCoverage]
public class ClientesContext(DbContextOptions<ClientesContext> options) : DbContext(options)
{
    public required DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Cliente>().HasKey(c => c.Id);
        builder.Entity<Cliente>().Property(c => c.Cpf).IsRequired().HasMaxLength(11);
        //Add outras configurações das propriedades
    }
}
