ALTER TABLE [dbo].[Movimento]
    ADD CONSTRAINT [FK_MovimentoReserva] FOREIGN KEY ([CodMovimentoReserva]) REFERENCES [dbo].[Movimento] ([CodMovimento]) ON DELETE NO ACTION ON UPDATE NO ACTION;

