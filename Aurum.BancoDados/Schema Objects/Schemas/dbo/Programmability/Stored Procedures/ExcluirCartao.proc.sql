CREATE PROCEDURE [dbo].[ExcluirCartao]
    @codCartao INT
AS

IF EXISTS (SELECT 1 FROM Movimento WHERE CodCartao = @codCartao)
	RETURN 2 -- Movimento vinculado ao cartão

DELETE Cartao
WHERE CodCartao = @codCartao

IF @@ROWCOUNT > 0
    RETURN 0
ELSE
    RETURN 1