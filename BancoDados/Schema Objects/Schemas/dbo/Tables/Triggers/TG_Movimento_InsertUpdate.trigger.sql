
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