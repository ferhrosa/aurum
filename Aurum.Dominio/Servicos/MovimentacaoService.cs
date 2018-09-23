using Aurum.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Model = Aurum.AcessoDados;

namespace Aurum.Dominio.Servicos
{
    public class MovimentacaoService : BaseService
    {

        public IEnumerable<Movimentacao> Listar()
        {
            return contexto.Movimentacao.Map<IEnumerable<Movimentacao>>();
        }

        public Movimentacao Obter(Guid id)
        {
            return contexto.Movimentacao.Find(id).Map<Movimentacao>();
        }

        public Movimentacao Inserir(Movimentacao movimentacao)
        {
            var movimentacaoModel = movimentacao.Map<Model.Movimentacao>();

            contexto.Movimentacao.Add(movimentacaoModel);
            contexto.SaveChanges();

            return movimentacaoModel.Map<Movimentacao>();
        }

        public Movimentacao Atualizar(Movimentacao movimentacao)
        {
            var movimentacaoModel = contexto.Movimentacao.Attach(movimentacao.Map<Model.Movimentacao>());
            contexto.Entry(movimentacaoModel).State = EntityState.Modified;
            contexto.SaveChanges();

            return movimentacaoModel.Map<Movimentacao>();
        }

        public void Excluir(Guid id)
        {
            contexto.Movimentacao.Remove(contexto.Movimentacao.Find(id));
            contexto.SaveChanges();
        }

    }
}
