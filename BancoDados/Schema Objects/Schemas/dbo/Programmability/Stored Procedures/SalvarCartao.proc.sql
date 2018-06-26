CREATE PROCEDURE [dbo].[SalvarCartao]
	@codCartao INT,
    @ativo BIT,
    @codConta INT,
    @descricao VARCHAR(30),
    @numero VARCHAR(25),
    @titular VARCHAR(30),
    @vencimento TINYINT,
    @validade DATETIME,
    @possuiAdicional BIT,
    @telefoneSac VARCHAR(15)
AS


IF (@codCartao IS NOT NULL AND @codCartao > 0)
BEGIN
    
    UPDATE Cartao
    SET
        Ativo = @ativo,
        CodConta = @codConta,
        Descricao = @descricao,
        Numero = @numero,
        Titular = @titular,
        Vencimento = @vencimento,
        Validade = @validade,
        PossuiAdicional = @possuiAdicional,
        TelefoneSac = @telefoneSac
    WHERE CodCartao = @codCartao

    IF @@ROWCOUNT > 0
    BEGIN
        
        -- Carrega o código do cartão que foi alterado.
        SELECT @codCartao AS CodCartao

        -- O cartão foi salvo com sucesso.
        RETURN 0

    END
    ELSE
        -- O cartão não foi salvo.
        RETURN 1

END
ELSE
BEGIN
    
    INSERT Cartao (Ativo, CodConta, Descricao, Numero, Titular, Vencimento, Validade, PossuiAdicional, TelefoneSac)
    VALUES (
        @ativo,
        @codConta,
        @descricao,
        @numero,
        @titular,
        @vencimento,
        @validade,
        @possuiAdicional,
        @telefoneSac
    )

    -- Carrega o código do cartão que foi incluído.
    SELECT CAST(@@IDENTITY AS INT) AS CodCartao

    -- O cartão foi salvo com sucesso.
    RETURN 0

END
