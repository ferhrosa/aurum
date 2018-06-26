
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