/***************************************************************/
-- Conta
SET IDENTITY_INSERT Aurum_Desenv.dbo.Conta ON

INSERT Aurum_Desenv.dbo.Conta (CodConta, Ativo, Banco, Tipo, AgenciaConta)
SELECT * FROM notepaty2.Aurum.dbo.Conta

SET IDENTITY_INSERT Aurum_Desenv.dbo.Conta OFF
/***************************************************************/

/***************************************************************/
-- Cartao
SET IDENTITY_INSERT Aurum_Desenv.dbo.Cartao ON

INSERT Aurum_Desenv.dbo.Cartao (CodCartao, Ativo, CodConta, Descricao, Numero, Titular, Vencimento, Validade, PossuiAdicional, TelefoneSac)
SELECT CodCartao, Ativo, CodConta, Descricao, Numero, Titular, Vencimento, Validade, PossuiAdicional, TelefoneSac
FROM notepaty2.Aurum.dbo.Cartao

SET IDENTITY_INSERT Aurum_Desenv.dbo.Cartao OFF
/***************************************************************/

/***************************************************************/
-- Categoria
SET IDENTITY_INSERT Aurum_Desenv.dbo.Categoria ON

INSERT Aurum_Desenv.dbo.Categoria (CodCategoria, Ativo, Descricao)
SELECT CodCategoria, Ativo, Descricao
FROM notepaty2.Aurum.dbo.Categoria

SET IDENTITY_INSERT Aurum_Desenv.dbo.Categoria OFF
/***************************************************************/

/***************************************************************/
-- MovimentoDescricao
SET IDENTITY_INSERT Aurum_Desenv.dbo.MovimentoDescricao ON

INSERT Aurum_Desenv.dbo.MovimentoDescricao (CodMovimentoDescricao, Descricao)
SELECT CodMovimentoDescricao, Descricao
FROM notepaty2.Aurum.dbo.MovimentoDescricao

SET IDENTITY_INSERT Aurum_Desenv.dbo.MovimentoDescricao OFF
/***************************************************************/

/***************************************************************/
-- Movimento
SET IDENTITY_INSERT Aurum_Desenv.dbo.Movimento ON

INSERT Aurum_Desenv.dbo.Movimento (CodMovimento, Ativo, DataHoraInclusao, Data, MesAno, CodMovimentoDescricao, CodCategoria, Valor, CodConta, CodCartao, Reserva, CodMovimentoReserva, Observacao)
SELECT CodMovimento, Ativo, DataHoraInclusao, Data, MesAno, CodMovimentoDescricao, CodCategoria, Valor, CodConta, CodCartao, Reserva, CodMovimentoReserva, Observacao
FROM notepaty2.Aurum.dbo.Movimento

SET IDENTITY_INSERT Aurum_Desenv.dbo.Movimento OFF
/***************************************************************/
