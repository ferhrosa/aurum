CREATE TABLE [dbo].[Cartao] (
    [CodCartao]       INT          IDENTITY (1, 1) NOT NULL,
    [Ativo]           BIT          DEFAULT ((1)) NOT NULL,
    [CodConta]        INT          NULL,
    [Descricao]       VARCHAR (30) NOT NULL,
    [Numero]          VARCHAR (25) NOT NULL,
    [Titular]         VARCHAR (30) NOT NULL,
    [Validade]        DATE         NULL,
);

