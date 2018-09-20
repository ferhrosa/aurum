CREATE PROCEDURE [dbo].[SalvarMovimentoDescricao]
	@codMovimentoDescricao INT,
	@descricao VARCHAR (50)

AS

IF (@codMovimentoDescricao IS NOT NULL AND @codMovimentoDescricao > 0)
BEGIN
    
    UPDATE MovimentoDescricao
    SET
        Descricao = @descricao
    WHERE CodMovimentoDescricao = @codMovimentoDescricao

    IF @@ROWCOUNT > 0
    BEGIN
        
        -- Carrega o código da descrição que foi alterada.
        SELECT @codMovimentoDescricao AS CodMovimentoDescricao

        -- A descrição foi salva com sucesso.
        RETURN 0

    END
    ELSE
        -- A descrição não foi salva.
        RETURN 1

END
ELSE
BEGIN
    
    IF EXISTS(SELECT 1 FROM MovimentoDescricao WHERE Descricao = @descricao)
    BEGIN
        
        -- Carrega o código da descrição já existente.
        SELECT CodMovimentoDescricao FROM MovimentoDescricao WHERE Descricao = @descricao

    END
    ELSE
    BEGIN

        INSERT MovimentoDescricao (Descricao)
        VALUES (
		    @descricao
        )

        -- Carrega o código da descrição que foi incluída.
        SELECT CAST(@@IDENTITY AS INT) AS CodMovimentoDescricao

    END

    -- A descrição foi salva com sucesso.
    RETURN 0

END
