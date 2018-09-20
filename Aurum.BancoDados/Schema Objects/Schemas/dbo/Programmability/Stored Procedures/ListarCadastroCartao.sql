CREATE PROCEDURE [dbo].[ListarCadastroCartao]
	@codCartao INT
AS


SELECT Cartao.CodCartao AS Codigo
     , Cartao.CodCartao AS "#"
	 , Cartao.Descricao
	 , Cartao.Numero
	 , Cartao.Titular
     , Conta.Banco AS ContaBanco
	 , Conta.AgenciaConta AS ContaAgenciaConta
FROM
	Cartao
	LEFT JOIN Conta ON Cartao.CodConta = Conta.CodConta
WHERE
	Cartao.CodCartao = ISNULL(@codCartao, Cartao.CodCartao)


IF @codCartao IS NOT NULL AND @@ROWCOUNT = 0
    RETURN 1

RETURN 0