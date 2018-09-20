CREATE PROCEDURE [dbo].[SalvarConta]
	@codConta INT,
    @ativo BIT,
	@banco VARCHAR (20),
	@tipo TINYINT,
	@agenciaConta VARCHAR (30)

AS

IF (@codConta IS NOT NULL AND @codConta > 0)
BEGIN
    
    UPDATE Conta
    SET
        Ativo = @ativo,
        Banco = @banco,
        Tipo = @tipo,
        AgenciaConta = @agenciaConta
    WHERE CodConta = @codConta

    IF @@ROWCOUNT > 0
    BEGIN
        
        -- Carrega o código da conta que foi alterada.
        SELECT @codConta AS CodConta

        -- A conta foi salva com sucesso.
        RETURN 0

    END
    ELSE
        -- A conta não foi salva.
        RETURN 1

END
ELSE
BEGIN
    
    INSERT Conta (Ativo, Banco, Tipo, AgenciaConta)
    VALUES (
        @ativo,
		@banco,
		@tipo,
		@agenciaConta
    )

    -- Carrega o código da conta que foi incluída.
    SELECT CAST(@@IDENTITY AS INT) AS CodConta

    -- A conta foi salva com sucesso.
    RETURN 0

END
