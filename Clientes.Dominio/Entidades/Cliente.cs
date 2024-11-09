namespace Clientes.Dominio.Entidades;

public class Cliente
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Cpf { get; set; }
    public required decimal Renda { get; set; }
    public required int ScoreCredito { get; set; }
    public required string Email { get; set; }
    public required string Telefone { get; set; }
}
