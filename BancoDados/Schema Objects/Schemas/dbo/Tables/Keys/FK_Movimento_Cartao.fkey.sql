ALTER TABLE [dbo].[Movimento]
    ADD CONSTRAINT [FK_Movimento_Cartao] FOREIGN KEY ([CodCartao]) REFERENCES [dbo].[Cartao] ([CodCartao]) ON DELETE NO ACTION ON UPDATE NO ACTION;

