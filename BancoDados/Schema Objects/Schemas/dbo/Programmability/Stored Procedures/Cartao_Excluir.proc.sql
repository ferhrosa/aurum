CREATE PROCEDURE Cartao_Excluir(@CodCartao INT)
AS

DELETE Cartao
WHERE CodCartao = @CodCartao

