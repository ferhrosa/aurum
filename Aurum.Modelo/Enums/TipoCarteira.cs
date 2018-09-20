using System.ComponentModel;

namespace Aurum.Modelo.Enums
{
    public enum TipoCarteira
    {
        [Description("Dinheiro")]
        Dinheiro = 1,

        [Description("Conta Corrente")]
        ContaCorrente = 2,

        [Description("Conta Poupança")]
        ContaPoupanca = 3,

        [Description("Cartão Crédito")]
        CartaoCredito = 4
    }
}
