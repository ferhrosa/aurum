/*
    Data da criação: 22.04.2011
    
    Última alteração
        Responsável: Fernando
        Data: 22.04.2011
        Descrição: Criação do script.
*/

CREATE TABLE Categoria (
    CodCategoria    INT         IDENTITY(1, 1)              ,
    Ativo           BIT         NOT NULL        DEFAULT 1   ,
    Descricao       VARCHAR(50) NOT NULL                    ,
    
    CONSTRAINT PK_Categoria PRIMARY KEY (CodCategoria)
)