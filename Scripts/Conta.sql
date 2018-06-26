/*
    Data da criação: 21.04.2011
    
    Última alteração
        Responsável: Patrícia
        Data: 01.05.2011
        Descrição: Aumentar a coluna de Agência e Conta
*/

CREATE TABLE Conta (
    CodConta        INT         IDENTITY(1, 1)              ,
    Ativo           BIT         NOT NULL        DEFAULT 1   ,
    Banco           VARCHAR(20) NOT NULL                    ,
    Tipo            TINYINT     NOT NULL                    ,
    AgenciaConta    VARCHAR(15) NOT NULL                    ,
    
    CONSTRAINT PK_Conta PRIMARY KEY (CodConta)
)

GO

ALTER TABLE
    Conta
ALTER COLUMN
    AgenciaConta VARCHAR(30)