using Castle.Core.Logging;
using PropostaCredito.Api.Consumers;
using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace PropostaCredito.Testes.Consumers;

public class EmissaoCartaoCreditoConsumerTestes
{
    private readonly Mock<ILogger<EmissaoCartaoCreditoConsumer>> loggerMock = new();
    private readonly Mock<IEmissaoCartaoCreditoServico> emissaoCartaoCreditoServicoMock = new();
    private readonly EmissaoCartaoCreditoConsumer consumer;

    public EmissaoCartaoCreditoConsumerTestes()
    {
        consumer = new EmissaoCartaoCreditoConsumer(loggerMock.Object, emissaoCartaoCreditoServicoMock.Object);
    }

    [Fact]
    public async Task EmissaoCartaoCreditoConsumer_Sucesso()
    {
        // Arrange
        var mensagem = new Mock<EmissaoCartaoCredito>();

        mensagem.Object.Id = 1;
        mensagem.Object.QtdCartoesEmitidos = 1;

        var contextMock = new Mock<ConsumeContext<EmissaoCartaoCredito>>();
        contextMock.Setup(x => x.Message).Returns(mensagem.Object);

        // Act
        await consumer.Consume(contextMock.Object);

        // Assert
        emissaoCartaoCreditoServicoMock.Verify(servico => servico.SalvarEmissaoCartaoCredito(It.Is<EmissaoCartaoCredito>(x => x.Id == 1 && x.QtdCartoesEmitidos == 1)), Times.Once);
    }

    [Fact]
    public async Task EmissaoCartaoCreditoConsumer_Erro()
    {
        //Arrage
        var context = Mock.Of<ConsumeContext<EmissaoCartaoCredito>>(x => x.Message == new EmissaoCartaoCredito
        {
            Id = 1,
            QtdCartoesEmitidos = 1
        });
        
        emissaoCartaoCreditoServicoMock.Setup(x => x.SalvarEmissaoCartaoCredito(context.Message)).Throws<Exception>();

        //Assert
        await Assert.ThrowsAsync<Exception>(() => consumer.Consume(context));
    }
}
