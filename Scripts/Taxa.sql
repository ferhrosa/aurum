/*
    Data da criação: 21.04.2011
    
    Última alteração
        Responsável: Fernando
        Data: 14.05.2011
        Descrição: Criação do campo de data de validade.

    Histórico de alterações:

        Responsável: Fernando
        Data: 21.04.2011
        Descrição: Criação do script        

        Responsável: Fernando
        Data: 22.04.2011
        Descrição: Criação do gatilho para não deixar inserir ou atualizar registro deixando ambos CodConta e CodCartao nulos.

        Responsável: Fernando
        Data: 14.05.2011
        Descrição: Criação do campo de data de validade.
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