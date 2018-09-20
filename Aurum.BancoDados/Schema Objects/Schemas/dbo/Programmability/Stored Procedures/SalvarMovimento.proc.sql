CREATE PROCEDURE [dbo].[SalvarMovimento]
    @codMovimento BIGINT,
    @data DATE,
    @consolidado BIT,
    @mesAno DATE,
    @codMovimentoDescricao INT,
    @codCategoria INT,
    @valor INT,
    @codConta INT,
    @codCartao INT,
    @codMovimentoPrimeiraParcela BIGINT,
    @numeroParcela TINYINT,
    @totalParcelas TINYINT,
    @observacao VARCHAR (250)

AS


IF (@codCartao IS NOT NULL AND @codConta IS NOT NULL)
	RETURN 2

IF (@codCartao IS NULL AND @codConta IS NULL)
	RETURN 3

IF (@codMovimento IS NOT NULL AND @codMovimento > 0)
BEGIN
    
    UPDATE Movimento
    SET
		Data = @data,
		--MesAno = @mesAno,
        Consolidado = @consolidado,
		CodMovimentoDescricao = @codMovimentoDescricao,
		CodCategoria = @codCategoria,
		Valor = @valor,
		CodConta = @codConta,
		CodCartao = @codCartao,
		Observacao = @observacao
    WHERE CodMovimento = @codMovimento

    IF @@ROWCOUNT > 0
    BEGIN
        
        -- Carrega o código do movimento que foi alterado.
        SELECT @codMovimento AS CodMovimento

        -- O movimento foi salvo com sucesso.
        RETURN 0

    END
    ELSE
        -- O movimento não foi salvo.
        RETURN 1

END
ELSE
BEGIN
    
    INSERT Movimento (Data, Consolidado, MesAno, CodMovimentoDescricao, CodCategoria, Valor, CodConta
					, CodCartao, CodMovimentoPrimeiraParcela, NumeroParcela, TotalParcelas, Observacao)
    VALUES (
		@data,
        @consolidado,
		@mesAno,
		@codMovimentoDescricao,
		@codCategoria,
		@valor,
		@codConta,
		@codCartao,
		@codMovimentoPrimeiraParcela,
        @numeroParcela,
        @totalParcelas,
		@observacao
    )

    DECLARE @codMovimentoIncluido AS BIGINT

    -- Carrega o código do movimento que foi incluído.
    SET @codMovimentoIncluido = CAST(@@IDENTITY AS BIGINT)


    IF (@codMovimentoPrimeiraParcela IS NULL AND @numeroParcela IS NOT NULL)
    BEGIN
        UPDATE Movimento
        SET CodMovimentoPrimeiraParcela = @codMovimentoIncluido
        WHERE CodMovimento = @codMovimentoIncluido
    END


    -- Retorna o código do movimento que foi incluído.
    SELECT @codMovimentoIncluido AS CodMovimento

    -- O movimento foi salvo com sucesso.
    RETURN 0

END
