using System.Diagnostics.CodeAnalysis;

namespace PropostaCredito.Dominio.DTOs
{
    [ExcludeFromCodeCoverage]
    public class PropostaCreditoDto
    {
        public int Id { get; set; }
        public int ScoreCredito { get; set; }
        public bool AprovacaoCredito { get; set; }
        public decimal Renda { get; set; }
    }
}
