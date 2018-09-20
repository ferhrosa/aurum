CREATE PROCEDURE [dbo].[SalvarCategoria]
	@codCategoria INT,
    @ativo BIT,
	@descricao VARCHAR (50)

AS

IF (@codCategoria IS NOT NULL AND @codCategoria > 0)
BEGIN
    
    UPDATE Categoria
    SET
        Ativo = @ativo,
        Descricao = @descricao
    WHERE CodCategoria = @codCategoria

    IF @@ROWCOUNT > 0
    BEGIN
        
        -- Carrega o código da categoria que foi alterada.
        SELECT @codCategoria AS CodCategoria

        -- A categoria foi salva com sucesso.
        RETURN 0

    END
    ELSE
        -- A categoria não foi salva.
        RETURN 1

END
ELSE
BEGIN
    
    INSERT Categoria (Ativo, Descricao)
    VALUES (
        @ativo,
		@descricao
    )

    -- Carrega o código da categoria que foi incluída.
    SELECT CAST(@@IDENTITY AS INT) AS CodCategoria

    -- A categoria foi salva com sucesso.
    RETURN 0

END
