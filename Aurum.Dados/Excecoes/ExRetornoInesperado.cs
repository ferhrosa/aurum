using System;
using Aurum.Dados.Acesso;

namespace Aurum.Dados.Excecoes
{
    /// <summary>
    /// Classe de exceção, criada quando o valor retornado por um stored procedure não era esperado pelo aplicativo.
    /// </summary>
    public class ExRetornoInesperado : ExRetornoStoredProcedure
    {
        /// <summary>
        /// Método construtor da exceção.
        /// </summary>
        /// <param name="sp">Stored procedure que retornou um valor inesperado pelo aplicativo.</param>
        public ExRetornoInesperado(StoredProcedure sp)
            // Executa o construtor da classe base, informando a mensagem de erro.
            : base(
                String.Format(
                    "O stored procedure \"{0}\" retornou um resultado inesperado ({1}).",
                    sp.CommandText,
                    sp.Retorno),
                sp
            )
        {
        }
    }
}