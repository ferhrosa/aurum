CREATE PROCEDURE [dbo].[ReplicarMovimento]
    @codMovimento BIGINT,
    @quantidadeMeses INT
AS

IF NOT EXISTS (SELECT 1 FROM Movimento WHERE CodMovimento = @codMovimento )
    RETURN 1 -- O movimento informado para replicação não existe.

IF EXISTS (SELECT 1 FROM Movimento WHERE CodMovimento = @codMovimento AND NumeroParcela IS NOT NULL)
	RETURN 2 -- Não é possível replicar uma movimentação que faça parte de um parcelamento.


DECLARE @mes INT = 1

WHILE (@mes <= @quantidadeMeses)
BEGIN
    
    INSERT Movimento (Ativo, Data, MesAno, CodMovimentoDescricao, CodCategoria, Valor, CodConta, CodCartao, Observacao)
    SELECT Ativo, DATEADD(MONTH, @mes, Data), DATEADD(MONTH, @mes, MesAno), CodMovimentoDescricao, CodCategoria, Valor, CodConta, CodCartao, Observacao
    FROM Movimento
    WHERE CodMovimento = @codMovimento

    SET @mes = @mes + 1

END

RETURN 0