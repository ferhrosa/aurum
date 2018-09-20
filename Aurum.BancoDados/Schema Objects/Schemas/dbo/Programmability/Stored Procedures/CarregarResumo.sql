CREATE PROCEDURE [dbo].[CarregarResumo]
	@tipoResumo INT,
    @mesAno DATE
AS

IF @tipoResumo = 1 BEGIN
	SELECT Conta.CodConta, Conta.AgenciaConta AS ContaAgenciaConta, SUM ( Movimento.Valor ) AS Valor
	FROM Movimento
	LEFT JOIN Cartao ON Movimento.CodCartao = Cartao.CodCartao
	LEFT JOIN Conta ON Movimento.CodConta = Conta.CodConta OR Cartao.CodConta = Conta.CodConta
	WHERE YEAR(Movimento.MesAno) = ISNULL(YEAR(@mesAno), YEAR(Movimento.MesAno))
	  AND MONTH(Movimento.MesAno) = ISNULL(MONTH(@mesAno), MONTH(Movimento.MesAno))
	GROUP BY Conta.CodConta, Conta.AgenciaConta
	ORDER BY Conta.AgenciaConta
END
ELSE IF @tipoResumo = 2 BEGIN
	SELECT Cartao.CodCartao, Cartao.Descricao AS CartaoDescricao, SUM ( Movimento.Valor ) AS Valor
	FROM Movimento
	INNER JOIN Cartao ON Movimento.CodCartao = Cartao.CodCartao
	WHERE YEAR(Movimento.MesAno) = ISNULL(YEAR(@mesAno), YEAR(Movimento.MesAno))
	  AND MONTH(Movimento.MesAno) = ISNULL(MONTH(@mesAno), MONTH(Movimento.MesAno))
	GROUP BY Cartao.CodCartao, Cartao.Descricao
	ORDER BY Cartao.Descricao
END
ELSE IF @tipoResumo = 3 BEGIN
	SELECT Categoria.CodCategoria, Categoria.Descricao AS CategoriaDescricao, SUM ( Movimento.Valor ) AS Valor
	FROM Movimento
	INNER JOIN Categoria ON Movimento.CodCategoria = Categoria.CodCategoria
	WHERE YEAR(Movimento.MesAno) = ISNULL(YEAR(@mesAno), YEAR(Movimento.MesAno))
	  AND MONTH(Movimento.MesAno) = ISNULL(MONTH(@mesAno), MONTH(Movimento.MesAno))
	GROUP BY Categoria.CodCategoria, Categoria.Descricao
	ORDER BY Categoria.Descricao
END

RETURN 0