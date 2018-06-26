CREATE TABLE [dbo].[Movimento] (
    [CodMovimento]                INT           IDENTITY (1, 1) NOT NULL,
    [Ativo]                       BIT           DEFAULT ((1)) NOT NULL,
    [DataHoraInclusao]            DATETIME      DEFAULT (getdate()) NOT NULL,
    [Data]                        DATE          NULL,
    [MesAno]                      DATE          NOT NULL,
    [CodMovimentoDescricao]       INT           NOT NULL,
    [CodCategoria]                INT           NULL,
    [Valor]                       INT           NOT NULL,
    [CodConta]                    INT           NULL,
    [CodCartao]                   INT           NULL,
    [Reserva]                     BIT           DEFAULT ((0)) NOT NULL,
    [CodMovimentoReserva]         INT           NULL,
    [CodMovimentoPrimeiraParcela] INT           NULL,
    [Observacao]                  VARCHAR (250) NULL
);

