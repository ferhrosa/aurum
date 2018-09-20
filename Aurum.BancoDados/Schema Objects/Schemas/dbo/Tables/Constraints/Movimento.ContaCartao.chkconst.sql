ALTER TABLE [dbo].[Movimento]
	ADD CONSTRAINT [CK_Movimento_ContaCartao] 
	CHECK  (
		(CodConta IS NOT NULL AND CodCartao IS NULL)
        OR (CodCartao IS NOT NULL AND CodConta IS NULL)
	)