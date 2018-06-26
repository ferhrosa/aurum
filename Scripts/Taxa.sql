/*
    Data da cria��o: 21.04.2011
    
    �ltima altera��o
        Respons�vel: Fernando
        Data: 14.05.2011
        Descri��o: Cria��o do campo de data de validade.

    Hist�rico de altera��es:

        Respons�vel: Fernando
        Data: 21.04.2011
        Descri��o: Cria��o do script        

        Respons�vel: Fernando
        Data: 22.04.2011
        Descri��o: Cria��o do gatilho para n�o deixar inserir ou atualizar registro deixando ambos CodConta e CodCartao nulos.

        Respons�vel: Fernando
        Data: 14.05.2011
        Descri��o: Cria��o do campo de data de validade.
*/

CREATE TABLE Taxa (
    CodTaxa     INT     IDENTITY(1, 1)              ,
    Ativo       BIT     NOT NULL        DEFAULT 1   ,
    CodConta    INT                                 ,
    CodCartao   INT                                 ,
    Valor       INT     NOT NULL                    ,
    
    CONSTRAINT PK_Taxa          PRIMARY KEY (CodTaxa),
    CONSTRAINT FK_Taxa_Conta    FOREIGN KEY (CodConta)  REFERENCES Conta(CodConta),
    CONSTRAINT FK_Taxa_Cartao   FOREIGN KEY (CodCartao) REFERENCES Cartao(CodCartao)
)

GO

CREATE TRIGGER TG_Taxa_InsertUpdate
    ON Taxa
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
END

-----------------------------------------

ALTER TABLE Taxa ADD Validade DATE