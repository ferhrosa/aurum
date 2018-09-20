using System;
using Aurum.Dados.Acesso;

namespace Aurum.Dados.Excecoes
{
    /// <summary>
    /// Classe de exceção, criada quando o registro tentando ser alterado ou excluído não existir.
    /// </summary>
    public class ExRegistroInexistente : ExRetornoStoredProcedure
    {
        /// <summary>
        /// Método construtor da exceção.
        /// </summary>
        /// <param name="sp">Stored procedure que retornou tentou alterar ou exlcluir um registro inexistente.</param>
        public ExRegistroInexistente(string mensagem, StoredProcedure sp)
            // Executa o construtor da classe base, informando a mensagem de erro.
            : base(mensagem, sp)
        {
        }
    }
}