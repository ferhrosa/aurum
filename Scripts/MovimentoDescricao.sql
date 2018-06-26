/*
    Data da cria��o: 22.04.2011
    
    �ltima altera��o
        Respons�vel: Fernando
        Data: 22.04.2011
        Descri��o: Cria��o do script.
*/

CREATE TABLE MovimentoDescricao (
    CodMovimentoDescricao INT IDENTITY(1, 1),
    Descricao VARCHAR(50) NOT NULL,
    
    CONSTRAINT PK_MovimentoDescricao PRIMARY KEY (CodMovimentoDescricao)
)