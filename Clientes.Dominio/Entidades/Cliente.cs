using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Clientes.Dominio.Entidades;

[ExcludeFromCodeCoverage]
[Table("Cliente")]
public class Cliente
{
    [Column("Id")]
    public int Id { get; set; }
    [Column("Nome")]
    public string Nome { get; set; }
    [Column("Cpf")]
    public string Cpf { get; set; }
    [Column("Renda")]
    public decimal Renda { get; set; }
    [Column("ScoreCredito")]
    public int ScoreCredito { get; set; }
    [Column("Email")]
    public string Email { get; set; }
    [Column("Telefone")]
    public string Telefone { get; set; }
    [Column("AprovacaoCredito")]
    public bool AprovacaoCredito { get; set; }
    [Column("QtdCartoesEmitidos")]
    public int QtdCartoesEmitidos {  get; set; } 
}
