using System;
using ServicoAurum.Entidades;
using System.Collections.Generic;

namespace ServicoAurum
{
    public class Aurum : IAurum
    {

        #region Cartão

        /// <summary>
        /// Carrega uma lista de registros da tabela Cartao.
        /// </summary>
        public List<Cartao> ListarCartao()
        {
            return Cartao.Listar();
        }

        /// <summary>
        /// Salva as alterações efetuadas na instância deste objeto ou cria um novo registro com os dados da instância.
        /// </summary>
        /// <returns>Retorna o código do cartão salvo.</returns>
        public int SalvarCartao(int? codCartao, bool ativo, int? codConta, string descricao, string numero, string titular, byte vencimento, DateTime? validade, bool possuiAdicional, string telefoneSac)
        {
            // Cria uma instância do cartão com os dados informados.
            var cartao = new Cartao()
            {
                CodCartao = codCartao,
                Ativo = ativo,
                CodConta = codConta,
                Descricao = descricao,
                Numero = numero,
                Titular = titular,
                Vencimento = vencimento,
                Validade = validade,
                PossuiAdicional = possuiAdicional,
                TelefoneSac = telefoneSac
            };

            // Executa o procedimento de salvar o cartão.
            cartao.Salvar();

            // Retorna o código do cartão que foi salvo.
            return cartao.CodCartao.Value;
        }

        /// <summary>
        /// Exclui o cartão que possui o código informado.
        /// </summary>
        /// <param name="codCartao">Código do cartão a ser excluído.</param>
        public void ExcluirCartao(int codCartao)
        {
            // Executa o procedimento de exclusão do cartão com o código informado.
            Cartao.Excluir(codCartao);
        }

        #endregion Cartão


        #region Conta

        // Carrega uma lista de registros da tabela Conta.
        public List<Conta> ListarConta()
        {
            return Conta.Listar();
        }

        #endregion Conta
    }
}
