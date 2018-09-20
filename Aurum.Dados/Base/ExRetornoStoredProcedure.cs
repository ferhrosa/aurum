using System;
using Aurum.Dados.Acesso;

namespace Aurum.Dados.Excecoes
{
    /// <summary>
    /// Classe de exceção, criada quando houver um erro determinado pelo retorno de um stored procedure.
    /// </summary>
    public abstract class ExRetornoStoredProcedure : Exception
    {
        /// <summary>
        /// Stored procedure que foi executado e retornou um valor que representa um erro.
        /// </summary>
        public StoredProcedure StoredProcedure { get; private set; }


        /// <summary>
        /// Método construtor da exceção.
        /// </summary>
        /// <param name="sp">Stored procedure que foi executado e retornou um valor que representa um erro.</param>
        public ExRetornoStoredProcedure(string mensagem, StoredProcedure sp)
            // Executa o construtor da classe base, informando a mensagem de erro.
            :base(mensagem)
        {
            // Armazena a instância do stored procedure que foi executado e retornou um valor que representa um erro.
            this.StoredProcedure = sp;
        }
    }
}