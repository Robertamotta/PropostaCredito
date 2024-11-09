using Clientes.Dominio;
using Clientes.Dominio.DTOs;
using Clientes.Infraestrutura.Interfaces;

namespace Clientes.Aplicacao.Servicos;

public class PropostaCreditoServico(IClienteRepositorio clienteRepositorio) : IPropostaCreditoServico
{
    public async Task SalvarPropostaCredito(PropostaCredito propostaCredito)
    {
        var atualizacaoPropostaCredito = propostaCredito.ParaAtualizacaoPropostaCredito();

        await clienteRepositorio.Atualizar(atualizacaoPropostaCredito);
    }
}
