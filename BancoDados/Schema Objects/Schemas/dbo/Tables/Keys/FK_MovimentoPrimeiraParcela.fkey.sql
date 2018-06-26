ALTER TABLE [dbo].[Movimento]
    ADD CONSTRAINT [FK_MovimentoPrimeiraParcela] FOREIGN KEY ([CodMovimentoPrimeiraParcela]) REFERENCES [dbo].[Movimento] ([CodMovimento]) ON DELETE NO ACTION ON UPDATE NO ACTION;

