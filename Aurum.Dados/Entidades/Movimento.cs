using System;
using System.Collections.Generic;
using System.Linq;
using Aurum.Dados.Acesso;
using Aurum.Dados.Excecoes;

namespace Aurum.Dados.Entidades
{
    public class Movimento : Aurum.Entidades.Movimento
    {

        #region Cadastro de Movimentos

        /// <summary>
        /// Carrega uma lista de registros da tabela Movimento.
        /// </summary>
        public static List<Movimento> Listar(DateTime? mesAno)
        {
            return Carregar(null, mesAno);
        }

        /// <summary>
        /// Carrega o registro da tabela Movimento que possui o código informado.
        /// </summary>
        public static Movimento Carregar(long codMovimento)
        {
            return Carregar((long?)codMovimento, null).First();
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Movimento ou somente o registro que possui o código informado.
        /// </summary>
        private static List<Movimento> Carregar(long? codMovimento, DateTime? mesAno)
        {
            using (var sp = new StoredProcedure("CarregarMovimento"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimento", codMovimento);
                sp.DefinirParametro("@mesAno", mesAno);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<Movimento>();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Registros carregados com sucesso.
                        return resultado;
                    case 1:
                        throw new Exception("O movimento informado não existe.");
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
            using (var sp = new StoredProcedure("SalvarMovimento"))
            {
                if ( this.CodMovimentoDescricao > 0 && !String.IsNullOrWhiteSpace(this.MovimentoDescricao.Descricao) )
                {
                    // Carrega a descrição do banco de dados que contém o código informado.
                    var movimentoDescricaoBanco = Aurum.Dados.Entidades.MovimentoDescricao.Carregar(this.CodMovimentoDescricao);

                    // Se a descrição informada for diferente da descrição do código informado, força a inserção de nova descrição com o texto informado.
                    if ( this.MovimentoDescricao.Descricao != movimentoDescricaoBanco.Descricao )
                        this.CodMovimentoDescricao = 0;
                }

                // Salva a descrição quando não tiver ID
                if (this.CodMovimentoDescricao == 0)
                {
                    // Cria nova descrição no banco de dados.
                    var movimentoDescricao = new MovimentoDescricao() { Descricao = this.MovimentoDescricao.Descricao };
                    movimentoDescricao.Salvar();

                    // Usa a nova descrição no registro de movimento.
                    this.CodMovimentoDescricao = movimentoDescricao.CodMovimentoDescricao.Value;
                    this.MovimentoDescricao = movimentoDescricao;
                }

                var incluindo = (!this.CodMovimento.HasValue);

                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimento", this.CodMovimento);
                sp.DefinirParametro("@data", this.Data);
                sp.DefinirParametro("@mesAno", this.MesAno);
                sp.DefinirParametro("@consolidado", this.Consolidado);
                sp.DefinirParametro("@codMovimentoDescricao", this.CodMovimentoDescricao);
                sp.DefinirParametro("@codCategoria", this.CodCategoria);
                sp.DefinirParametro("@valor", this.Valor);
                sp.DefinirParametro("@codConta", this.CodConta);
                sp.DefinirParametro("@codCartao", this.CodCartao);
                sp.DefinirParametro("@codMovimentoPrimeiraParcela", this.CodMovimentoPrimeiraParcela);
                sp.DefinirParametro("@numeroParcela", this.NumeroParcela);
                sp.DefinirParametro("@totalParcelas", this.TotalParcelas);
                sp.DefinirParametro("@observacao", this.Observacao);

                // Executa o stored procedure e carrega o código do registro salvo.
                this.CodMovimento = sp.CarregarUmRegistro<Movimento>().CodMovimento;

                // Se estiver inserindo e possuir informações de parcelamento...
                if ( incluindo && !this.CodMovimentoPrimeiraParcela.HasValue && this.NumeroParcela.HasValue && this.TotalParcelas.HasValue )
                {
                    this.CodMovimentoPrimeiraParcela = this.CodMovimento;

                    for ( byte i = 1; i <= (this.TotalParcelas.Value - this.NumeroParcela.Value); i++ )
                    {
                        // Cria uma cópia do movimento atual, que será uma das parcelas.
                        var parcela = this.Clonar<Movimento>();
                        
                        parcela.CodMovimento = null;
                        parcela.Consolidado = false;
                        parcela.MesAno = this.MesAno.AddMonths(i);
                        parcela.NumeroParcela = (byte?)(this.NumeroParcela + i);
                        parcela.CodMovimentoPrimeiraParcela = this.CodMovimento;

                        parcela.Salvar();
                    }
                }

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Salvo com sucesso.
                        return;
                    case 1:
                        throw new Exception("O movimento informado para alteração não existe.");
                    case 2:
                        throw new Exception("Só pode ser selecionado um cartão ou uma conta.");
                    case 3:
                        throw new Exception("Não foi selecionado um cartão ou uma conta.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Exclui o registro que contém o código informado.
        /// </summary>
        /// <param name="codMovimento">Código do movimento a ser excluído.</param>
        public static void Excluir(long codMovimento)
        {
            using (var sp = new StoredProcedure("ExcluirMovimento"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimento", codMovimento);

                // Executa o stored procedure.
                sp.Executar();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Excluído com sucesso.
                        return;
                    case 1:
                        throw new Exception(String.Format("O movimento informado para exclusão não existe. (código: {0})", codMovimento));
                    case 2:
                        throw new Exception(String.Format("Não é possível excluir uma parcela que não seja a primeira da sequência. (código: {0})", codMovimento));
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Exclui os registros que contém os códigos informados.
        /// </summary>
        /// <param name="movimentos">Códigos dos movimentos a serem excluídos.</param>
        public static void Excluir(IEnumerable<long> movimentos)
        {
            foreach (var codMovimento in movimentos)
            {
                Excluir(codMovimento);
            }
        }

        public static void Replicar(long codMovimento, int quantidadeMeses)
        {
            using ( var sp = new StoredProcedure("ReplicarMovimento") )
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimento", codMovimento);
                sp.DefinirParametro("@quantidadeMeses", quantidadeMeses);

                // Executa o stored procedure.
                sp.Executar();

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Excluído com sucesso.
                        return;
                    case 1:
                        throw new Exception("O movimento informado para replicação não existe.");
                    case 2:  // Não foi possível replicar o movimento por fazer parte de uma movimentação.
                        throw new ExReplicacaoMovimentoParcela(sp);
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Marca como consolidado o registro que contém o código informado.
        /// </summary>
        /// <param name="codMovimento">Código do movimento a ser alterado.</param>
        public static void Consolidar(long codMovimento)
        {
            using (var sp = new StoredProcedure("ConsolidarMovimento"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@codMovimento", codMovimento);

                // Executa o stored procedure.
                sp.Executar();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Excluído com sucesso.
                        return;
                    case 1:
                        throw new Exception(String.Format("O movimento informado para ser marcado como consolidado não existe. (código: {0})", codMovimento));
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Marca como consolidados os registros que contém os códigos informados.
        /// </summary>
        /// <param name="movimentos">Códigos dos movimentos a serem alterados.</param>
        public static void Consolidar(IEnumerable<long> movimentos)
        {
            foreach (var codMovimento in movimentos)
            {
                Consolidar(codMovimento);
            }
        }

        #endregion Cadastro de Movimentos


        #region Resumo

        private enum TipoResumo
        {
            Conta = 1,
            Cartao = 2,
            Categoria = 3
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Movimento agrupados pelo tipo informado.
        /// </summary>
        private static List<Movimento> ListarResumo(TipoResumo tipo, DateTime mesAno)
        {
            using (var sp = new StoredProcedure("CarregarResumo"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@tipoResumo", tipo);
                sp.DefinirParametro("@mesAno", mesAno);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<Movimento>();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Registros carregados com sucesso.
                        return resultado;
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Movimento agrupados por Conta.
        /// </summary>
        public static List<Movimento> ListarResumoConta(DateTime mesAno)
        {
            return ListarResumo(TipoResumo.Conta, mesAno);
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Movimento agrupados por Cartão.
        /// </summary>
        public static List<Movimento> ListarResumoCartao(DateTime mesAno)
        {
            return ListarResumo(TipoResumo.Cartao, mesAno);
        }

        /// <summary>
        /// Carrega uma lista de registros da tabela Movimento agrupados por Categoria.
        /// </summary>
        public static List<Movimento> ListarResumoCategoria(DateTime mesAno)
        {
            return ListarResumo(TipoResumo.Categoria, mesAno);
        }

        #endregion Resumo


        #region Conferência

        /// <summary>
        /// Carrega uma lista de registros consolidados da tabela Movimento agrupados por Conta.
        /// </summary>
        public static Dictionary<DateTime, List<Movimento>> ListarConferencia(DateTime dataInicio, DateTime dataFim)
        {
            using (var sp = new StoredProcedure("CarregarConferencia"))
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@dataInicio", dataInicio);
                sp.DefinirParametro("@dataFim", dataFim);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarListaRegistros<Movimento>();

                // Faz o tratamento do retorno do stored procedure.
                switch (sp.Retorno)
                {
                    case 0:  // Registros carregados com sucesso.

                        var retorno = new Dictionary<DateTime, List<Movimento>>();

                        if ( resultado.Count > 0 )
                        {
                            foreach ( var movimento in resultado )
                            {
                                var mesAno = movimento.MesAno;

                                if ( !retorno.ContainsKey(mesAno) )
                                    retorno.Add(mesAno, new List<Movimento>());

                                retorno[mesAno].Add(movimento);
                            }
                        }

                        return retorno;

                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

        #endregion Conferência

    }
}