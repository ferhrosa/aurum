using System;
using System.Collections.Generic;
using System.Linq;
using Aurum.Dados.Acesso;
using Aurum.Dados.Excecoes;

namespace Aurum.Dados.Entidades
{
    public class Cartao : Aurum.Entidades.Cartao
    {

        /// <summary>
        /// Carrega uma lista de registros da tabela Cartao.
        /// </summary>
        public static List<Aurum.Entidades.Cartao> Listar()
        {
            return Carregar(null);
        }

        /// <summary>
        /// Carrega o registro da tabela Cartao que possui o código informado.
        /// </summary>
        public static Aurum.Entidades.Cartao Carregar(int codCartao)
        {
            return Carregar((int?)codCartao).First();
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Cartao ou somente o registro que possui o código informado.
        /// </summary>
        private static List<Aurum.Entidades.Cartao> Carregar(int? codCartao)
        {
            using ( var sp = new StoredProcedure("CarregarCartao") )
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCartao", codCartao);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<Aurum.Entidades.Cartao>();

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Registros carregados com sucesso.
                        return resultado;
                    case 1:
                        throw new Exception("O cartão informado não existe.");
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
            using ( var sp = new StoredProcedure("SalvarCartao") )
            {
                // Na inclusão de registro força o valor true para o campo Ativo.
                if ( !this.CodCartao.HasValue )
                {
                    this.Ativo = true;
                }

                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCartao", this.CodCartao);
                sp.DefinirParametro("@ativo", this.Ativo);
                sp.DefinirParametro("@codConta", this.CodConta);
                sp.DefinirParametro("@descricao", this.Descricao);
                sp.DefinirParametro("@numero", this.Numero);
                sp.DefinirParametro("@titular", this.Titular);
                sp.DefinirParametro("@validade", this.Validade);

                // Executa o stored procedure e carrega o código do registro salvo.
                this.CodCartao = sp.CarregarUmRegistro<Cartao>().CodCartao;

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Salvo com sucesso.
                        return;
                    case 1:
                        throw new Exception("O cartão informado para alteração não existe.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Exclui o registro que contém o código informado.
        /// </summary>
        /// <param name="codCartao">Código do cartão a ser excluído.</param>
        public static void Excluir(int codCartao)
        {
            using ( var sp = new StoredProcedure("ExcluirCartao") )
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCartao", codCartao);

                // Executa o stored procedure.
                sp.Executar();

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Excluído com sucesso.
                        return;
                    case 1: 
                        throw new Exception("O cartão informado para exclusão não existe.");
                    case 2:
                        throw new Exception("O cartão informado para exclusão possui movimento(s) vinculado(s).");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

    }
}