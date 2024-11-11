using System.Diagnostics.CodeAnalysis;

namespace PropostaCredito.Dominio.DTOs
{
    [ExcludeFromCodeCoverage]
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public decimal Renda { get; set; }
        public int ScoreCredito { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool AprovacaoCredito { get; set; }
        public int QtdCartoesEmitidos { get; set; }
    }
}
