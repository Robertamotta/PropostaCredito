using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.DTOs;

public class PropostaCredito
{
    public int Id { get; set; }
    public bool AprovacaoCredito {  get; set; }

    public virtual Cliente ParaAtualizacaoPropostaCredito()
    {
        return new Cliente
        {
            Id = Id,
            AprovacaoCredito = AprovacaoCredito
        };
    }
}
