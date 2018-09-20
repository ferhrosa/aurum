using Aurum.Modelo;
using Aurum.Modelo.Enums;
using Aurum.Negocio.Exceptions;
using Aurum.Negocio.Extensoes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;

namespace Aurum.Negocio
{
    public class MovimentacaoNegocio
    {
        Contexto contexto;

        public MovimentacaoNegocio()
        {
            this.contexto = Contexto.InstanciaWeb;
        }

        public MovimentacaoNegocio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public IQueryable<Movimentacao> Listar(DateTime mesAno)
        {
            return contexto.Movimentacao
                .Include(c => c.Carteira)
                .Include(c => c.Categoria)
                .Include(c => c.Descricao)
                .Where(m => m.MesAno == mesAno)
                .OrderBy(m => m.Consolidado)
                .ThenBy(m => m.Carteira.Descricao)
                .ThenBy(m => m.Data);
        }

        public IQueryable<MovimentacaoDescricao> PesquisarDescricoes(string descricao)
        {
            if (descricao == null)
                descricao = String.Empty;

            var descricoes = from d in contexto.MovimentacaoDescricao
                             where d.Descricao.ToLower().Contains(descricao.ToLower())
                             orderby
                                 // Faz com que sejam exibidas primeiro as descrições que iniciem com o termo buscado.
                                 (d.Descricao.ToLower().StartsWith(descricao.ToLower()) ? 0 : 1),
                                 d.Descricao
                             select d;

            return descricoes;
        }

        /// <summary>
        /// Carrega os dados para serem exibidos na tela de resumo das movimentações.
        /// Os dados estarão agrupados por Categoria.
        /// </summary>
        public dynamic CarregarResumoPorCategoria(DateTime mesAno)
        {
            var dias = ListarDiasDoMes(mesAno);

            var movimentacoes = ListarResumoNormais(mesAno);

            // Carrega a lista de diferentes categorias existentes nas movimentações do mês.
            var categorias = (from m in movimentacoes
                              select m.Categoria).Distinct();

            dynamic resultado = new ExpandoObject();

            resultado.Totais = CarregarResumoTotais(mesAno, movimentacoes);

            resultado.CartoesCredito = ListarResumoCartoesCredito(mesAno);

            resultado.Normais =
                from c in categorias
                select new
                {
                    Descricao = c.Descricao,

                    Dias = (
                        from d in dias
                        select new
                        {
                            Dia = d,

                            Valor = (int?)(
                                from m in movimentacoes
                                where m.MesAno == mesAno
                                   && m.Data.Day == d
                                   && m.Categoria_Codigo == c.Codigo
                                select m.Valor
                            ).Sum(v => v)
                        }

                    )
                };

            return resultado;
        }

        /// <summary>
        /// Carrega os dados para serem exibidos na tela de resumo das movimentações.
        /// Os dados estarão agrupados por Descrição.
        /// </summary>
        public dynamic CarregarResumoPorDescricao(DateTime mesAno)
        {
            var dias = ListarDiasDoMes(mesAno);

            var movimentacoes = ListarResumoNormais(mesAno);

            // Carrega a lista de diferentes categorias existentes nas movimentações do mês.
            var descricoes = (from m in movimentacoes
                              select m.Descricao).Distinct();

            dynamic resultado = new ExpandoObject();

            resultado.Totais = CarregarResumoTotais(mesAno, movimentacoes);

            resultado.CartoesCredito = ListarResumoCartoesCredito(mesAno);

            resultado.Normais =
                from c in descricoes
                select new
                {
                    Descricao = c.Descricao,

                    Dias = (
                        from d in dias
                        select new
                        {
                            Dia = d,

                            Valor = (int?)(
                                from m in movimentacoes
                                where m.MesAno == mesAno
                                   && m.Data.Day == d
                                   && m.Descricao_Codigo == c.Codigo
                                select m.Valor
                            ).Sum(v => v)
                        }

                    )
                };

            return resultado;
        }

        private IQueryable<Movimentacao> ListarResumoNormais(DateTime mesAno)
        {
            return contexto.Movimentacao.Where(m =>
                m.Data.Year == mesAno.Year
                && m.Data.Month == mesAno.Month
                && m.Carteira.Tipo != TipoCarteira.CartaoCredito);
        }

