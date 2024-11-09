using Clientes.Dominio;
using Clientes.Dominio.DTOs;
using Clientes.Infraestrutura.Interfaces;

namespace Clientes.Aplicacao.Servicos;

public class EmissaoCartaoCreditoServico(IClienteRepositorio clienteRepositorio) : IEmissaoCartaoCreditoServico
{
    public async Task SalvarEmissaoCartaoCredito(EmissaoCartaoCredito emissaoCartaoCredito)
    {
        var atualizacaoEmissaoCartaoCredito = emissaoCartaoCredito.ParaAtualizacaoQtdCartoesEmitidos();

        await clienteRepositorio.Atualizar(atualizacaoEmissaoCartaoCredito);
    }
}
