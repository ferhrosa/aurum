using Aurum.Modelo;
using System.Linq;
using System.Data.Entity;
using Aurum.Negocio.Exceptions;

namespace Aurum.Negocio
{
    public class CarteiraNegocio
    {
        Contexto contexto;

        public CarteiraNegocio()
        {
            this.contexto = Contexto.InstanciaWeb;
        }

        public CarteiraNegocio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public System.Linq.IQueryable<Carteira> Listar()
        {
            return contexto.Carteira;
        }

        public System.Linq.IQueryable<Carteira> ListarSomenteAtivas()
        {
            return contexto.Carteira
                .Where(c => c.Ativo == true);
        }

        public Carteira Carregar(int codigo)
        {
            return contexto.Carteira.Find(codigo);
        }

        public bool Existe(int codigo, out Carteira carteira)
        {
            carteira = contexto.Carteira.FirstOrDefault(e => e.Codigo == codigo);
            return (carteira != null);
        }
        public bool Existe(int codigo)
        {
            Carteira carteira;
            return Existe(codigo, out carteira);
        }

        public void Alterar(Carteira carteira)
        {
            ValidarDuplicidade(carteira);

            if (carteira.CarteiraMae_Codigo == carteira.Codigo)
                throw new NegociosException("A carteira não pode estar vinculada a ela mesma");

            // Se não for cartão de crédido, não salva as informações de dia de vencimento e carteira vinculada
            if (carteira.Tipo != Modelo.Enums.TipoCarteira.CartaoCredito)
            {
                carteira.DiaVencimentoFatura = null;
                carteira.CarteiraMae_Codigo = null;
            }

            contexto.Entry(carteira).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Inserir(Carteira carteira)
        {
            ValidarDuplicidade(carteira);

            // Se não for cartão de crédido, não salva as informações de dia de vencimento e carteira vinculada
            if (carteira.Tipo != Modelo.Enums.TipoCarteira.CartaoCredito)
            {
                carteira.DiaVencimentoFatura = null;
                carteira.CarteiraMae_Codigo = null;
            }

            contexto.Carteira.Add(carteira);
            contexto.SaveChanges();
        }

        public void Excluir(Carteira carteira)
        {
            if (contexto.Carteira.Any(c => c.CarteiraMae_Codigo == carteira.Codigo))
                throw new NegociosException("A carteira não pode ser excluída, pois está vinculada a outra carteira.");

            // Se tiver vinculo com movimentação, apenas inativa a carteira
            if (contexto.Movimentacao.Any(c => c.Carteira_Codigo == carteira.Codigo))
            {
                carteira.Ativo = false;
                contexto.Entry(carteira).State = EntityState.Modified;
                contexto.SaveChanges();
            }
            else
            {
                contexto.Carteira.Remove(carteira);
                contexto.SaveChanges();
            }
        }
        public void Excluir(int codigo)
        {
            Excluir(contexto.Carteira.FirstOrDefault(e => e.Codigo == codigo));
        }

        private void ValidarDuplicidade(Carteira carteira)
        {
            if (contexto.Carteira.Any(s => s.Codigo != carteira.Codigo && s.Descricao == carteira.Descricao))
                throw new NegociosException("Já existe carteira cadastrada com a descrição informada.");
        }
    }
}
