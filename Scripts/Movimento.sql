/*
    Data da criação: 22.04.2011
    
    Última alteração
        Responsável: Patrícia
        Data: 01.05.2011
        Descrição: Criação da chave estrangeira Movimento x MovimentoDescrição.
*/

CREATE TABLE Movimento (
    CodMovimento INT IDENTITY(1, 1),
    Ativo BIT NOT NULL DEFAULT 1,
    DataHoraInclusao DATETIME NOT NULL DEFAULT GETDATE(),
    Data DATE,
    MesAno DATE NOT NULL,
    CodMovimentoDescricao INT NOT NULL,
    CodCategoria INT,
    Valor INT NOT NULL,
    CodConta INT,
    CodCartao INT,
    Reserva BIT NOT NULL DEFAULT 0,
    CodMovimentoReserva INT,
    CodMovimentoPrimeiraParcela INT,
    Observacao VARCHAR(250),
    
    CONSTRAINT PK_Movimento PRIMARY KEY (CodMovimento),
    CONSTRAINT FK_MovimentoDescricao FOREIGN KEY (CodMovimentoDescricao) REFERENCES MovimentoDescricao(CodMovimentoDescricao),
    CONSTRAINT FK_Movimento_Categoria FOREIGN KEY (CodCategoria) REFERENCES Categoria(CodCategoria),
    CONSTRAINT FK_Movimento_Conta FOREIGN KEY (CodConta) REFERENCES Conta(CodConta),
    CONSTRAINT FK_Movimento_Cartao FOREIGN KEY (CodCartao) REFERENCES Cartao(CodCartao),
    CONSTRAINT FK_MovimentoReserva FOREIGN KEY (CodMovimentoReserva) REFERENCES Movimento(CodMovimento),
    CONSTRAINT FK_MovimentoPrimeiraParcela FOREIGN KEY (CodMovimentoPrimeiraParcela) REFERENCES Movimento(CodMovimento)
)

GO

CREATE TRIGGER TG_Movimento_InsertUpdate
    ON Movimento
    AFTER INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;
    
    DECLARE @CodConta   INT,
            @CodCartao  INT
    
    SELECT @CodConta = CodConta,
           @CodCartao = CodCartao
    FROM INSERTED
    
    IF @CodConta IS NULL AND @CodCartao IS NULL BEGIN
        RAISERROR(100001, 16, 1)
        ROLLBACK TRANSACTION
    END
    
    DECLARE @CodMovimento           INT,
            @CodMovimentoReserva    INT
    
    SELECT @CodMovimento = CodMovimento,
           @CodMovimentoReserva = CodMovimentoReserva
    FROM INSERTED
    
    IF @CodMovimento = @CodMovimentoReserva BEGIN
        RAISERROR(100002, 16, 1)
        ROLLBACK TRANSACTION
    END
END