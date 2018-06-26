CREATE PROCEDURE Conta_Excluir(@CodConta INT)
AS

DELETE Conta
WHERE CodConta = @CodConta
