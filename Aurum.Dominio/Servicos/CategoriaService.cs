using Aurum.Modelo.Entidades;
using System.Collections.Generic;
using System.Data.Entity;
using Model = Aurum.AcessoDados;

namespace Aurum.Dominio.Servicos
{
    public class CategoriaService : BaseService
    {

        public IEnumerable<Categoria> Listar()
        {
            return contexto.Categoria.Map<IEnumerable<Categoria>>();
        }

        public Categoria Obter(int id)
        {
            return contexto.Categoria.Find(id).Map<Categoria>();
        }

        public Categoria Inserir(Categoria categoria)
        {
            var categoriaModel = categoria.Map<Model.Categoria>();

            contexto.Categoria.Add(categoriaModel);
            contexto.SaveChanges();

            return categoriaModel.Map<Categoria>();
        }

        public Categoria Atualizar(Categoria categoria)
        {
            var categoriaModel = contexto.Categoria.Attach(categoria.Map<Model.Categoria>());
            contexto.Entry(categoriaModel).State = EntityState.Modified;
            contexto.SaveChanges();

            return categoriaModel.Map<Categoria>();
        }

        public void Excluir(int id)
        {
            contexto.Categoria.Remove(contexto.Categoria.Find(id));
            contexto.SaveChanges();
        }

    }
}
