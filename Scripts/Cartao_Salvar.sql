CREATE PROCEDURE Cartao_Salvar(@CodCartao INT, @Ativo BIT, @CodConta INT, @Descricao VARCHAR(30), @Numero VARCHAR(25), @Titular VARCHAR(30),
                               @Vencimento TINYINT, @Validade DATETIME, @PossuiAdicional BIT, @TelefoneSac VARCHAR(15))
AS

IF (@CodCartao IS NOT NULL AND @CodCartao > 0) BEGIN
    UPDATE Cartao
    SET
        Ativo = @Ativo,
        CodConta = @CodConta,
        Descricao = @Descricao,
        Numero = @Numero,
        Titular = @Titular,
        Vencimento = @Vencimento,
        Validade = @Validade,
        PossuiAdicional = @PossuiAdicional,
        TelefoneSac = @TelefoneSac
    WHERE CodCartao = @CodCartao
END
ELSE BEGIN
    INSERT Cartao (Ativo, CodConta, Descricao, Numero, Titular, Vencimento, Validade, PossuiAdicional, TelefoneSac)
    VALUES (
        @Ativo,
        @CodConta,
        @Descricao,
        @Numero,
        @Titular,
        @Vencimento,
        @Validade,
        @PossuiAdicional,
        @TelefoneSac
    )
END


GO

--EXEC Cartao_Salvar 0, 1, 'Itaú', 1, '00000'
--EXEC Cartao_Salvar 3, 0, NULL, 2, '00000 111'

GO


DROP PROCEDURE Cartao_Salvar

GO

SELECT * FROM Cartao
