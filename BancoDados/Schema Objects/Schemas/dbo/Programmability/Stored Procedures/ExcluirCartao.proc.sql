CREATE PROCEDURE [dbo].[ExcluirCartao]
    @codCartao INT
AS

DELETE Cartao
WHERE CodCartao = @codCartao

IF @@ROWCOUNT > 0
    RETURN 0
ELSE
    RETURN 1