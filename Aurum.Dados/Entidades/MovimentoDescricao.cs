using System;
using System.Collections.Generic;
using System.Linq;
using Aurum.Dados.Acesso;
using Aurum.Dados.Excecoes;

namespace Aurum.Dados.Entidades
{
    public class MovimentoDescricao : Aurum.Entidades.MovimentoDescricao
    {

        /// <summary>
        /// Carrega uma lista de registros da tabela MovimentoDescricao.
        /// </summary>
        public static List<MovimentoDescricao> Listar(string inicioDescricao)
        {
            return Carregar(null, inicioDescricao);
        }

        /// <summary>
        /// Carrega o registro da tabela MovimentoDescricao que possui o código informado.
        /// </summary>
        public static MovimentoDescricao Carregar(int codMovimentoDescricao)
        {
            return Carregar((int?)codMovimentoDescricao, null).First();
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela MovimentoDescricao ou somente o registro que possui o código informado.
        /// </summary>
        private static List<MovimentoDescricao> Carregar(int? codMovimentoDescricao, string inicioDescricao)
        {
            using (var sp = new StoredProcedure("CarregarMovimentoDescricao"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimentoDescricao", codMovimentoDescricao);
                sp.DefinirParametro("@inicioDescricao", inicioDescricao);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<MovimentoDescricao>();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Registros carregados com sucesso.
                        return resultado;
                    case 1:
                        throw new Exception("A descrição de movimento informada não existe.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Salva AS alterações efetuadas na instância deste objeto ou cria um novo registro com os dados da instância.
        /// </summary>
        public void Salvar()
        {
            using (var sp = new StoredProcedure("SalvarMovimentoDescricao"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimentoDescricao", this.CodMovimentoDescricao);
                sp.DefinirParametro("@descricao", this.Descricao);

                // Executa o stored procedure e carrega o código do registro salvo.
                this.CodMovimentoDescricao = sp.CarregarUmRegistro<MovimentoDescricao>().CodMovimentoDescricao;

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Salvo com sucesso.
                        return;
                    case 1:
                        throw new Exception("A descrição de movimento informada para alteração não existe.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Exclui o registro que contém o código informado.
        /// </summary>
        /// <param name="codMovimentoDescricao">Código da descrição do movimento a ser excluída.</param>
        public static void Excluir(int codMovimentoDescricao)
        {
            using (var sp = new StoredProcedure("ExcluirMovimentoDescricao"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimentoDescricao", codMovimentoDescricao);

                // Executa o stored procedure.
                sp.Executar();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Excluído com sucesso.
                        return;
                    case 1:
                        throw new Exception("A descrição de movimento informada para exclusão não existe.");
                    case 2:
                        throw new Exception("A descrição de movimento informada para exclusão possui movimento(s) vinculado(s).");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

    }
}