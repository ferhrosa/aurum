CREATE PROCEDURE [dbo].[CarregarMovimentoDescricao]
	@codMovimentoDescricao INT,
    @inicioDescricao VARCHAR(MAX)
AS


IF @codMovimentoDescricao IS NOT NULL
	SELECT CodMovimentoDescricao, Descricao
	FROM MovimentoDescricao
	WHERE CodMovimentoDescricao = @codMovimentoDescricao
ELSE
	SELECT MovimentoDescricao.CodMovimentoDescricao, MovimentoDescricao.Descricao
	FROM MovimentoDescricao
	LEFT JOIN Movimento ON MovimentoDescricao.CodMovimentoDescricao = Movimento.CodMovimentoDescricao
    WHERE Descricao LIKE ISNULL(@inicioDescricao, Descricao) + '%'
	GROUP BY MovimentoDescricao.CodMovimentoDescricao, MovimentoDescricao.Descricao
	ORDER BY COUNT(Movimento.CodMovimento) DESC


IF @codMovimentoDescricao IS NOT NULL AND @@ROWCOUNT = 0
    RETURN 1

RETURN 0