CREATE TABLE [dbo].[Movimentacao]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID(), 
    [IdCategoria] INT NOT NULL, 
    [IdConta] INT NULL, 
    [IdCartao] INT NULL, 
    [Data] DATE NOT NULL, 
    [Descricao] VARCHAR(50) NOT NULL, 
    [Valor] MONEY NOT NULL, 
    [Efetivada] BIT NOT NULL, 
    [DataHoraInclusao] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [IdMovimentacaoPrimeiraParcela] UNIQUEIDENTIFIER NULL, 
    [NumeroParcela] SMALLINT NULL, 
    [TotalParcelas] SMALLINT NULL, 
    [Observacao] VARCHAR(500) NULL, 
    CONSTRAINT [PK_Movimentacao] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Movimentacao_Categoria] FOREIGN KEY ([IdCategoria]) REFERENCES [Categoria]([Id]), 
    CONSTRAINT [FK_Movimentacao_Conta] FOREIGN KEY ([IdConta]) REFERENCES [Conta]([Id]), 
    CONSTRAINT [FK_Movimentacao_Cartao] FOREIGN KEY ([IdCartao]) REFERENCES [Cartao]([Id]), 
    CONSTRAINT [CK_Movimentacao_ContaOuCartao] CHECK ((IdConta IS NULL AND IdCartao IS NOT NULL) OR (IdConta IS NOT NULL AND IdCartao IS NULL)), 
    CONSTRAINT [FK_Movimentacao_MovimentacaoPrimeiraParcela] FOREIGN KEY ([IdMovimentacaoPrimeiraParcela]) REFERENCES [Movimentacao]([Id]) 
)
