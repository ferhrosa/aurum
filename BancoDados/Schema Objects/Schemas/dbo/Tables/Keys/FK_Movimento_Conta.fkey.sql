ALTER TABLE [dbo].[Movimento]
    ADD CONSTRAINT [FK_Movimento_Conta] FOREIGN KEY ([CodConta]) REFERENCES [dbo].[Conta] ([CodConta]) ON DELETE NO ACTION ON UPDATE NO ACTION;

