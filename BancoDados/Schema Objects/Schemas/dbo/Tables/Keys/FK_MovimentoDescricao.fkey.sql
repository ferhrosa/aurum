ALTER TABLE [dbo].[Movimento]
    ADD CONSTRAINT [FK_MovimentoDescricao] FOREIGN KEY ([CodMovimentoDescricao]) REFERENCES [dbo].[MovimentoDescricao] ([CodMovimentoDescricao]) ON DELETE NO ACTION ON UPDATE NO ACTION;

