using System;
using System.Collections.Generic;
using System.Linq;
using Aurum.Dados.Acesso;
using Aurum.Dados.Excecoes;

namespace Aurum.Dados.Entidades
{
    public class Conta : Aurum.Entidades.Conta
    {

        /// <summary>
        /// Carrega uma lista de registros da tabela Conta.
        /// </summary>
        public static List<Conta> Listar()
        {
            return Carregar(null);
        }

        /// <summary>
        /// Carrega o registro da tabela Conta que possui o código informado.
        /// </summary>
        public static Conta Carregar(int codConta)
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
                    case 1: 
                        throw new Exception("A conta informada não existe.");
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
            using (var sp = new StoredProcedure("SalvarConta"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codConta", this.CodConta);
                sp.DefinirParametro("@ativo", this.Ativo);
                sp.DefinirParametro("@banco", this.Banco);
                sp.DefinirParametro("@tipo", this.Tipo);
                sp.DefinirParametro("@agenciaConta", this.AgenciaConta);

                // Executa o stored procedure e carrega o código do registro salvo.
                this.CodConta = sp.CarregarUmRegistro<Conta>().CodConta;

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Salvo com sucesso.
                        return;
                    case 1: 
                        throw new Exception("A conta informada para alteração não existe.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Exclui o registro que contém o código informado.
        /// </summary>
        /// <param name="codConta">Código da conta a ser excluída.</param>
        public static void Excluir(int codConta)
        {
            using (var sp = new StoredProcedure("ExcluirConta"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codConta", codConta);

                // Executa o stored procedure.
                sp.Executar();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Excluído com sucesso.
                        return;
                    case 1:
                        throw new Exception("A conta informada para exclusão não existe.");
                    case 2:
                        throw new Exception("A conta informada para exclusão possui cartão(s) vinculado(s).");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

    }
}