CREATE PROCEDURE [dbo].[ExcluirMovimento]
    @codMovimento BIGINT
AS

IF EXISTS (
    SELECT 1
    FROM Movimento
    WHERE CodMovimento = @codMovimento
      AND CodMovimentoPrimeiraParcela IS NOT NULL
      AND CodMovimento != CodMovimentoPrimeiraParcela
    )
	RETURN 2 -- Não é possível excluir uma parcela que não seja a primeira da sequência.
	--RETURN 2 -- Há parcelas vinculadas ao movimento.


IF EXISTS (SELECT 1 FROM Movimento WHERE CodMovimentoPrimeiraParcela = @codMovimento)
    DELETE Movimento
    WHERE CodMovimentoPrimeiraParcela = @codMovimento
      AND CodMovimento != @codMovimento

DELETE Movimento
WHERE CodMovimento = @codMovimento

IF @@ROWCOUNT > 0
    RETURN 0
ELSE
    RETURN 1