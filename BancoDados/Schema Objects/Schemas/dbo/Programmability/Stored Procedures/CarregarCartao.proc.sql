CREATE PROCEDURE [dbo].[CarregarCartao]
	@codCartao INT
AS


SELECT CodCartao, Ativo, CodConta, Descricao, Numero, Titular
     , Vencimento, Validade, PossuiAdicional, TelefoneSac
FROM Cartao
WHERE CodCartao = ISNULL(@codCartao, CodCartao)


IF @codCartao IS NOT NULL AND @@ROWCOUNT = 0
    RETURN 1

RETURN 0