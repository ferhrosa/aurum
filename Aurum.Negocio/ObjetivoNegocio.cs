using Aurum.Modelo;
using System.Linq;
using System.Data.Entity;
using Aurum.Negocio.Exceptions;

namespace Aurum.Negocio
{
    public class ObjetivoNegocio
    {
        Contexto contexto;

        public ObjetivoNegocio()
        {
            this.contexto = Contexto.InstanciaWeb;
        }

        public ObjetivoNegocio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public System.Linq.IQueryable<Objetivo> Listar()
        {
            return contexto.Objetivo
                .Include(c => c.Carteira);
        }

        public Objetivo Carregar(int codigo)
        {
            return contexto.Objetivo.Find(codigo);
        }

        public bool Existe(int codigo, out Objetivo objetivo)
        {
            objetivo = contexto.Objetivo.FirstOrDefault(e => e.Codigo == codigo);
            return (objetivo != null);
        }
        public bool Existe(int codigo)
        {
            Objetivo objetivo;
            return Existe(codigo, out objetivo);
        }

        public void Alterar(Objetivo objetivo)
        {
            ValidarDuplicidade(objetivo);

            contexto.Entry(objetivo).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Inserir(Objetivo objetivo)
        {
            ValidarDuplicidade(objetivo);

            contexto.Objetivo.Add(objetivo);
            contexto.SaveChanges();
        }

        public void Excluir(Objetivo objetivo)
        {
            // Se tiver vinculo com movimentação, apenas inativa o objetivo
            if (contexto.Movimentacao.Any(c => c.Objetivo_Codigo == objetivo.Codigo))
            {
                objetivo.Ativo = false;
                contexto.Entry(objetivo).State = EntityState.Modified;
                contexto.SaveChanges();
            }
            else
            {
                contexto.Objetivo.Remove(objetivo);
                contexto.SaveChanges();
            }
        }
        public void Excluir(int codigo)
        {
            Excluir(contexto.Objetivo.FirstOrDefault(e => e.Codigo == codigo));
        }

        private void ValidarDuplicidade(Objetivo objetivo)
        {
            if (contexto.Objetivo.Any(s => s.Codigo != objetivo.Codigo && s.Descricao == objetivo.Descricao))
                throw new NegociosException("Já existe objetivo cadastrado com a descrição informada.");
        }
    }
}
