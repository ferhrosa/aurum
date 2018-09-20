using System;
using System.Collections.Generic;
using System.Linq;
using Aurum.Dados.Acesso;
using Aurum.Dados.Excecoes;
using Aurum.Entidades;

namespace Aurum.Dados.Entidades
{
    public class Categoria : Aurum.Entidades.Categoria
    {

        /// <summary>
        /// Carrega uma lista de registros da tabela Categoria.
        /// </summary>
        public static List<Entidades.Categoria> Listar()
        {
            return Carregar(null);
        }

        /// <summary>
        /// Carrega o registro da tabela Categoria que possui o código informado.
        /// </summary>
        public static Entidades.Categoria Carregar(int codCategoria)
        {
            return Carregar((int?)codCategoria).First();
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Categoria ou somente o registro que possui o código informado.
        /// </summary>
        private static List<Entidades.Categoria> Carregar(int? codCategoria)
        {
            using (var sp = new StoredProcedure("CarregarCategoria"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCategoria", codCategoria);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<Categoria>();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Registros carregados com sucesso.
                        return resultado;
                    case 1:
                        throw new Exception("A categoria informada não existe.");
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
            using (var sp = new StoredProcedure("SalvarCategoria"))
            {
                // Na inclusão de registro força o valor true para o campo Ativo.
                if ( !this.CodCategoria.HasValue )
                {
                    this.Ativo = true;
                }

                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCategoria", this.CodCategoria);
                sp.DefinirParametro("@ativo", this.Ativo);
                sp.DefinirParametro("@descricao", this.Descricao);

                // Executa o stored procedure e carrega o código do registro salvo.
                this.CodCategoria = sp.CarregarUmRegistro<Categoria>().CodCategoria;

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Salvo com sucesso.
                        return;
                    case 1:
                        throw new ExRegistroInexistente("A categoria informada para alteração não existe.", sp);
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Exclui o registro que contém o código informado.
        /// </summary>
        /// <param name="codCategoria">Código da categoria a ser excluída.</param>
        public static void Excluir(int codCategoria)
        {
            using (var sp = new StoredProcedure("ExcluirCategoria"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCategoria", codCategoria);

                // Executa o stored procedure.
                sp.Executar();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Excluído com sucesso.
                        return;
                    case 1:
                        throw new ExRegistroInexistente("A categoria informada para exclusão não existe.", sp);
                    case 2:
                        throw new Exception("A categoria informada para exclusão possui movimento(s) vinculado(s).");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

    }
}