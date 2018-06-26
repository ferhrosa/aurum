using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServicoAurum.Dados;
using ServicoAurum.Excecoes;

namespace ServicoAurum.Entidades
{
    public class Conta : Entidade
    {

        #region Propriedades dos campos da tabela

        public int? CodConta { get; set; }
        public bool Ativo { get; set; }
        public string Banco { get; set; }
        public byte Tipo { get; set; }
        public string AgenciaConta { get; set; }

        #endregion Propriedades dos campos da tabela


        /// <summary>
        /// Carrega uma lista de registros da tabela Conta.
        /// </summary>
        internal static List<Conta> Listar()
        {
            return Carregar(null);
        }

        /// <summary>
        /// Carrega o registro da tabela Conta que possui o código informado.
        /// </summary>
        internal static Conta Carregar(int codConta)
        {
            return Carregar((int?)codConta).First();
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Conta ou somente o registro que possui o código informado.
        /// </summary>
        private static List<Conta> Carregar(int? codConta)
        {
            using ( var sp = new StoredProcedure("CarregarConta") )
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codConta", codConta);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<Conta>();

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Registros carregados com sucesso.
                        return resultado;
                    case 1:  // A conta informada não existe.
                        throw new Exception("A conta informada não existe.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        ///// <summary>
        ///// Salva as alterações efetuadas na instância deste objeto ou cria um novo registro com os dados da instância.
        ///// </summary>
        //public void Salvar()
        //{
        //    using ( var sp = new StoredProcedure("SalvarCartao") )
        //    {
        //        // Define os parâmetros a serem passados ao stored procedure.
        //        sp.DefinirParametro("@codCartao", this.CodCartao);
        //        sp.DefinirParametro("@ativo", this.Ativo);
        //        sp.DefinirParametro("@codConta", this.CodConta);
        //        sp.DefinirParametro("@descricao", this.Descricao);
        //        sp.DefinirParametro("@numero", this.Numero);
        //        sp.DefinirParametro("@titular", this.Titular);
        //        sp.DefinirParametro("@vencimento", this.Vencimento);
        //        sp.DefinirParametro("@validade", this.Validade);
        //        sp.DefinirParametro("@possuiAdicional", this.PossuiAdicional);
        //        sp.DefinirParametro("@telefoneSac", this.TelefoneSac);

        //        // Executa o stored procedure e carrega o código do registro salvo.
        //        this.CodCartao = sp.CarregarUmRegistro<Cartao>().CodCartao;

        //        // Faz o tratamento do retorno do stored procedure.
        //        switch ( sp.Retorno )
        //        {
        //            case 0:  // Salvo com sucesso.
        //                return;
        //            case 1:  // O cartão informado para alteração não existe.
        //                throw new Exception("O cartão informado para alteração não existe.");
        //            default:  // O valor retornado pelo stored procedure era inesperado.
        //                throw new ExRetornoInesperado(sp);
        //        }
        //    }
        //}
        
        ///// <summary>
        ///// Exclui o registro que contém o código informado.
        ///// </summary>
        ///// <param name="codCartao">Código do cartão a ser excluído.</param>
        //internal static void Excluir(int codCartao)
        //{
        //    using ( var sp = new StoredProcedure("ExcluirCartao") )
        //    {
        //        // Define os parâmetros a serem passados ao stored procedure.
        //        sp.DefinirParametro("@codCartao", codCartao);

        //        // Executa o stored procedure.
        //        sp.Executar();

        //        // Faz o tratamento do retorno do stored procedure.
        //        switch ( sp.Retorno )
        //        {
        //            case 0:  // Excluído com sucesso.
        //                return;
        //            case 1:  // O cartão informado para exclusão não existe.
        //                throw new Exception("O cartão informado para exclusão não existe.");
        //            default:  // O valor retornado pelo stored procedure era inesperado.
        //                throw new ExRetornoInesperado(sp);
        //        }
        //    }
        //}

    }
}