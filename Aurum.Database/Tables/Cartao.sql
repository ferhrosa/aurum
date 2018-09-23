CREATE TABLE [dbo].[Cartao]
(
	[Id] INT NOT NULL IDENTITY, 
    [Descricao] VARCHAR(30) NOT NULL, 
    [IdConta] INT NULL, 
    CONSTRAINT [PK_Cartao] PRIMARY KEY ([Id]),
    CONSTRAINT [UK_Cartao_Descricao] UNIQUE ([Descricao]),
    CONSTRAINT [FK_Cartao_Conta] FOREIGN KEY ([IdConta]) REFERENCES [Conta]([Id])
)
