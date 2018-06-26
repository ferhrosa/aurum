/*
    Data da criação: 21.04.2011
    
    Última alteração
        Responsável: Fernando
        Data: 21.04.2011
        Descrição: Criação do script
*/

CREATE TABLE Cartao (
    CodCartao       INT         IDENTITY(1, 1)              ,
    Ativo           BIT         NOT NULL        DEFAULT 1   ,
    CodConta        INT                                     ,
    Descricao       VARCHAR(30) NOT NULL                    ,
    Numero          VARCHAR(25) NOT NULL                    ,
    Titular         VARCHAR(30) NOT NULL                    ,
    Vencimento      TINYINT     NOT NULL                    ,
    Validade        DATETIME                                ,
    PossuiAdicional BIT         NOT NULL        DEFAULT 0   ,
    TelefoneSac     VARCHAR(15)                             ,
    
    CONSTRAINT PK_Cartao        PRIMARY KEY (CodCartao),
    CONSTRAINT FK_Cartao_Conta  FOREIGN KEY (CodConta)  REFERENCES Conta(CodConta)
)