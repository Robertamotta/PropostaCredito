using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.DTOs;

public class EmissaoCartaoCredito
{
    public int Id { get; set; }
    public int QtdCartoesEmitidos {  get; set; }

    public virtual Cliente ParaAtualizacaoQtdCartoesEmitidos()
    {
        return new Cliente
        {
            Id = Id,
            QtdCartoesEmitidos = QtdCartoesEmitidos
        };
    }
}
