using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Entidades;
using PropostaCredito.Dominio.Interfaces;

namespace PropostaCredito.Dominio.Servico
{
    public class ProcessadorPropostaCreditoClienteServico(IMensageria mensageria) : IProcessadorPropostaCreditoClienteServico
    {
        public async Task Processar(ClienteDto cliente)
        {
            bool aprovacao = AvaliarCredito(cliente);

            await EnviarMensagens(cliente, aprovacao);
        }

        private async Task EnviarMensagens(ClienteDto cliente, bool aprovacao)
        {
            cliente.AprovacaoCredito = aprovacao;

            var clienteProposta = ConverterParaClienteProposta(cliente);

            await mensageria.EnviarPropostaCreditoClientes(clienteProposta);

            if (aprovacao)
            {
                var emissaoCartao = ConverterParaEmissaoCartaoCredito(cliente);

                await mensageria.EnviarPropostaCreditoEmissaoCartoes(emissaoCartao);
            }
        }

        private static bool AvaliarCredito(ClienteDto cliente)
        {
            return cliente.ScoreCredito > 750;
        }

        private static ClienteProposta ConverterParaClienteProposta(ClienteDto cliente)
        {
            return new ClienteProposta
            {
                Id = cliente.Id,
                AprovacaoCredito = cliente.AprovacaoCredito
            };
        }

        private static PropostaCreditoDto ConverterParaEmissaoCartaoCredito(ClienteDto cliente)
        {
            return new PropostaCreditoDto
            {
                Id = cliente.Id,
                Renda = cliente.Renda,
                ScoreCredito = cliente.ScoreCredito,
                AprovacaoCredito = cliente.AprovacaoCredito
            };
        }
    }
}
