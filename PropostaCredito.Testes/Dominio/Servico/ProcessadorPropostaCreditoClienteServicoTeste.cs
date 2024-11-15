using Moq;
using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Entidades;
using PropostaCredito.Dominio.Interfaces;
using PropostaCredito.Dominio.Servico;

namespace PropostaCredito.Testes.Dominio.Servico
{
    public class ProcessadorPropostaCreditoClienteServicoTeste
    {
        private readonly Mock<IMensageria> mensageriaMock = new();

        private readonly ProcessadorPropostaCreditoClienteServico processadorPropostaCreditoClienteServico;

        public ProcessadorPropostaCreditoClienteServicoTeste()
        {
            processadorPropostaCreditoClienteServico = new ProcessadorPropostaCreditoClienteServico(mensageriaMock.Object);
        }

        [Fact]
        public async Task Processar_DeveEnviarPropostaCreditoClientesEEmitirCartoes()
        {
            // Arrange
            var cliente = new ClienteDto { Id = 1, ScoreCredito = 800, Renda = 3000 };
            mensageriaMock.Setup(m => m.EnviarPropostaCreditoClientes(It.IsAny<ClienteProposta>()))
                           .Returns(Task.CompletedTask);

            // Act
            await processadorPropostaCreditoClienteServico.Processar(cliente);

            // Assert
            mensageriaMock.Verify(m => m.EnviarPropostaCreditoClientes(It.Is<ClienteProposta>(
                c => c.Id == cliente.Id && c.AprovacaoCredito == true)), Times.Once);

            mensageriaMock.Verify(m => m.EnviarPropostaCreditoEmissaoCartoes(It.Is<PropostaCreditoDto>(
               c => c.Id == cliente.Id && c.Renda == cliente.Renda && c.AprovacaoCredito == true)), Times.Once);
        }


        [Fact]
        public async Task Processar_NaoDeveEnviarPropostaCreditoEmissaoCartoesSeReprovado()
        {
            // Arrange
            var cliente = new ClienteDto { Id = 1, ScoreCredito = 700, Renda = 3000 };

            // Act
            await processadorPropostaCreditoClienteServico.Processar(cliente);

            // Assert
            mensageriaMock.Verify(m => m.EnviarPropostaCreditoClientes(It.IsAny<ClienteProposta>()), Times.Once);

            mensageriaMock.Verify(m => m.EnviarPropostaCreditoEmissaoCartoes(It.IsAny<PropostaCreditoDto>()), Times.Never);
        }
    }
}
