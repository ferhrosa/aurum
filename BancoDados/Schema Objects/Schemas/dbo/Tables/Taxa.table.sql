CREATE TABLE [dbo].[Taxa] (
    [CodTaxa]   INT IDENTITY (1, 1) NOT NULL,
    [Ativo]     BIT DEFAULT ((1))   NOT NULL,
    [CodConta]  INT                 NULL,
    [CodCartao] INT                 NULL,
    [Valor]     INT                 NOT NULL
);

