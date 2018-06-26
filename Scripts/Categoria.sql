/*
    Data da cria��o: 22.04.2011
    
    �ltima altera��o
        Respons�vel: Fernando
        Data: 22.04.2011
        Descri��o: Cria��o do script.
*/

CREATE TABLE Categoria (
    CodCategoria    INT         IDENTITY(1, 1)              ,
    Ativo           BIT         NOT NULL        DEFAULT 1   ,
    Descricao       VARCHAR(50) NOT NULL                    ,
    
    CONSTRAINT PK_Categoria PRIMARY KEY (CodCategoria)
)