CREATE TABLE [dbo].[Conta]
(
	[Id] INT NOT NULL IDENTITY, 
    [Descricao] VARCHAR(50) NOT NULL, 
    [Tipo] TINYINT NOT NULL, 
    [AgenciaConta] VARCHAR(20) NULL, 
    CONSTRAINT [PK_Conta] PRIMARY KEY ([Id]),
    CONSTRAINT [UK_Conta_Descricao] UNIQUE ([Descricao])
)
