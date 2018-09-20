CREATE PROCEDURE [dbo].[ConsolidarMovimento]
    @codMovimento BIGINT
AS

UPDATE Movimento
SET Consolidado = 1
WHERE CodMovimento = @codMovimento

IF @@ROWCOUNT > 0
    RETURN 0
ELSE
    RETURN 1