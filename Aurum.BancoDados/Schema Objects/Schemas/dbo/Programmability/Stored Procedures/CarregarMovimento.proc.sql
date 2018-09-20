CREATE PROCEDURE [dbo].[CarregarMovimento]
	@codMovimento BIGINT,
    @mesAno DATE
AS


SELECT Movimento.CodMovimento, Movimento.DataHoraInclusao, Movimento.Data, Movimento.Consolidado
	 , Movimento.MesAno, Movimento.CodMovimentoDescricao, Movimento.CodCategoria, Movimento.Valor
	 , Movimento.CodConta, Movimento.CodCartao, Movimento.CodMovimentoPrimeiraParcela
     , Movimento.NumeroParcela, Movimento.TotalParcelas
	 , Movimento.Observacao, MovimentoDescricao.Descricao AS MovimentoDescricaoDescricao
	 , Categoria.Descricao AS CategoriaDescricao, Conta.AgenciaConta AS ContaAgenciaConta
	 , Cartao.Descricao AS CartaoDescricao
FROM Movimento
INNER JOIN MovimentoDescricao ON Movimento.CodMovimentoDescricao = MovimentoDescricao.CodMovimentoDescricao
INNER JOIN Categoria ON Movimento.CodCategoria = Categoria.CodCategoria
LEFT JOIN Conta ON Movimento.CodConta = Conta.CodConta
LEFT JOIN Cartao ON Movimento.CodCartao = Cartao.CodCartao
WHERE Movimento.CodMovimento = ISNULL(@codMovimento, Movimento.CodMovimento)
  AND YEAR(Movimento.MesAno) = ISNULL(YEAR(@mesAno), YEAR(Movimento.MesAno))
  AND MONTH(Movimento.MesAno) = ISNULL(MONTH(@mesAno), MONTH(Movimento.MesAno))
ORDER BY Movimento.Consolidado, ISNULL(Conta.AgenciaConta, Cartao.Descricao), Movimento.Data


IF @codMovimento IS NOT NULL AND @@ROWCOUNT = 0
    RETURN 1

RETURN 0