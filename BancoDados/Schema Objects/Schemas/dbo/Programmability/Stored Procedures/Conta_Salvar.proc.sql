CREATE PROCEDURE Conta_Salvar(@CodConta INT, @Ativo BIT, @Banco VARCHAR(20), @Tipo TINYINT, @AgenciaConta VARCHAR(30))
AS

IF (@CodConta IS NOT NULL AND @CodConta > 0) BEGIN
    UPDATE Conta
    SET
        Ativo = @Ativo,
        Banco = @Banco,
        Tipo = @Tipo,
        AgenciaConta = @AgenciaConta
    WHERE CodConta = @CodConta
END
ELSE BEGIN
    INSERT Conta (Ativo, Banco, Tipo, AgenciaConta)
    VALUES (
        @Ativo,
        @Banco,
        @Tipo,
        @AgenciaConta
    )
END