using System.Diagnostics.CodeAnalysis;

namespace PropostaCredito.Dominio.Entidades;

[ExcludeFromCodeCoverage]
public class ClienteProposta
{
    public int Id { get; set; }
    public bool AprovacaoCredito { get; set; }
}
