using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Entidades;
using PropostaCredito.Dominio.Interfaces;

namespace PropostaCredito.Dominio.Servico
{
    public class ProcessadorClienteServico(IMensageria mensageria) : IProcessadorClienteServico
    {
        public async Task Processar(Cliente cliente)
        {

            bool aprovacao = AvaliarCredito(cliente);
            await EnviarMensagensAsync(cliente, aprovacao);
        }

        private static bool AvaliarCredito(Cliente cliente)
        {
            cliente.AprovacaoCredito = cliente.ScoreCredito > 750;
            return cliente.AprovacaoCredito;
        }

        private async Task EnviarMensagensAsync(Cliente cliente, bool aprovacao)
        {
            var clienteProposta = ConverterParaClienteProposta(cliente);
            await mensageria.EnviarPropostaCreditoClientes(clienteProposta);

            if (aprovacao)
            {
                var emissaoCartao = ConverterParaEmissaoCartaoCredito(cliente);
                await mensageria.EnviarPropostaCreditoEmissaoCartoes(emissaoCartao);
            }
        }

        private static ClienteProposta ConverterParaClienteProposta(Cliente cliente)
        {
            return new ClienteProposta
            {
                Id = cliente.Id,
                AprovacaoCredito = cliente.AprovacaoCredito
            };
        }

        private static EmissaoCartaoCredito ConverterParaEmissaoCartaoCredito(Cliente cliente)
        {
            return new EmissaoCartaoCredito
            {
                Id = cliente.Id,
                Renda = cliente.Renda,
                AprovacaoCredito = cliente.AprovacaoCredito
            };
        }
    }
}
