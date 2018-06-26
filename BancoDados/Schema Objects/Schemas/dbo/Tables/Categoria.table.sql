CREATE TABLE [dbo].[Categoria] (
    [CodCategoria] INT          IDENTITY (1, 1) NOT NULL,
    [Ativo]        BIT          DEFAULT ((1)) NOT NULL,
    [Descricao]    VARCHAR (50) NOT NULL
);

