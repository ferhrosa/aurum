CREATE TABLE [dbo].[MovimentoDescricao] (
    [CodMovimentoDescricao] INT          IDENTITY (1, 1) NOT NULL,
    [Descricao]             VARCHAR (50) NOT NULL, 
    CONSTRAINT [AK_MovimentoDescricao_Descricao] UNIQUE (Descricao), 
);

