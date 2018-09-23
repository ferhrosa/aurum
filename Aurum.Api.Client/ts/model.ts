export enum TipoConta {
    Corrente = 1,
    Poupanca = 2,
}

export interface Cartao {
    Id?: number;
    Descricao: string;
    IdConta?: number;
    Conta: Conta;
}

export interface Conta {
    Id?: number;
    Descricao: string;
    Tipo: TipoConta;
    AgenciaConta: string;
}

export interface Categoria {
    Id?: number;
    Nome: string;
}

export interface Movimentacao {
    Id?: string;
    IdCategoria: number;
    IdConta?: number;
    Conta: Conta;
    IdCartao?: number;
    Cartao: Cartao;
    Data: Date;
    Descricao: string;
    Valor: number;
    Efetivada: boolean;
    DataHoraInclusao: Date;
    IdMovimentacaoPrimeiraParcela?: string;
    MovimentacaoPrimeiraParcela: Movimentacao;
    NumeroParcela?: number;
    TotalParcelas?: number;
    Observacao: string;
}

