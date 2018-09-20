CREATE TABLE [aurum].[Categoria] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [IdPai] INT          NULL,
    [Nome]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_categoria_X_categoria] FOREIGN KEY ([IdPai]) REFERENCES [aurum].[Categoria] ([Id]),
    CONSTRAINT [UK_categoria] UNIQUE NONCLUSTERED ([IdPai] ASC, [Nome] ASC)
);



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cadastro de categorias de movimentações.
Os registros são hierarquizados, podendo ou não ter um item acima dele (item pai).', @level0type = N'SCHEMA', @level0name = N'aurum', @level1type = N'TABLE', @level1name = N'Categoria';

