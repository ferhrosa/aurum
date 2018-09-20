CREATE PROCEDURE [dbo].[ExcluirConta]
    @codConta INT
AS

IF EXISTS (SELECT 1 FROM Cartao WHERE CodConta = @codConta)
	RETURN 2 -- Cartão vinculado à conta

DELETE Conta
WHERE CodConta = @codConta

IF @@ROWCOUNT > 0
    RETURN 0
ELSE
    RETURN 1