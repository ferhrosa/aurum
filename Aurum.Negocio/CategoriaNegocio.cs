using Aurum.Modelo;
using System.Linq;
using System.Data.Entity;
using Aurum.Negocio.Exceptions;

namespace Aurum.Negocio
{
    public class CategoriaNegocio
    {
        Contexto contexto;

        public CategoriaNegocio()
        {
            this.contexto = Contexto.InstanciaWeb;
        }

        public CategoriaNegocio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public System.Linq.IQueryable<Categoria> Listar()
        {
            return contexto.Categoria;
        }

        public Categoria Carregar(int codigo)
        {
            return contexto.Categoria.Find(codigo);
        }

        public bool Existe(int codigo, out Categoria categoria)
        {
            categoria = contexto.Categoria.FirstOrDefault(e => e.Codigo == codigo);
            return (categoria != null);
        }
        public bool Existe(int codigo)
        {
            Categoria categoria;
            return Existe(codigo, out categoria);
        }

        public void Alterar(Categoria categoria)
        {
            ValidarDuplicidade(categoria);

            contexto.Entry(categoria).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Inserir(Categoria categoria)
        {
            ValidarDuplicidade(categoria);

            contexto.Categoria.Add(categoria);
            contexto.SaveChanges();
        }

        public void Excluir(Categoria categoria)
        {
            // Se tiver vinculo com movimentação, apenas inativa a categoria
            if (contexto.Movimentacao.Any(c => c.Categoria_Codigo == categoria.Codigo))
            {
                categoria.Ativo = false;
                contexto.Entry(categoria).State = EntityState.Modified;
                contexto.SaveChanges();
            }
            else
            {
                contexto.Categoria.Remove(categoria);
                contexto.SaveChanges();
            }
        }
        public void Excluir(int codigo)
        {
            Excluir(contexto.Categoria.FirstOrDefault(e => e.Codigo == codigo));
        }

        private void ValidarDuplicidade(Categoria categoria)
        {
            if (contexto.Categoria.Any(s => s.Codigo != categoria.Codigo && s.Descricao == categoria.Descricao))
                throw new NegociosException("Já existe categoria cadastrada com a descrição informada.");
        }
    }
}
