CREATE TABLE [dbo].[Conta] (
    [CodConta]     INT          IDENTITY (1, 1) NOT NULL,
    [Ativo]        BIT          DEFAULT ((1)) NOT NULL,
    [Banco]        VARCHAR (20) NOT NULL,
    [Tipo]         TINYINT      NOT NULL,
    [AgenciaConta] VARCHAR (30) NOT NULL
);

