﻿using PropostaCredito.Api.Consumers;
using PropostaCredito.Dominio.DTOs;
using PropostaCredito.Dominio.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace PropostaCredito.Testes.Consumers;

public class PropostaCreditoConsumerTestes
{
    private readonly Mock<ILogger<ClienteConsumer>> logger = new();
    private readonly Mock<IPropostaCreditoServico> propostaCreditoServicoMock = new();
    private readonly ClienteConsumer consumer;

    public PropostaCreditoConsumerTestes()
    {
        consumer = new ClienteConsumer(logger.Object, propostaCreditoServicoMock.Object);
    }

    [Fact]
    public async Task PropostaCreditoConsumer_Sucesso()
    {
        // Arrange
        var mensagem = new Mock<PropostaCredito>();

        mensagem.Object.Id = 1;
        mensagem.Object.AprovacaoCredito = true;

        var contextMock = new Mock<ConsumeContext<PropostaCredito>>();
        contextMock.Setup(x => x.Message).Returns(mensagem.Object);

        // Act
        await consumer.Consume(contextMock.Object);

        // Assert
        propostaCreditoServicoMock.Verify(servico => servico.SalvarPropostaCredito(It.Is<PropostaCredito>(x => x.Id == 1 && x.AprovacaoCredito == true)), Times.Once);
    }

    [Fact]
    public async Task EmissaoCartaoCreditoConsumer_Erro()
    {
        //Arrage
        var context = Mock.Of<ConsumeContext<PropostaCredito>>(x => x.Message == new PropostaCredito
        {
            Id = 1,
            AprovacaoCredito = true
        });

        propostaCreditoServicoMock.Setup(x => x.SalvarPropostaCredito(context.Message)).Throws<Exception>();

        //Assert
        await Assert.ThrowsAsync<Exception>(() => consumer.Consume(context));
    }
}
