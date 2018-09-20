CREATE PROCEDURE [dbo].[ExcluirCategoria]
    @codCategoria INT
AS

IF EXISTS (SELECT 1 FROM Movimento WHERE CodCategoria = @codCategoria)
	RETURN 2 -- Movimento vinculado à categoria

DELETE Categoria
WHERE CodCategoria = @codCategoria

IF @@ROWCOUNT > 0
    RETURN 0
ELSE
    RETURN 1