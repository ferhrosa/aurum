using System;
using Aurum.Dados.Acesso;

namespace Aurum.Dados.Excecoes
{
    /// <summary>
    /// Classe de exceção, criada quando o valor retornado por um stored procedure não era esperado pelo aplicativo.
    /// </summary>
    public class ExReplicacaoMovimentoParcela : ExRetornoStoredProcedure
    {
        /// <summary>
        /// Método construtor da exceção.
        /// </summary>
        /// <param name="sp">Stored procedure que tentou replicar uma movimentação que faz parte de um parcelamento.</param>
        public ExReplicacaoMovimentoParcela(StoredProcedure sp)
            // Executa o construtor da classe base, informando a mensagem de erro.
            : base(
                String.Format(
                    "O movimento {0} não pode ser replicado pois faz parte de um parcelamento.",
                    sp.CarregarParametro("@codMovimento")),
                sp
            )
        {
        }
    }
}