CREATE PROCEDURE [dbo].[CarregarCategoria]
	@codCategoria INT
AS


SELECT CodCategoria, Ativo, Descricao
FROM Categoria
WHERE CodCategoria = ISNULL(@codCategoria, CodCategoria)


IF @codCategoria IS NOT NULL AND @@ROWCOUNT = 0
    RETURN 1

RETURN 0