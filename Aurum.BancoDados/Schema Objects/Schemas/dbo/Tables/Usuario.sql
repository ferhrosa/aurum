CREATE TABLE [dbo].[Usuario]
(
    [CodUsuario] INT            IDENTITY (1, 1) NOT NULL,
    [Ativo]      BIT            DEFAULT (1) NOT NULL,
    [Login]      VARCHAR (20)   NOT NULL,
    [Nome]       VARCHAR (30)   NOT NULL,
    [Email]      VARCHAR (100)  NOT NULL,
	[SenhaPass]  VARBINARY(MAX) NOT NULL
)
