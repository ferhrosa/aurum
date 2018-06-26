CREATE PROCEDURE Cartao_Excluir(@CodCartao INT)
AS

DELETE Cartao
WHERE CodCartao = @CodCartao

GO

--EXEC Cartao_Salvar 0, 1, 'Itaú', 1, '00000'
EXEC Cartao_Excluir 3

GO


DROP PROCEDURE Cartao_Excluir

GO

SELECT * FROM Cartao
