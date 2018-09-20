CREATE PROCEDURE [dbo].[CarregarConta]
	@codConta INT
AS


SELECT CodConta, Ativo, Banco, Tipo, AgenciaConta
FROM Conta
WHERE CodConta = ISNULL(@codConta, CodConta)


IF @codConta IS NOT NULL AND @@ROWCOUNT = 0
    RETURN 1

RETURN 0