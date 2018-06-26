ALTER TABLE [dbo].[Movimento]
    ADD CONSTRAINT [FK_Movimento_Categoria] FOREIGN KEY ([CodCategoria]) REFERENCES [dbo].[Categoria] ([CodCategoria]) ON DELETE NO ACTION ON UPDATE NO ACTION;

