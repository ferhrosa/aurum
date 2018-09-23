CREATE TABLE [dbo].[Categoria]
(
	[Id] INT NOT NULL IDENTITY, 
    [Nome] VARCHAR(30) NOT NULL, 
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id]), 
    CONSTRAINT [UK_Categoria_Nome] UNIQUE ([Nome])
)
