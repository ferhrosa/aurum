﻿ALTER TABLE [dbo].[Taxa]
    ADD CONSTRAINT [FK_Taxa_Cartao] FOREIGN KEY ([CodCartao]) REFERENCES [dbo].[Cartao] ([CodCartao]) ON DELETE NO ACTION ON UPDATE NO ACTION;