        private dynamic ListarResumoCartoesCredito(DateTime mesAno)
        {
            var dias = ListarDiasDoMes(mesAno);

            var movimentacoes = contexto.Movimentacao.Where(m =>
                m.Carteira.Tipo == TipoCarteira.CartaoCredito
                && m.MesAno == mesAno);

            // Carrega a lista de diferentes categorias existentes nas movimentações do mês.
            var cartoes = (from m in movimentacoes
                           select m.Carteira).Distinct();

            var cartoesCredito =
                from c in cartoes
                select new
                {
                    Descricao = c.Descricao,

                    Dias = (
                       from d in dias
                       select new
                       {
                           Dia = d,

                           Valor = (
                               c.DiaVencimentoFatura != d
                               ? null
                               : (int?)(
                                   from m in movimentacoes
                                   where m.Carteira_Codigo == c.Codigo
                                   select m.Valor
                               ).Sum(v => v)
                           )
                       }
                   )
                };

            return cartoesCredito;
        }

        private dynamic CarregarResumoTotais(DateTime mesAno, IEnumerable<Movimentacao> movimentacoes)
        {
            var dias = ListarDiasDoMes(mesAno);

            var totais = from d in dias
                         select new
                         {
                             Dia = d,

                             Valor = (int?)(
                                 from m in movimentacoes
                                 where m.Data.Day == d
                                 select m
                             ).Sum(m => m.Valor)
                         };

            return totais;
        }

        private dynamic CarregarResumoHistorico(DateTime mesAno)
        {
            var dias = ListarDiasDoMes(mesAno);

            var movimentacoes =
                from m in contexto.Movimentacao
                where (
                    m.Carteira.Tipo != TipoCarteira.CartaoCredito
                    && m.Data.Year == mesAno.Year
                    && m.Data.Month == mesAno.Month
                )
                || (
                    m.Carteira.Tipo == TipoCarteira.CartaoCredito
                    && m.MesAno == mesAno
                )
                select m;

            var valorInicial = (
                from m in contexto.Movimentacao
                where (
                    m.Carteira.Tipo != TipoCarteira.CartaoCredito
                    && m.Data < mesAno
                )
                || (
                    m.Carteira.Tipo == TipoCarteira.CartaoCredito
                    && m.MesAno < mesAno
                )
                select m.Valor).Sum();

            // TODO: Retornar objeto do histórico.
            return null;
        }

        /// <summary>
        /// Carrega uma lista com todos os dias existentes no mês informado.
        /// </summary>
        private IEnumerable<int> ListarDiasDoMes(DateTime mesAno)
        {
            var dias = new List<int>();

            for (var i = 1; i <= DateTime.DaysInMonth(mesAno.Year, mesAno.Month); i++)
                dias.Add(i);

            return dias;
        }


        public Movimentacao Carregar(long codigo)
        {
            return contexto.Movimentacao
                .Include(m => m.Descricao)
                .Include(m => m.Carteira)
                .SingleOrDefault(m => m.Codigo == codigo);
        }

        public bool Existe(long codigo, out Movimentacao movimentacao)
        {
            movimentacao = contexto.Movimentacao.FirstOrDefault(e => e.Codigo == codigo);
            return (movimentacao != null);
        }
        public bool Existe(long codigo)
        {
            Movimentacao movimentacao;
            return Existe(codigo, out movimentacao);
        }

