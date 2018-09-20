CREATE PROCEDURE [dbo].[CarregarConferencia]
	@dataInicio DATE,
	@dataFim DATE
AS

SELECT CONVERT(DATE, CAST(YEAR(MesAno) AS CHAR(4)) + '-' + CAST(MONTH(MesAno) AS VARCHAR(2)) + '-01') AS MesAno,
	   Conta.CodConta, Conta.AgenciaConta AS ContaAgenciaConta, SUM ( Movimento.Valor ) AS Valor

FROM Movimento
LEFT JOIN Cartao ON Movimento.CodCartao = Cartao.CodCartao
LEFT JOIN Conta ON Movimento.CodConta = Conta.CodConta OR Cartao.CodConta = Conta.CodConta

WHERE Movimento.Consolidado = 1
AND (
	YEAR(MesAno) > YEAR(@dataInicio)
	OR (
		YEAR(MesAno) = YEAR(@dataInicio)
		AND MONTH(MesAno) >= MONTH(@dataInicio)
	)
)
AND (
	YEAR(MesAno) < YEAR(@dataFim)
	OR (
		YEAR(MesAno) = YEAR(@dataFim)
		AND MONTH(MesAno) <= MONTH(@dataFim)
	)
)
				
GROUP BY YEAR(MesAno), MONTH(MesAno), Conta.CodConta, Conta.AgenciaConta
ORDER BY YEAR(MesAno), MONTH(MesAno), Conta.AgenciaConta


RETURN 0