using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServicoAurum.Dados;
using ServicoAurum.Excecoes;

namespace ServicoAurum.Entidades
{
    public class Cartao : Entidade
    {

        #region Propriedades dos campos da tabela

        public int? CodCartao { get; set; }
        public bool Ativo { get; set; }
        public int? CodConta { get; set; }
        public string Descricao { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public byte Vencimento { get; set; }
        public DateTime? Validade { get; set; }
        public bool PossuiAdicional { get; set; }
        public string TelefoneSac { get; set; }

        #endregion Propriedades dos campos da tabela

        #region Propriedades de entidades filhas

        [EntidadeFilha]
        public Conta Conta { get; set; }

        #endregion Propriedades de entidades filhas


        /// <summary>
        /// Carrega uma lista de registros da tabela Cartao.
        /// </summary>
        internal static List<Cartao> Listar()
        {
            return Carregar(null);
        }

        /// <summary>
        /// Carrega o registro da tabela Cartao que possui o código informado.
        /// </summary>
        internal static Cartao Carregar(int codCartao)
        {
            return Carregar((int?)codCartao).First();
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Cartao ou somente o registro que possui o código informado.
        /// </summary>
        private static List<Cartao> Carregar(int? codCartao)
        {
            using ( var sp = new StoredProcedure("CarregarCartao") )
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCartao", codCartao);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<Cartao>();

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Registros carregados com sucesso.
                        return resultado;
                    case 1:  // O cartão informado não existe.
                        throw new Exception("O cartão informado não existe.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Salva as alterações efetuadas na instância deste objeto ou cria um novo registro com os dados da instância.
        /// </summary>
        public void Salvar()
        {
            using ( var sp = new StoredProcedure("SalvarCartao") )
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codCartao", this.CodCartao);
                sp.DefinirParametro("@ativo", this.Ativo);
                sp.DefinirParametro("@codConta", this.CodConta);
                sp.DefinirParametro("@descricao", this.Descricao);
                sp.DefinirParametro("@numero", this.Numero);
                sp.DefinirParametro("@titular", this.Titular);
                sp.DefinirParametro("@vencimento", this.Vencimento);
                sp.DefinirParametro("@validade", this.Validade);
                sp.DefinirParametro("@possuiAdicional", this.PossuiAdicional);
                sp.DefinirParametro("@telefoneSac", this.TelefoneSac);

                // Executa o stored procedure e carrega o código do registro salvo.
                this.CodCartao = sp.CarregarUmRegistro<Cartao>().CodCartao;

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Salvo com sucesso.
                        return;
                    case 1:  // O cartão informado para alteração não existe.
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
        internal static void Excluir(int codCartao)
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
                    case 1:  // O cartão informado para exclusão não existe.
                        throw new Exception("O cartão informado para exclusão não existe.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

    }
}