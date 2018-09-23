using Aurum.Modelo.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using Model = Aurum.AcessoDados;

namespace Aurum.Dominio.Servicos
{
    public class ContaService : BaseService
    {

        public IEnumerable<Conta> Listar()
        {
            return contexto.Conta.Map<IEnumerable<Conta>>();
        }

        public Conta Obter(int id)
        {
            return contexto.Conta.Find(id).Map<Conta>();
        }

        public Conta Inserir(Conta conta)
        {
            var contaModel = conta.Map<Model.Conta>();

            contexto.Conta.Add(contaModel);
            contexto.SaveChanges();

            return contaModel.Map<Conta>();
        }

        public Conta Atualizar(Conta conta)
        {
            var contaModel = contexto.Conta.Attach(conta.Map<Model.Conta>());
            contexto.Entry(contaModel).State = EntityState.Modified;
            contexto.SaveChanges();

            return contaModel.Map<Conta>();
        }

        public void Excluir(int id)
        {
            contexto.Conta.Remove(contexto.Conta.Find(id));
            contexto.SaveChanges();
        }

    }
}