        public void Alterar(Movimentacao movimentacao)
        {
            VerificarExistenciaDescricao(movimentacao);

            // Quando está alterando, não permite alterar as informações de parcelamento.
            var movimentacaoOriginal = Carregar(movimentacao.Codigo);
            movimentacao.NumeroParcela = movimentacaoOriginal.NumeroParcela;
            movimentacao.TotalParcelas = movimentacaoOriginal.TotalParcelas;

            // Remove o objeto original do contexto, para não ser salvo no banco de dados.
            contexto.Entry(movimentacaoOriginal).State = EntityState.Detached;

            contexto.Entry(movimentacao).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Inserir(Movimentacao movimentacao)
        {
            // Não permite que seja informado apenas um dos dois campos de parcelamento.
            if (movimentacao.NumeroParcela.HasValue != movimentacao.TotalParcelas.HasValue)
                throw new NegociosException("Devem ser informados os dois campos de parcelas, ou nenhum dos dois.");

            var carteiraNegocio = new CarteiraNegocio(contexto);
            movimentacao.Carteira = carteiraNegocio.Carregar(movimentacao.Carteira_Codigo);

            if (movimentacao.NumeroParcela.HasValue && movimentacao.Carteira.Tipo != TipoCarteira.CartaoCredito)
                throw new NegociosException("Parcelamento só pode ser feito com cartão de crédito.");

            VerificarExistenciaDescricao(movimentacao);

            contexto.Movimentacao.Add(movimentacao);
            contexto.SaveChanges();

            // Se há informação de parcelamento...
            if (movimentacao.NumeroParcela.HasValue)
            {
                // Cria registros para todas as parcelas.
                for (short i = 1; i <= (movimentacao.TotalParcelas.Value - movimentacao.NumeroParcela.Value); i++)
                {
                    // Cria uma cópia da movimentação atual, que será uma das parcelas.
                    var parcela = movimentacao.Clonar();

                    parcela.Codigo = 0;
                    parcela.Consolidado = false;
                    parcela.MesAno = movimentacao.MesAno.AddMonths(i);
                    parcela.NumeroParcela = (short?)(movimentacao.NumeroParcela + i);
                    parcela.PrimeiraParcela = movimentacao;

                    contexto.Movimentacao.Add(parcela);
                }

                // Salva as parcelas que foram criadas.
                contexto.SaveChanges();
            }
        }

        public void Excluir(Movimentacao movimentacao)
        {
            contexto.Movimentacao.Remove(movimentacao);
            contexto.SaveChanges();
        }
        public void Excluir(long codigo)
        {
            Excluir(contexto.Movimentacao.FirstOrDefault(e => e.Codigo == codigo));
        }

        private void VerificarExistenciaDescricao(Movimentacao movimentacao)
        {
            if (movimentacao.Descricao != null)
            {
                movimentacao.Descricao.Descricao = movimentacao.Descricao.Descricao.Trim();

                // Verifica se já existe descrição cadastrada com o mesmo
                // texto da descrição informada para a movimentação.
                var descricao = contexto.MovimentacaoDescricao.FirstOrDefault(
                    d => d.Descricao.ToLower() == movimentacao.Descricao.Descricao.ToLower());

                // Se for encontrada uma descrição...
                if (descricao != null)
                {
                    // Faz com que a descrição que veio junto com a movimentação não seja incluída.
                    contexto.Entry(movimentacao.Descricao).State = EntityState.Detached;

                    // Relaciona a movimentação à descrição encontrada.
                    movimentacao.Descricao = descricao;
                    movimentacao.Descricao_Codigo = descricao.Codigo;

                    // Marca a descrição para que não seja alterada.
                    contexto.Entry(descricao).State = EntityState.Unchanged;
                }
                else
                {
                    var estadoAnterior = contexto.Entry(movimentacao).State;

                    contexto.Entry(movimentacao).State = EntityState.Detached;
                    contexto.Entry(movimentacao.Descricao).State = EntityState.Added;

                    contexto.SaveChanges();

                    contexto.Entry(movimentacao).State = estadoAnterior;
                    contexto.Entry(movimentacao.Descricao).State = EntityState.Unchanged;

                    movimentacao.Descricao_Codigo = movimentacao.Descricao.Codigo;
                }
            }
        }

        public void Consolidar(long codigo)
        {
            var movimentacao = Carregar(codigo);
            movimentacao.Consolidado = true;

            contexto.SaveChanges();
        }

        public void Replicar(long codigo, int quantidadeMeses)
        {
            var movimentacao = Carregar(codigo);

            // Bloqueia a replicação caso a movimentação seja parte de um parcelamento
            if (movimentacao.PrimeiraParcela != null || movimentacao.NumeroParcela.HasValue || movimentacao.TotalParcelas.HasValue)
                throw new NegociosException(String.Format("Não é possível replicar a movimentação de código {0}, pois essa faz parte de um parcelamento.", codigo));

            for (int i = 1; i <= quantidadeMeses; i++)
            {
                var replicado = movimentacao.Clonar();
                replicado.Codigo = 0;
                replicado.Consolidado = false;
                replicado.MesAno = replicado.MesAno.AddMonths(i);
                replicado.Data = replicado.Data.AddMonths(i);
                //replicado.PrimeiraParcela = null;
                //replicado.NumeroParcela = null;
                //replicado.TotalParcelas = null;

                contexto.Movimentacao.Add(replicado);
            }

            contexto.SaveChanges();
        }
    }
}
