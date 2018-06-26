CREATE PROCEDURE Conta_Excluir(@CodConta INT)
AS

DELETE Conta
WHERE CodConta = @CodConta

GO

--EXEC Conta_Salvar 0, 1, 'Itaú', 1, '00000'
EXEC Conta_Excluir 3

GO


DROP PROCEDURE Conta_Excluir

GO

SELECT * FROM Conta
