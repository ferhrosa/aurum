CREATE TABLE [dbo].[Movimento] (
    [CodMovimento]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [Ativo]                       BIT           DEFAULT (1) NOT NULL,
    [DataHoraInclusao]            DATETIME      DEFAULT (GETDATE()) NOT NULL,
    [Data]                        DATE          NOT NULL,
    [Consolidado]                 BIT           DEFAULT (0) NOT NULL,
    [MesAno]                      DATE          NOT NULL,
    [CodMovimentoDescricao]       INT           NOT NULL,
    [CodCategoria]                INT           NOT NULL,
    [Valor]                       INT           NOT NULL,
    [CodConta]                    INT           NULL,
    [CodCartao]                   INT           NULL,
    [CodMovimentoPrimeiraParcela] BIGINT        NULL,
    [NumeroParcela]               TINYINT       NULL,
    [TotalParcelas]               TINYINT       NULL,
    [Observacao]                  VARCHAR (250) NULL,

    CONSTRAINT [FK_MovimentoPrimeiraParcela]
        FOREIGN KEY ([CodMovimentoPrimeiraParcela])
        REFERENCES [Movimento] ([CodMovimento]),

);

