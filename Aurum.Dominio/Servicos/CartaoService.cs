using Aurum.Modelo.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using Model = Aurum.AcessoDados;

namespace Aurum.Dominio.Servicos
{
    public class CartaoService : BaseService
    {

        public IEnumerable<Cartao> Listar()
        {
            return contexto.Cartao.Map<IEnumerable<Cartao>>();
        }

        public Cartao Obter(int id)
        {
            return contexto.Cartao.Find(id).Map<Cartao>();
        }

        public Cartao Inserir(Cartao cartao)
        {
            var cartaoModel = cartao.Map<Model.Cartao>();

            contexto.Cartao.Add(cartaoModel);
            contexto.SaveChanges();

            return cartaoModel.Map<Cartao>();
        }

        public Cartao Atualizar(Cartao cartao)
        {
            var cartaoModel = contexto.Cartao.Attach(cartao.Map<Model.Cartao>());
            contexto.Entry(cartaoModel).State = EntityState.Modified;
            contexto.SaveChanges();

            return cartaoModel.Map<Cartao>();
        }

        public void Excluir(int id)
        {
            contexto.Cartao.Remove(contexto.Cartao.Find(id));
            contexto.SaveChanges();
        }

    }
}
