using System;
using ServicoAurum.Dados;

namespace ServicoAurum.Excecoes
{
    /// <summary>
    /// Classe de exceção, criada quando o valor retornado por um stored procedure não era esperado pelo aplicativo.
    /// </summary>
    public class ExRetornoInesperado : Exception
    {
        /// <summary>
        /// Stored procedure que retornou um valor inesperado pelo aplicativo.
        /// </summary>
        public StoredProcedure StoredProcedure { get; private set; }


        /// <summary>
        /// Método construtor da exceção.
        /// </summary>
        /// <param name="sp">Stored procedure que retornou um valor inesperado pelo aplicativo.</param>
        public ExRetornoInesperado(StoredProcedure sp)
            // Executa o construtor da classe base, informando a mensagem de erro.
            :base(String.Format("O stored procedure \"{0}\" retornou um resultado inesperado ({1}).", sp.CommandText, sp.Retorno))
        {
            // Armazena a instância do stored procedure que retornou um valor inesperado pelo aplicativo.
            this.StoredProcedure = sp;
        }
    }
}