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


GO

--EXEC Conta_Salvar 0, 1, 'Itaú', 1, '00000'
EXEC Conta_Salvar 3, 0, 'Itaú aaa', 2, '00000 111'

GO


DROP PROCEDURE Conta_Salvar

GO

SELECT * FROM Conta
