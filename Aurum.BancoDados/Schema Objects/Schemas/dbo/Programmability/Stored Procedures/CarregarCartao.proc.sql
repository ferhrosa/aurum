CREATE PROCEDURE [dbo].[CarregarCartao]
	@codCartao INT
AS


SELECT Cartao.CodCartao, Cartao.Ativo, Cartao.CodConta, Cartao.Descricao, Cartao.Numero, Cartao.Titular, Cartao.Validade
     , Conta.Banco AS ContaBanco, Conta.AgenciaConta AS ContaAgenciaConta
FROM Cartao
LEFT JOIN Conta ON Cartao.CodConta = Conta.CodConta
WHERE Cartao.CodCartao = ISNULL(@codCartao, Cartao.CodCartao)


IF @codCartao IS NOT NULL AND @@ROWCOUNT = 0
    RETURN 1

RETURN 0