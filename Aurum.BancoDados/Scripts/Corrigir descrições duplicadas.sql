DECLARE @Principais AS TABLE(Descricao VARCHAR(50) NOT NULL, CodMovimentoDescricao INT NOT NULL)

-- Carrega as descrições que possuem duplicatas.
INSERT @Principais
SELECT Descricao, MIN(CodMovimentoDescricao)
FROM MovimentoDescricao
GROUP BY Descricao
HAVING COUNT(CodMovimentoDescricao) > 1

-- Atualiza a tabela de movimentos para não utilizar mais as descrições duplicadas.
UPDATE Movimento
SET CodMovimentoDescricao = c.CodMovimentoDescricao
FROM Movimento AS a
INNER JOIN MovimentoDescricao AS b ON a.CodMovimentoDescricao = b.CodMovimentoDescricao
INNER JOIN @Principais        AS c ON b.Descricao = c.Descricao

-- Exclui descrições duplicadas.
DELETE MovimentoDescricao
FROM MovimentoDescricao AS a
INNER JOIN @Principais  AS b ON a.Descricao = b.Descricao
WHERE A.CodMovimentoDescricao != B.CodMovimentoDescricao
