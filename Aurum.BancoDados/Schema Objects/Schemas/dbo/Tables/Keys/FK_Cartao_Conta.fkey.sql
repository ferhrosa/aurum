﻿ALTER TABLE [dbo].[Cartao]
    ADD CONSTRAINT [FK_Cartao_Conta] FOREIGN KEY ([CodConta]) REFERENCES [dbo].[Conta] ([CodConta]) ON DELETE NO ACTION ON UPDATE NO ACTION;

