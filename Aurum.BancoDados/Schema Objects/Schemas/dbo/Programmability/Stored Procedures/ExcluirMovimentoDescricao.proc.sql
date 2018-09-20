CREATE PROCEDURE [dbo].[ExcluirMovimentoDescricao]
    @codMovimentoDescricao INT
AS

IF EXISTS (SELECT 1 FROM Movimento WHERE CodMovimentoDescricao = @codMovimentoDescricao)
	RETURN 2 -- Movimento vinculado à descrição

DELETE MovimentoDescricao
WHERE CodMovimentoDescricao = @codMovimentoDescricao

IF @@ROWCOUNT > 0
    RETURN 0
ELSE
    RETURN 1